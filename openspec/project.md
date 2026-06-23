# Project Context

## Purpose

**Sample Project** — шаблон full stack для AI-assisted разработки. Демонстрирует сквозной сценарий: OpenSpec specs → OpenAPI → Clean Architecture backend → FSD frontend → PostgreSQL.

Бизнес-цели: [docs/requirements/business/goals.md](../docs/requirements/business/goals.md)

SPDF: [docs/FRAMEWORK.md](../docs/FRAMEWORK.md) · [docs/manifest.yaml](../docs/manifest.yaml)

## Tech Stack

| Слой | Технологии | Документ |
|------|------------|----------|
| Frontend | React, TypeScript, Vite, Ant Design, FSD | [docs/FRAMEWORK.md](../docs/FRAMEWORK.md) |
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
- **C4 / контейнеры**: [docs/ai/context/containers.md](../docs/ai/context/containers.md), LikeC4 в `docs/architecture/diagram/`
- **Стратегические решения**: ADR в `docs/architecture/adr/` — не дублировать в capability specs

### Testing Strategy

- Unit-тесты для домена и UseCase handlers
- Integration-тесты для REST endpoints (`WebApplicationFactory`, InMemory БД)
- Проверка: `.\scripts\verify.ps1`

### Git Workflow

- [docs/git-flow.md](../docs/git-flow.md): `feature/*`, `fix/*`, `docs/*`

## Domain Context

- Контейнеры: `CNT_SP_Web` (SPA), `CNT_SP_WebAPI` (REST), `CNT_SP_DB` (PostgreSQL)
- Идентификаторы домена: TBD в [containers.md](../docs/ai/context/containers.md)
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
| `docs/requirements/constraints/` | Ограничения: perf, security |
| `docs/architecture/adr/` | Стратегические архитектурные решения |
| `docs/architecture/openapi/` | REST-контракт (синхрон с capability API) |
| `openspec/changes/` | Предложения изменений (delta specs) |

## Reference Implementation

| Capability | Spec | Backend | Frontend | Tests |
|------------|------|---------|----------|-------|
| `examples` (CRUD) | [spec.md](specs/examples/spec.md) | `Handlers/Example/` | `entities/example`, `features/example/*` | `ExamplesEndpointTests`, validators |
| `categories` (read-only) | [spec.md](specs/categories/spec.md) | `Handlers/Category/` | `entities/category` | `CategoriesEndpointTests` |
| `auth` (skeleton) | [spec.md](specs/auth/spec.md) | `Authentication/*` | `shared/auth` | — |

Старт из шаблона: [docs/ai/workflows/bootstrap-project.md](../docs/ai/workflows/bootstrap-project.md)
