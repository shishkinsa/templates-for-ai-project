# Требования к системе

| Каталог | Тип | Назначение |
|---------|-----|------------|
| [business/](business/) | BR | Бизнес-цели, user stories (WHY) |
| [functional/](functional/) | FR (view) | Ссылки на OpenSpec specs для трассировки |
| [non-functional/](non-functional/) | NFR | Производительность, безопасность |

**Канон поведения системы:** [openspec/specs/](../../openspec/specs/) — capability specs с Requirement + Scenario.

Новые фичи оформляются через OpenSpec change (`/opsx-propose`), не через отдельные FR-файлы.

## Стартовые документы

| Файл | Назначение |
|------|------------|
| [business/01-goals.md](business/01-goals.md) | Цели (заготовка) |
| [business/02-user-stories.md](business/02-user-stories.md) | Истории для `ExampleItem` |
| [functional/01-core-api.md](functional/01-core-api.md) | View FR-001 → [openspec/specs/examples/spec.md](../../openspec/specs/examples/spec.md) |
| [non-functional/01-performance.md](non-functional/01-performance.md) | NFR производительности |
| [non-functional/02-security.md](non-functional/02-security.md) | NFR безопасности |

## Матрица трассировки (эталон)

| Capability / FR | OpenSpec | OpenAPI | Backend | Frontend | Тесты |
|-----------------|----------|---------|---------|----------|-------|
| `examples` / [FR-001](functional/01-core-api.md) | [spec.md](../../openspec/specs/examples/spec.md) | `/examples` CRUD | `Handlers/Example/` | `entities/example`, `features/example/*` | `ExamplesEndpointTests`, `ExampleItemTests` |

Pilot (update/delete): [docs/ai/workflows/pilot-update-delete.md](../ai/workflows/pilot-update-delete.md)

## OpenSpec workflow

1. `/opsx-propose add-{entity}` — schema `full-stack` для новых сущностей
2. Реализация по `tasks.md` — см. [docs/ai/workflows/add-entity.md](../ai/workflows/add-entity.md)
3. `/opsx-archive` — merge delta в `openspec/specs/`
