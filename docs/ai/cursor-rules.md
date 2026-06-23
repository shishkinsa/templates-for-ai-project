# Инструкции для AI Cursor

См. также [AGENTS.md](../../AGENTS.md) — краткая входная точка для агентов.

Cursor hooks (`/.cursor/hooks.json`) в шаблоне не используются. Автоматизация — через scoped rules в `.cursor/rules/`, OpenSpec slash-команды и `scripts/verify.ps1`.

## OpenSpec (spec-driven development)

| Команда | Назначение |
|---------|------------|
| `/opsx-propose` | Новый change + артефакты (proposal, design, tasks, delta specs) |
| `/opsx-apply` | Реализация по tasks.md |
| `/opsx-archive` | Архив + merge delta в `openspec/specs/` |
| `/opsx-explore` | Обсуждение идеи до commit |
| `/opsx-sync` | Merge delta specs |

Детали: [openspec/AGENTS.md](../../openspec/AGENTS.md), контекст: [openspec/project.md](../../openspec/project.md).

Для новых сущностей: schema `full-stack` — [openspec/schemas/full-stack/](../../openspec/schemas/full-stack/).

```powershell
npx openspec list --specs          # capabilities
npx openspec validate <id> --strict --no-interactive
```

## Как использовать документацию

### 1. Всегда начинай с контекста

```yaml
before_any_task:
  - "Определи текущий контекст (backend / frontend / architecture / infra)"
  - "Прочитай openspec/project.md и docs/ai/project-context.md"
  - "Проверь openspec/specs/ и активные changes: npx openspec list"
```

### 2. Требования и поведение

```yaml
requirements:
  - "Канон поведения: openspec/specs/<capability>/spec.md"
  - "Новые фичи: OpenSpec change (delta specs) → OpenAPI → код"
  - "Бизнес-цели: docs/requirements/business/"
  - "NFR: docs/requirements/non-functional/"
```

### 3. Архитектура

```yaml
architecture:
  - "Backend-слои: docs/architecture/specs/backend/11-backend-app-architecture.md"
  - "Frontend FSD: docs/architecture/specs/frontend/12-frontend-app-architecture.md"
  - "REST-контракт: docs/architecture/openapi/components/openapi.yaml"
  - "ADR: docs/architecture/adr/"
```

### 4. Стандарты кода

- C#: `docs/c-charp-naming-conventions.md` + `src/.editorconfig`
- TypeScript / React: `docs/ts-naming-conventions.md`, `docs/react-naming-conventions.md`
- SQL: `docs/psql-naming-conventions.md`
- Git: `docs/git-flow.md`

### 5. Scoped rules

- Backend: `.cursor/rules/backend-csharp.mdc`
- Frontend: `.cursor/rules/frontend-fsd.mdc`, `.cursor/rules/frontend-tsdoc.mdc`
- Документация: `.cursor/rules/docs-first.mdc`

### 6. Workflow

- Новая сущность: [workflows/add-entity.md](workflows/add-entity.md)
- Антипаттерны: [snippets/bad-examples.md](snippets/bad-examples.md)
