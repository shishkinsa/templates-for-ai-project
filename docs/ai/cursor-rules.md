# Инструкции для AI Cursor

См. также [AGENTS.md](../../AGENTS.md) — краткая входная точка для агентов.

Cursor hooks (`/.cursor/hooks.json`) в шаблоне не используются. Автоматизация — через scoped rules в `.cursor/rules/` и `scripts/verify.ps1`.

## Как использовать документацию

### 1. Всегда начинай с контекста

```yaml
before_any_task:
  - "Определи текущий контекст (backend / frontend / architecture / infra)"
  - "Найди соответствующие документы в /docs"
```

### 2. Требования должны быть актуальны

```yaml
requirements:
  - "Оформляй требования по docs/requirements/template.md"
  - "Изменения: сначала требования → архитектура → код"
```

### 3. Архитектура

```yaml
architecture:
  - "Backend-слои: docs/architecture/specs/backend/11-backend-app-architecture.md"
  - "Frontend FSD: docs/architecture/specs/frontend/12-frontend-app-architecture.md"
  - "REST-контракт: docs/architecture/openapi/components/openapi.yaml"
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
