# Инструкции для AI-агентов

**Sample Project** — шаблон full stack: React/TypeScript (FSD) + ASP.NET Core / .NET 10 (Clean Architecture) + PostgreSQL.

## Роль

Инженер по разработке системы. Соблюдай границы контейнеров C4. Не смешивай назначения узлов без обновления архитектуры.

## Порядок работы (docs-first)

1. Определи слой: `frontend` / `backend` / `architecture` / `infra`
2. Прочитай [docs/ai/project-context.md](docs/ai/project-context.md)
3. Проверь требования в [docs/requirements/](docs/requirements/)
4. Сверься с ADR в [docs/architecture/adr/](docs/architecture/adr/)
5. Изменения: **требования → архитектура (ADR, LikeC4, OpenAPI) → код → тесты**

## Ключевые документы

| Документ | Назначение |
|----------|------------|
| [docs/ai/tech-stack.md](docs/ai/tech-stack.md) | Стек и ADR-ссылки |
| [docs/ai/cursor-rules.md](docs/ai/cursor-rules.md) | Детальные инструкции |
| [docs/ai/workflows/add-entity.md](docs/ai/workflows/add-entity.md) | Чеклист новой сущности |
| [docs/ai/snippets/good-examples.md](docs/ai/snippets/good-examples.md) | Эталонный код |
| [docs/ai/snippets/bad-examples.md](docs/ai/snippets/bad-examples.md) | Антипаттерны |
| [docs/architecture/openapi/components/openapi.yaml](docs/architecture/openapi/components/openapi.yaml) | Канон REST |

## Эталонная фича

Сквозной пример `ExampleItem` — копируй паттерн:

- Backend: `Handlers/Example/` → `ExamplesController.cs`
- Frontend: `entities/example` → `features/example/create-item` → `pages/home`
- Тесты: `SP.WebApi.Tests/Integration/ExamplesEndpointTests.cs`

## Правила

- Не меняй публичный API без синхронного обновления OpenAPI
- Не добавляй зависимости без необходимости
- Новые UseCases сопровождай тестами
- Язык коммуникации: русский

## Проверка

```powershell
.\scripts\verify.ps1
```
