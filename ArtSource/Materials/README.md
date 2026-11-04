# Material source library — InTheDeep

> Agent-facing inventory and handling notes. These are **source textures**, not final Unity materials, shader assignments, or approved region palettes.

## Locations

- `PolyHaven/Terrain/` — 10 selected ground / cliff / rock / sediment sources.
- `PolyHaven/Industrials/` — 6 selected industrial surface sources.
- `Custom/WetSand/` — additional user-chosen wavy/wet sand material (preserve it; provenance/license must be checked separately).

## Poly Haven material inventory (folder → original asset slug)

| Folder | Original source |
|---|---|
| Terrain/BrownMud | `brown_mud` |
| Terrain/CliffSide | `rock_face_03` |
| Terrain/ClayFloor001 | `clay_floor_001` |
| Terrain/DampBeachSand02 | `damp_beach_sand_02` |
| Terrain/DarkRock | `dark_rock` |
| Terrain/GravelFloor04 | `gravel_floor_04` |
| Terrain/RockWall | `quarry_wall_02` |
| Terrain/RockyTerrain | `rocks_ground_02` |
| Terrain/Rubble | `rocks_ground_09` |
| Terrain/SandyGravel02 | `sandy_gravel_02` |
| Industrials/AntiSkidTiles | `anti_skid_tiles` |
| Industrials/Concrete | `gravel_concrete_03` |
| Industrials/MetalTreadPlate | `metal_plate` (raised anti-slip tread plate, NOT perforated grating) |
| Industrials/MetalPlate | `metal_plate_02` |
| Industrials/RubberTiles | `rubber_tiles` |
| Industrials/RustyMetal | `rusty_metal_04` |

The folder labels describe intended uses, not geological identity. Do not assume an entire biome needs a unique texture set.

## Custom/WetSand — preserved PBR sources

This user-selected non-Poly-Haven set is stored outside the Unity `Assets/` folder. Retained maps: `GroundSand005_COL_1K.jpg` (color), `GroundSand005_NRM_1K.jpg` (normal), `GroundSand005_GLOSS_1K.jpg` (glossiness), `GroundSand005_AO_1K.jpg` (ambient occlusion), and `GroundSand005_DISP16_1K.tiff` (16-bit displacement/height source).

Removed redundant bump and lower-precision displacement alternatives, reflection map, preview image, and old Unity `.meta` / `.terrainlayer` files. Do not assume the old Unity terrain layer can be reused; create a fresh layer/material during Unity import. Keep the 16-bit height map available for displacement experiments, but do not import it into runtime builds without a demonstrated need. Glossiness is not roughness: invert as appropriate for a selected shader. Avoid generating a second normal from the displacement map unless explicitly required.

## File types and usage

- `*_diff_*` / `*_col_*`: base color (sRGB).
- `*_nor_gl_*`: **OpenGL-convention** tangent-space normals (linear). Check the target Unity shader/normal-Y convention; flip green if required.
- `*_rough_*`: roughness (linear); Unity shaders may instead expect **smoothness = 1 − roughness**.
- `*_metal_*`: metallic mask, mainly useful for metals. Nonmetal sources can use constant metallic = 0.
- `*_ao_*`: ambient occlusion; optional depending on shader.
- `*_arm_*`: channel-packed AO / roughness / metallic source; do not feed directly to a Unity mask input without checking channel layout and repacking for the selected render pipeline.
- `*_disp_*` / `*_height_*`: optional height data for parallax, blending or authored geometry; do not enable expensive tessellation by default.
- `*_bump_*`: alternative height/bump source; normally not needed in addition to a normal map.
- `*_1k.blend`: Poly Haven material preview / source scene; retain as a reference. `.bin` and `.gltf` preview exports were removed as unused for the PBR texture workflow.

**Do not delete or duplicate remaining source maps based solely on suffixes.** Choose the actual Unity shader/pipeline first, generate optimized/importable derivatives under the Unity project, then reassess unused source channels. EXR maps may require conversion to efficient GPU formats; source file size is not runtime VRAM size.

## Asset provenance and integration rules

- Poly Haven materials are published as CC0; still record each source URL in the final asset register (e.g. `https://polyhaven.com/a/<asset_slug>`) and verify it resolves.
- The separate `WetSand` texture is **not automatically covered by Poly Haven CC0**; verify its origin and license before distributing the game.
- Keep original source assets separate from runtime assets. Avoid importing the entire source library into Unity by default.
- Prefer stylized readable silhouettes and measured texture scale. Use material blending, lighting and shader treatment rather than assuming photographic source textures make the project photorealistic.
- For geological usage constraints see `../../Concepts/World/GEOLOGY_AND_ENVIRONMENTS.md`.
