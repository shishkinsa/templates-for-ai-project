# Инструкции для AI-агентов

**Sample Project** — шаблон full stack: React/TypeScript (FSD) + ASP.NET Core / .NET 10 (Clean Architecture) + PostgreSQL.

<!-- OPENSPEC:START -->
## OpenSpec (изменения поведения)

Для новых фич, breaking changes и архитектурных сдвигов используй **spec-driven workflow**:

1. Прочитай [openspec/project.md](openspec/project.md) и [openspec/AGENTS.md](openspec/AGENTS.md)
2. Создай change: `/opsx-propose <описание>` или `openspec new change "<id>"`
3. Для full stack сущностей: schema `full-stack` — см. [openspec/schemas/full-stack/](openspec/schemas/full-stack/)
4. Валидируй: `npx openspec validate <change-id> --strict --no-interactive`
5. Реализуй: `/opsx-apply` после approve proposal
6. Архивируй: `/opsx-archive` — merge delta в `openspec/specs/`

**Канон поведения:** `openspec/specs/<capability>/spec.md`  
**Бизнес-цели:** `docs/requirements/business/`  
**REST-контракт:** `docs/architecture/openapi/components/openapi.yaml`  
**Стратегия:** ADR в `docs/architecture/adr/`

Keep this managed block so `openspec update` can refresh the instructions.
<!-- OPENSPEC:END -->

## Роль

Инженер по разработке системы. Соблюдай границы контейнеров C4. Не смешивай назначения узлов без обновления архитектуры.

## Порядок работы (docs-first + OpenSpec)

1. Определи слой: `frontend` / `backend` / `architecture` / `infra`
2. Прочитай [docs/ai/project-context.md](docs/ai/project-context.md) и [openspec/project.md](openspec/project.md)
3. Проверь capability specs: `npx openspec list --specs` и [docs/requirements/](docs/requirements/)
4. Сверься с ADR в [docs/architecture/adr/](docs/architecture/adr/)
5. **Новая фича:** OpenSpec change → OpenAPI/ADR → код → тесты → archive
6. **Bugfix / typo:** правка без change proposal (если восстанавливает spec behavior)

## Ключевые документы

| Документ | Назначение |
|----------|------------|
| [openspec/project.md](openspec/project.md) | Контекст проекта для OpenSpec |
| [openspec/AGENTS.md](openspec/AGENTS.md) | Детальный SDD-workflow |
| [docs/ai/tech-stack.md](docs/ai/tech-stack.md) | Стек и ADR-ссылки |
| [docs/ai/cursor-rules.md](docs/ai/cursor-rules.md) | Инструкции Cursor |
| [docs/ai/workflows/add-entity.md](docs/ai/workflows/add-entity.md) | Чеклист новой сущности (full stack) |
| [docs/ai/snippets/good-examples.md](docs/ai/snippets/good-examples.md) | Эталонный код |
| [docs/ai/snippets/bad-examples.md](docs/ai/snippets/bad-examples.md) | Антипаттерны |
| [docs/architecture/openapi/components/openapi.yaml](docs/architecture/openapi/components/openapi.yaml) | Канон REST |

## Эталонная фича

Сквозной пример `ExampleItem` — capability `examples`:

- Spec: [openspec/specs/examples/spec.md](openspec/specs/examples/spec.md)
- Backend: `Handlers/Example/` → `ExamplesController.cs`
- Frontend: `entities/example` → `features/example/create-item` → `pages/home`
- Тесты: `SP.WebApi.Tests/Integration/ExamplesEndpointTests.cs`

## Правила

- Не меняй публичный API без синхронного обновления OpenAPI и delta specs
- Не добавляй зависимости без необходимости
- Новые UseCases сопровождай тестами
- Язык коммуникации: русский

## Проверка

```powershell
.\scripts\verify.ps1
```

Slash-команды Cursor: `/opsx-propose`, `/opsx-apply`, `/opsx-archive`, `/opsx-explore`, `/opsx-sync`
