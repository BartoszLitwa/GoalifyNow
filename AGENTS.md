# AGENTS

This repository uses `.cursor/rules/` as the canonical AI instruction set. Start with:

- `.cursor/rules/00 - INDEX.mdc`
- Area-specific rules (for example `.cursor/rules/GoalifyNow.WebApp - core.mdc` when editing `apps/webapp`)

**Non-negotiable**

- Never commit or push without explicit user request.
- Never use subagents or the Task tool (see `00 - SubAgent - Disable.mdc`).
- Do not create documentation or summary markdown files unless explicitly requested.
- Prefer the comment and documentation policies in `.cursor/rules/.cursorrules.mdc`.

If instructions conflict, treat `.cursor/rules/` as the source of truth.
