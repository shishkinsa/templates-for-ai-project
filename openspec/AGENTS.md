# OpenSpec Instructions

Instructions for AI assistants using OpenSpec in **Sample Project**.

См. также [openspec/project.md](project.md) — контекст стека, ADR, соглашения. Язык коммуникации: **русский**.

## TL;DR Quick Checklist

- Search existing work: `npx openspec list --specs`, `npx openspec list`
- Decide scope: new capability vs modify existing capability
- Pick a unique `change-id`: kebab-case, verb-led (`add-`, `update-`, `remove-`)
- For full stack entities: use schema `full-stack` — `openspec new change "<id>" --schema full-stack`
- Scaffold: `proposal.md`, `tasks.md`, `design.md` (if needed), delta specs per affected capability
- Write deltas: `## ADDED|MODIFIED|REMOVED|RENAMED Requirements`; at least one `#### Scenario:` per requirement
- Validate: `npx openspec validate <change-id> --strict --no-interactive`
- Request approval: do not start implementation until proposal is approved

## Three-Stage Workflow

### Stage 1: Creating Changes

Create proposal when you need to:

- Add features or functionality
- Make breaking changes (API, schema)
- Change architecture or patterns
- Update security patterns

Skip proposal for:

- Bug fixes restoring spec behavior
- Typos, formatting, comments
- Non-breaking dependency updates

**Workflow**

1. Review `openspec/project.md`, `npx openspec list`, `npx openspec list --specs`
2. Choose verb-led `change-id`; scaffold under `openspec/changes/<id>/`
3. Draft delta specs with `## ADDED|MODIFIED|REMOVED Requirements`
4. Run `npx openspec validate <id> --strict --no-interactive`

### Stage 2: Implementing Changes

1. Read `proposal.md`, `design.md` (if exists), `tasks.md`
2. Sync OpenAPI: [docs/architecture/openapi/components/openapi.yaml](../docs/architecture/openapi/components/openapi.yaml)
3. Implement backend (Clean Architecture) and frontend (FSD) per [docs/ai/workflows/add-entity.md](../docs/ai/workflows/add-entity.md)
4. Mark all tasks `- [x]` when done
5. Run `.\scripts\verify.ps1`

### Stage 3: Archiving Changes

After deployment:

- Run `/opsx-archive` or `npx openspec archive <change-id> --yes`
- Delta specs merge into `openspec/specs/`
- Change moves to `openspec/changes/archive/YYYY-MM-DD-<name>/`

## Artifact Roles (Sample Project)

| Layer | Location | Role |
|-------|----------|------|
| Behavior (WHAT) | `openspec/specs/` | Canonical requirements |
| Changes (PROPOSED) | `openspec/changes/` | Delta specs + tasks |
| Business (WHY) | `docs/requirements/business/` | Goals, user stories |
| NFR | `docs/requirements/non-functional/` | Performance, security |
| Strategy | `docs/architecture/adr/` | Technology decisions |
| REST contract | `docs/architecture/openapi/` | API paths/schemas |
| C4 | `docs/architecture/diagram/` | Container boundaries |

## Spec File Format

Each spec needs `## Purpose` and `## Requirements`.

```markdown
## Purpose
[brief purpose]

## Requirements

### Requirement: Clear statement
The system SHALL ...

#### Scenario: Descriptive name
- **WHEN** ...
- **THEN** ...
```

Delta operations: `## ADDED Requirements`, `## MODIFIED Requirements`, `## REMOVED Requirements`, `## RENAMED Requirements`

## CLI Commands

```bash
npx openspec list                  # Active changes
npx openspec list --specs          # Capabilities
npx openspec show <item>           # Details
npx openspec validate <item> --strict --no-interactive
npx openspec archive <change> --yes
npx openspec new change "<name>" --schema full-stack
```

## Cursor Slash Commands

| Command | Purpose |
|---------|---------|
| `/opsx-propose` | Create change + all planning artifacts |
| `/opsx-apply` | Implement from tasks.md |
| `/opsx-archive` | Archive and merge specs |
| `/opsx-explore` | Think before committing to a change |
| `/opsx-sync` | Merge delta specs into main specs |

## Decision Tree

```
New request?
├─ Bug fix restoring spec behavior? → Fix directly
├─ Typo/format/comment? → Fix directly
├─ New feature/capability? → Create proposal (schema full-stack for entities)
├─ Breaking change? → Create proposal
└─ Unclear? → Create proposal (safer)
```

Remember: **Specs are truth. Changes are proposals. Keep them in sync.**
