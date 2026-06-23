# Project Context

## Purpose

**Sample Project** — шаблон full stack для AI-assisted разработки. Демонстрирует сквозной сценарий: OpenSpec specs → OpenAPI → Clean Architecture backend → FSD frontend → PostgreSQL.

Бизнес-цели: [docs/requirements/business/01-goals.md](../docs/requirements/business/01-goals.md)

## Tech Stack

| Слой | Технологии | Документ |
|------|------------|----------|
| Frontend | React, TypeScript, Vite, Ant Design, FSD | [docs/ai/tech-stack.md](../docs/ai/tech-stack.md) |
| Backend | ASP.NET Core, .NET 10, EF Core, Requestum, FluentValidation | [docs/architecture/specs/backend/11-backend-app-architecture.md](../docs/architecture/specs/backend/11-backend-app-architecture.md) |
| Data | PostgreSQL | [docs/architecture/adr/0003-use-postgres.md](../docs/architecture/adr/0003-use-postgres.md) |
| Observability | OpenTelemetry, Prometheus, Loki, Tempo, Grafana | [monitoring/README.md](../monitoring/README.md) |

## Project Conventions

### Code Style

- C#: [docs/c-charp-naming-conventions.md](../docs/c-charp-naming-conventions.md) + `src/.editorconfig`
- TypeScript / React: [docs/ts-naming-conventions.md](../docs/ts-naming-conventions.md), [docs/react-naming-conventions.md](../docs/react-naming-conventions.md)
- SQL: [docs/psql-naming-conventions.md](../docs/psql-naming-conventions.md)
- Общие принципы: [docs/coding-standards.md](../docs/coding-standards.md)

### Architecture Patterns

- **Backend**: Clean Architecture, CQRS через Requestum — см. [11-backend-app-architecture.md](../docs/architecture/specs/backend/11-backend-app-architecture.md)
- **Frontend**: Feature-Sliced Design — см. [12-frontend-app-architecture.md](../docs/architecture/specs/frontend/12-frontend-app-architecture.md)
- **C4 / контейнеры**: [docs/ai/project-context.md](../docs/ai/project-context.md), LikeC4 в `docs/architecture/diagram/`
- **Стратегические решения**: ADR в `docs/architecture/adr/` — не дублировать в capability specs

### Testing Strategy

- Unit-тесты для домена и UseCase handlers
- Integration-тесты для REST endpoints (`WebApplicationFactory`, InMemory БД)
- Проверка: `.\scripts\verify.ps1`

### Git Workflow

- [docs/git-flow.md](../docs/git-flow.md): `feature/*`, `fix/*`, `docs/*`

## Domain Context

- Контейнеры: `CNT_SP_Web` (SPA), `CNT_SP_WebAPI` (REST), `CNT_SP_DB` (PostgreSQL)
- Идентификаторы домена: TBD в [project-context.md](../docs/ai/project-context.md)
- Эталонная сущность: `ExampleItem` — capability `examples`

## Important Constraints

- Публичный REST-контракт: [docs/architecture/openapi/components/openapi.yaml](../docs/architecture/openapi/components/openapi.yaml) — синхронно с кодом
- Не противоречить принятым ADR без нового ADR
- Язык коммуникации с пользователем: **русский**

## External Dependencies

- PostgreSQL 16+ (локально или Docker)
- .NET SDK 10.x, Node.js 22+

## Artifact Roles (гибридная модель)

| Артефакт | Назначение |
|----------|------------|
| `openspec/specs/` | Канон поведения системы (WHAT) |
| `docs/requirements/business/` | Бизнес-цели и user stories (WHY) |
| `docs/requirements/non-functional/` | NFR: perf, security |
| `docs/architecture/adr/` | Стратегические архитектурные решения |
| `docs/architecture/openapi/` | REST-контракт (синхрон с capability API) |
| `openspec/changes/` | Предложения изменений (delta specs) |

## Reference Implementation

Сквозной пример `ExampleItem`:

- Spec: [openspec/specs/examples/spec.md](specs/examples/spec.md)
- Backend: `Handlers/Example/` → `ExamplesController.cs`
- Frontend: `entities/example` → `features/example/create-item` → `pages/home`
- Tests: `ExamplesEndpointTests`, `ExampleItemTests`
