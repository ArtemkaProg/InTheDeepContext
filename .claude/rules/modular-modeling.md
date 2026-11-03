# Modular asset modeling (Blender → Unity)

Apply this workflow whenever creating or changing a **multi-part 3D asset** (vehicles, machines, buildings, equipment), particularly when parts must be swapped or animated. This is an **agent implementation rule**, not a finalized asset specification. Consult the task reference(s) and any authoritative design document; do not invent gameplay mechanics.

## Non-negotiable constraints

- Preserve the approved reference **silhouette, proportions, major shapes, component placement and style**. Do not silently redesign an asset for convenience.
- An image is a **visual reference, not an engineering drawing**. Hidden geometry and joints are uncertain; label assumptions, choose the least disruptive solution and request a decision only for consequential ambiguities.
- **Separate only meaningful parts**: independently replaceable, interactive, moving, or separately reusable components. Don't turn bolts/panels into hundreds of gameplay modules. Decorative submeshes can remain grouped.
- **Model compatibility explicitly** with stable connectors and rules. No assumption that arbitrary parts fit each other. For ship-specific slot rules, read `Mechanics/Vehicles/SHIP_CONSTRUCTION.md`.
- Prefer simple, deliberate geometry and legible shapes over dense AI-concept greebling. Additional geometry must serve silhouette, interaction or close-up readability.

## Execution stages

1. **Reference audit** — Identify views, intended scale, visible modular boundaries, moving parts, materials, and uncertainty. Write a compact component map: fixed parts, interchangeable parts, optional attachments, attachment directions and expected functions.
2. **Blockout** — Generate simple Blender meshes via **repeatable Python scripts**. Use separate objects/collections for meaningful components, consistent units and axes, coherent origins and naming. Establish the assembled silhouette before surface detail.
3. **Validate blockout** — Render **front / side / top / 3-quarter** previews and an assembled view; render an exploded view only when helpful. Compare them to reference imagery. Verify shape and interfaces before proceeding. If a reference has inconsistent projections, preserve the strongest established design features and record the discrepancy.
4. **Connector design** — Define socket IDs, local transforms, alignment axes, allowable part categories, and clearance envelopes; use Blender empties and corresponding Unity attachment transforms. Confirm mating interfaces and alignment numerically; check for unwanted gaps/penetration in the assembled configuration.
5. **Detail in passes** — Refine primary forms, then secondary forms, then essential accents/materials. Keep replaceable modules separate. Avoid changing approved overall proportions. Test moving-part travel and nearby clearances.
6. **Export and Unity integration** — Preserve the editable `.blend` and generation scripts; export named parts, consistent scale/axes and pivots. Create prefabs as needed. Provide simple colliders / LOD only when warranted; test assembled alignment, transforms, animations and material import in Unity.
7. **Report and stop** — State generated files, previews, tests run, deviations, assumptions and unverified aspects. Never claim physical realism, perfect reference matching or successful Unity integration without checking.

## Practical automation guidelines

- **Small, rerunnable scripts**, deterministic input parameters and checkpoints; avoid a giant one-shot script that produces a final mesh without inspection.
- Prefer programmatic geometry/transform checks (socket orientation, bounds, relative offsets, intersecting volumes). Visual quality **still requires rendered previews and review**; code alone cannot verify resemblance.
- Store asset metadata and connector definitions separately from mesh names where practical; do not hard-code gameplay behavior in Blender object names.
- A module may look mechanically continuous with the base asset while remaining a separate object. Hide seams with authored collars/frames **without** changing the reference silhouette.
- Avoid simulated physics for decorative joints or covers; animate transform/keyframes unless actual gameplay physics requires otherwise.
- If preview inspection is not available, stop at the validated-as-far-as-possible stage, mark visual approval as pending and **do not** progress to irreversible detail on assumptions.

## Completion criteria

A modular asset is ready for further detailing/integration only when: (a) the assembled blockout matches the reference at major-shape level, (b) component boundaries are explicitly documented, (c) relevant attachment points align and incompatible combinations are rejected, (d) source scripts/meshes are reusable, and (e) the current stage's verification evidence is recorded.

**Example only — not a mandatory ship design:** a submersible could have a cockpit section, main pressure hull, propulsion units and optional external tools. Their interchangeability is governed by actual connector specs and design documentation; the concept art alone does not establish it.
