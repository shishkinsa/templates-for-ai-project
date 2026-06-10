# Требования к системе

| Каталог | Тип | Шаблон |
|---------|-----|--------|
| [business/](business/) | Бизнес-требования (BR) | [template.md](template.md) |
| [functional/](functional/) | Функциональные (FR) | [template.md](template.md) |
| [non-functional/](non-functional/) | Нефункциональные (NFR) | [template.md](template.md) |

## Стартовые документы

В шаблоне уже есть эталоны:

| Файл | Назначение |
|------|------------|
| [business/01-goals.md](business/01-goals.md) | Цели (заготовка) |
| [business/02-user-stories.md](business/02-user-stories.md) | Истории для `ExampleItem` |
| [functional/01-core-api.md](functional/01-core-api.md) | FR для REST `/examples` |
| [non-functional/01-performance.md](non-functional/01-performance.md) | NFR производительности |
| [non-functional/02-security.md](non-functional/02-security.md) | NFR безопасности |

Именование файлов: `{номер}-{kebab-case-описание}.md`.

## Матрица трассировки (эталон)

| Требование | OpenAPI | Backend | Frontend | Тесты |
|------------|---------|---------|----------|-------|
| [FR-001](functional/01-core-api.md) | `/examples`, `/examples/{id}` | `ExamplesController`, `Handlers/Example/` | `entities/example`, `features/example/create-item` | `ExamplesEndpointTests`, `ExampleItemTests` |
