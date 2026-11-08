## Documentation rules

- Read CONTEXT.md for the project overview and key constraints.
- Read only the documents relevant to the current task.
- For modular 3D assets (vehicles, machines, buildings, equipment), follow `.claude/rules/modular-modeling.md` before creating Blender geometry or Unity prefabs.
- Concepts/World/ defines world-design principles and geological guidance.
- Creatures/ contains creature-specific documentation.
- Mechanics/ contains gameplay-system documentation.
- GAME_PLAN.md tracks development priorities.
- DESIGN_DECISION_WORKFLOW.md provides an optional, focused method for finding design gaps, asking prioritized questions, and documenting decisions without duplicating source files. Do not launch a full questionnaire automatically.
- InTheDeep_ClaudeCode_Unity_Workflow.md defines the development workflow.
- CLAUDE_CODE_TOKEN_OPTIMIZATION.md describes practical token, prompt-cache,
  session and Unity CLI log optimization; use it when configuring the workflow.
- If two documents conflict, do not silently choose one.
  Identify the conflict and ask which decision is authoritative.
- Update the document that owns a decision instead of copying
  the same detailed explanation into multiple files.