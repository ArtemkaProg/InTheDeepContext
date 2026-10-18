using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace SilentRiftExport
{
    public static class SilentRiftSetup
    {
        const string Root = "Assets/SilentRift";
        [Serializable] public class MaterialInfo { public string name, source, texture; public bool twoSided; public float smoothness; public int atlasSize; }
        [Serializable] public class LightInfo { public string name, type; public float energy; public float[] color; }
        [Serializable] public class ViewInfo { public string name, label; public float fov; }
        [Serializable] public class Manifest { public MaterialInfo[] materials; public LightInfo[] lights; public ViewInfo[] views; }

        [MenuItem("Tools/Silent Rift/Create Unity Scene")]
        public static void CreateScene()
        {
            string manifestPath = Root + "/SilentRift_Manifest.json";
            if (!File.Exists(manifestPath)) throw new FileNotFoundException("Copy the SilentRift folder into Assets first.", manifestPath);
            Manifest data = JsonUtility.FromJson<Manifest>(File.ReadAllText(manifestPath));
            var pipeline = GraphicsSettings.currentRenderPipeline;
            string pipelineName = pipeline == null ? "Built-in" : pipeline.GetType().Name;
            bool hd = pipelineName.Contains("HDRenderPipeline");
            bool urp = pipelineName.Contains("Universal");
            if (pipeline != null && !hd && !urp)
                throw new NotSupportedException("This setup supports Built-in, URP and HDRP. Current pipeline: " + pipelineName);
            string shaderName = hd ? "HDRP/Lit" : urp ? "Universal Render Pipeline/Lit" : "Standard";
            Shader shader = Shader.Find(shaderName);
            if (shader == null) throw new InvalidOperationException("Shader is unavailable: " + shaderName);
            Directory.CreateDirectory(Root + "/Materials");
            Directory.CreateDirectory(Root + "/Generated");
            AssetDatabase.Refresh();
            var materials = new Dictionary<string, Material>();
            foreach (var info in data.materials)
            {
                string texturePath = Root + "/Textures/" + info.texture;
                var ti = AssetImporter.GetAtPath(texturePath) as TextureImporter;
                if (ti == null) throw new FileNotFoundException("Texture not imported", texturePath);
                ti.textureType = TextureImporterType.Default;
                ti.sRGBTexture = true;
                // Each triangle has its own tiny color tile. Mips and compression would bleed across tiles.
                ti.mipmapEnabled = false;
                ti.textureCompression = TextureImporterCompression.Uncompressed;
                ti.filterMode = FilterMode.Bilinear;
                ti.wrapMode = TextureWrapMode.Clamp;
                ti.npotScale = TextureImporterNPOTScale.None;
                ti.maxTextureSize = Math.Max(2048, info.atlasSize);
                ti.SaveAndReimport();
                string matPath = Root + "/Materials/" + info.name + ".mat";
                Material mat = AssetDatabase.LoadAssetAtPath<Material>(matPath);
                Shader chosen = shader;
                if (!hd && !urp && info.twoSided)
                {
                    EnsureBuiltInFoliageShader();
                    chosen = Shader.Find("SilentRift/BuiltInTwoSided");
                    if (chosen == null) chosen = shader;
                }
                if (mat == null) { mat = new Material(chosen); AssetDatabase.CreateAsset(mat, matPath); }
                else mat.shader = chosen;
                mat.name = info.name;
                Texture texture = AssetDatabase.LoadAssetAtPath<Texture2D>(texturePath);
                foreach (string property in new[] { "_MainTex", "_BaseMap", "_BaseColorMap" })
                    if (mat.HasProperty(property)) mat.SetTexture(property, texture);
                foreach (string property in new[] { "_Color", "_BaseColor" })
                    if (mat.HasProperty(property)) mat.SetColor(property, Color.white);
                SetFloat(mat, "_Metallic", 0);
                SetFloat(mat, "_Smoothness", info.smoothness);
                SetFloat(mat, "_Glossiness", info.smoothness);
                SetFloat(mat, "_Cull", info.twoSided ? 0 : 2);
                if (hd)
                {
                    SetFloat(mat, "_DoubleSidedEnable", info.twoSided ? 1 : 0);
                    SetFloat(mat, "_DoubleSidedNormalMode", 1);
                    SetFloat(mat, "_CullMode", info.twoSided ? 0 : 2);
                    SetFloat(mat, "_CullModeForward", info.twoSided ? 0 : 2);
                    if (info.twoSided) mat.EnableKeyword("_DOUBLESIDED_ON");
                    else mat.DisableKeyword("_DOUBLESIDED_ON");
                    Type utils = FindType("UnityEditor.Rendering.HighDefinition.HDShaderUtils");
                    MethodInfo reset = utils == null ? null : utils.GetMethod("ResetMaterialKeywords", BindingFlags.Public | BindingFlags.Static, null, new[] { typeof(Material) }, null);
                    if (reset != null) reset.Invoke(null, new object[] { mat });
                }
                EditorUtility.SetDirty(mat);
                materials[info.name] = mat;
            }
            string fbxPath = Root + "/Models/SilentRift.fbx";
            var mi = AssetImporter.GetAtPath(fbxPath) as ModelImporter;
            if (mi == null) throw new FileNotFoundException("FBX not imported", fbxPath);
            mi.globalScale = 1;
            mi.useFileScale = true;
            mi.bakeAxisConversion = true;
            mi.importAnimation = false;
            mi.importCameras = false;
            mi.importLights = false;
            mi.importNormals = ModelImporterNormals.Import;
            mi.isReadable = false;
            mi.generateSecondaryUV = false;
            mi.meshCompression = ModelImporterMeshCompression.Off;
            foreach (var pair in materials)
                mi.AddRemap(new AssetImporter.SourceAssetIdentifier(typeof(Material), pair.Key), pair.Value);
            mi.SaveAndReimport();
            AssetDatabase.SaveAssets();

            // Create an additive scene so the user's current scene is never cleared.
            Scene created = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Additive);
            SceneManager.SetActiveScene(created);
            GameObject asset = AssetDatabase.LoadAssetAtPath<GameObject>(fbxPath);
            GameObject model = (GameObject)PrefabUtility.InstantiatePrefab(asset, created);
            model.name = "Silent Rift";
            foreach (MeshRenderer renderer in model.GetComponentsInChildren<MeshRenderer>(true))
            {
                var assigned = renderer.sharedMaterials;
                for (int i = 0; i < assigned.Length; i++)
                {
                    if (assigned[i] == null) continue;
                    var pair = materials.FirstOrDefault(p => assigned[i].name.StartsWith(p.Key, StringComparison.Ordinal));
                    if (pair.Value != null) assigned[i] = pair.Value;
                }
                renderer.sharedMaterials = assigned;
                if (renderer.name.StartsWith("Terrain_", StringComparison.Ordinal))
                {
                    MeshCollider collider = renderer.gameObject.AddComponent<MeshCollider>();
                    collider.sharedMesh = renderer.GetComponent<MeshFilter>().sharedMesh;
                    collider.convex = false;
                }
            }
            var transforms = model.GetComponentsInChildren<Transform>(true).ToDictionary(t => t.name, t => t);
            foreach (var info in data.lights)
            {
                if (!transforms.TryGetValue(info.name, out Transform marker)) continue;
                var go = new GameObject("Lighting " + info.name);
                go.transform.SetParent(model.transform, false);
                go.transform.position = marker.position;
                if (transforms.TryGetValue(info.name + "_Target", out Transform target))
                    go.transform.rotation = Quaternion.LookRotation(target.position - marker.position);
                Light light = go.AddComponent<Light>();
                light.type = info.type == "SUN" ? LightType.Directional : LightType.Point;
                light.color = new Color(info.color[0], info.color[1], info.color[2]);
                light.range = info.type == "AREA" ? 100 : 17;
                light.intensity = info.type == "SUN" ? 1.3f : info.type == "AREA" ? 0.5f : Mathf.Clamp(info.energy / 210f, 0.3f, 4f);
                light.shadows = info.type == "SUN" ? LightShadows.Soft : LightShadows.None;
                if (hd) ConfigureHDLight(go, info.type == "SUN" ? 3000 : info.type == "AREA" ? 6000 : Mathf.Max(250, info.energy * 2));
            }
            foreach (var view in data.views)
            {
                if (!transforms.TryGetValue(view.name, out Transform marker)) continue;
                var go = new GameObject(view.label);
                go.transform.SetParent(model.transform, false);
                go.transform.position = marker.position;
                if (transforms.TryGetValue(view.name + "_Target", out Transform target))
                    go.transform.rotation = Quaternion.LookRotation(target.position - marker.position);
                Camera camera = go.AddComponent<Camera>();
                if (hd)
                {
                    Type cameraData = FindType("UnityEngine.Rendering.HighDefinition.HDAdditionalCameraData");
                    if (cameraData != null && go.GetComponent(cameraData) == null) go.AddComponent(cameraData);
                }
                camera.fieldOfView = view.fov;
                camera.nearClipPlane = 0.08f;
                camera.farClipPlane = 650;
                camera.clearFlags = CameraClearFlags.SolidColor;
                camera.backgroundColor = new Color(0.035f, 0.075f, 0.065f);
                camera.enabled = view == data.views[0];
                if (camera.enabled) go.tag = "MainCamera";
            }
            RenderSettings.ambientMode = AmbientMode.Flat;
            RenderSettings.ambientLight = new Color(0.12f, 0.17f, 0.15f);
            if (hd) ConfigureHDExposure(model);
            string scenePath = AssetDatabase.GenerateUniqueAssetPath(Root + "/Generated/SilentRift.unity");
            PrefabUtility.SaveAsPrefabAsset(model, AssetDatabase.GenerateUniqueAssetPath(Root + "/Generated/SilentRift.prefab"));
            EditorSceneManager.SaveScene(created, scenePath);
            AssetDatabase.SaveAssets();
            Selection.activeGameObject = model;
            Debug.Log("Silent Rift created: " + scenePath + ". Pipeline: " + pipelineName + ". Lighting is an approximation; adjust exposure for your project.");
            EditorUtility.DisplayDialog("Silent Rift", "Scene and prefab created in Assets/SilentRift/Generated.\nPipeline: " + pipelineName + "\nUse the Scene view to explore. No player controller is included.", "OK");
        }

        static void SetFloat(Material m, string key, float value) { if (m.HasProperty(key)) m.SetFloat(key, value); }
        static Type FindType(string name) { return AppDomain.CurrentDomain.GetAssemblies().Select(a => a.GetType(name)).FirstOrDefault(t => t != null); }
        static void ConfigureHDLight(GameObject go, float intensity)
        {
            Type type = FindType("UnityEngine.Rendering.HighDefinition.HDAdditionalLightData");
            if (type == null) return;
            Component data = go.GetComponent(type) ?? go.AddComponent(type);
            MethodInfo method = type.GetMethod("SetIntensity", new[] { typeof(float) });
            if (method != null) method.Invoke(data, new object[] { intensity });
        }
        static void ConfigureHDExposure(GameObject parent)
        {
            Type volumeType = FindType("UnityEngine.Rendering.Volume");
            Type profileType = FindType("UnityEngine.Rendering.VolumeProfile");
            Type exposureType = FindType("UnityEngine.Rendering.HighDefinition.Exposure");
            if (volumeType == null || profileType == null || exposureType == null) return;
            var go = new GameObject("Cave exploration exposure");
            go.transform.SetParent(parent.transform, false);
            Component volume = go.AddComponent(volumeType);
            volumeType.GetProperty("isGlobal")?.SetValue(volume, true);
            ScriptableObject profile = ScriptableObject.CreateInstance(profileType);
            string path = AssetDatabase.GenerateUniqueAssetPath(Root + "/Generated/Exposure.asset");
            AssetDatabase.CreateAsset(profile, path);
            MethodInfo add = profileType.GetMethod("Add", new[] { typeof(Type), typeof(bool) });
            if (add != null)
            {
                object exposure = add.Invoke(profile, new object[] { exposureType, true });
                SetParameter(exposure, "mode", "Automatic");
                SetParameter(exposure, "limitMin", -4f);
                SetParameter(exposure, "limitMax", 9f);
                if (exposure is UnityEngine.Object obj) AssetDatabase.AddObjectToAsset(obj, profile);
            }
            volumeType.GetField("sharedProfile")?.SetValue(volume, profile);
            EditorUtility.SetDirty(profile);
        }
        static void SetParameter(object component, string field, object value)
        {
            object parameter = component.GetType().GetField(field)?.GetValue(component);
            if (parameter == null) return;
            var property = parameter.GetType().GetProperty("value");
            if (property == null) return;
            if (property.PropertyType.IsEnum && value is string label) value = Enum.Parse(property.PropertyType, label);
            property.SetValue(parameter, value);
            parameter.GetType().GetProperty("overrideState")?.SetValue(parameter, true);
        }
        static void EnsureBuiltInFoliageShader()
        {
            string target = Root + "/Generated/BuiltInTwoSided.shader";
            if (!File.Exists(target))
            {
                File.Copy(Root + "/Editor/BuiltInTwoSided.shader.txt", target);
                AssetDatabase.ImportAsset(target, ImportAssetOptions.ForceSynchronousImport);
            }
        }
    }
}
