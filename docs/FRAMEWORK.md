# SPDF — Sample Project Documentation Framework

Единая модель документации шаблона. **Машиночитаемый индекс:** [manifest.yaml](manifest.yaml).

## Для AI: с чего начать

1. [AGENTS.md](../AGENTS.md) — правила и OpenSpec
2. [manifest.yaml](manifest.yaml) — карта артефактов
3. По задаче — слой из таблицы ниже

**При расхождении приоритет:** `openspec/specs` → OpenAPI → ADR → `requirements/constraints`.

## Слои

| Вопрос | Канон | Путь |
|--------|-------|------|
| **Что** делает система? | OpenSpec Requirement + Scenario | `openspec/specs/<capability>/spec.md` |
| **Зачем**? | Бизнес-контекст | `requirements/business/` |
| **Сколько / насколько безопасно?** | Ограничения | `requirements/constraints/` |
| **Как устроен код (слои)?** | Layer specs | `architecture/specs/backend|frontend/` |
| **Какой REST-контракт?** | OpenAPI | `architecture/openapi/components/openapi.yaml` |
| **Стратегические решения** | ADR | `architecture/adr/` |
| **Топология C4** | LikeC4 | `architecture/diagram/` |
| **Контейнеры (кратко)** | Таблица для AI | `ai/context/containers.md` |
| **Где в коде** | Design + manifest | `openspec/specs/<cap>/design.md` |
| **Как работать** | Workflows | `ai/workflows/` |
| **Стиль кода** | Standards | `docs/coding-standards.md`, `*-naming-conventions.md` |

## Не дублировать

| Не создавать | Вместо этого |
|--------------|--------------|
| `requirements/functional/*.md` с Gherkin (устарело) | `openspec/specs/` |
| Отдельный `tech-stack.md` (устарело) | ADR + § Стек ниже |
| Полная копия C4 в markdown | `ai/context/containers.md` + LikeC4 |
| Матрица трассировки в 3 местах | `manifest.yaml` + `design.md` |

## Lifecycle фичи

```text
Идея → /opsx-propose → delta spec → OpenAPI (если API) → ADR/LikeC4 (если нужно)
     → код + тесты → scenario-coverage.txt → verify.ps1 → /opsx-archive
     → обновить manifest.yaml (новая capability)
```

Подробнее: [ai/workflows/change-lifecycle.md](ai/workflows/change-lifecycle.md).

## Стек (индекс ADR)

| Область | ADR |
|---------|-----|
| Backend | [0001](architecture/adr/0001-dotnet-aspnet-core-backend.md) |
| Frontend | [0002](architecture/adr/0002-react-typescript-frontend.md) |
| PostgreSQL | [0003](architecture/adr/0003-use-postgres.md) |
| Ant Design | [0004](architecture/adr/0004-ant-design-ui.md) |
| Requestum CQRS | [0005](architecture/adr/0005-requestum-cqrs.md) |
| OpenTelemetry | [0006](architecture/adr/0006-opentelemetry-observability.md) |
| FluentValidation | [0007](architecture/adr/0007-fluentvalidation.md) |
| Auth skeleton | [0008](architecture/adr/0008-jwt-authentication-skeleton.md) |

## Capability (эталон)

| ID | Тип | Spec |
|----|-----|------|
| `examples` | CRUD | [openspec/specs/examples/spec.md](../openspec/specs/examples/spec.md) |
| `categories` | read-only | [openspec/specs/categories/spec.md](../openspec/specs/categories/spec.md) |
| `auth` | skeleton | [openspec/specs/auth/spec.md](../openspec/specs/auth/spec.md) |

## Проверка

```powershell
.\scripts\verify.ps1          # OpenSpec + tests + manifest + spec coverage
npx likec4 validate docs/architecture/diagram
```
