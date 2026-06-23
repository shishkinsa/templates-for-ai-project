# Pilot: update/delete для ExampleItem

Сквозной пример OpenSpec workflow «1 → n»: расширение capability `examples` от create/list/get до полного CRUD.

Архив change: [openspec/changes/archive/2026-06-23-add-example-update-delete/](../../openspec/changes/archive/2026-06-23-add-example-update-delete/)

## Что демонстрирует pilot

| Этап | Артефакт | Путь |
|------|----------|------|
| 1. Proposal | Why / What / Impact | `proposal.md` |
| 2. Design | Технические решения | `design.md` |
| 3. Delta specs | ADDED Requirements | `specs/examples/spec.md` |
| 4. Tasks | Full stack чеклист | `tasks.md` (schema `full-stack`) |
| 5. Implement | Код + тесты | см. ниже |
| 6. Archive | Merge в канон | `openspec/specs/examples/spec.md` |

## Команды (как повторить)

```powershell
# 1. Создать change
npx openspec new change "add-example-update-delete" --schema full-stack

# 2. Заполнить proposal.md, design.md, specs/examples/spec.md, tasks.md
npx openspec validate add-example-update-delete --strict --no-interactive

# 3. Реализовать по tasks.md
.\scripts\verify.ps1

# 4. Archive
npx openspec archive add-example-update-delete --yes
```

В Cursor: `/opsx-propose add-example-update-delete` → `/opsx-apply` → `/opsx-archive`

## Реализованный код (reference)

### Backend

| Компонент | Путь |
|-----------|------|
| Domain | `ExampleItem.Rename()` |
| Repository | `UpdateAsync`, `DeleteAsync` |
| Commands | `UpdateExampleCommand`, `DeleteExampleCommand` |
| API | `PUT/DELETE /api/v1/examples/{id}` |

### Frontend (FSD)

| Слой | Путь |
|------|------|
| API | `entities/example/api` — `updateExample`, `deleteExample` |
| Features | `features/example/edit-item`, `features/example/delete-item` |
| Page | `pages/home` — wiring + `EditExampleModal` |

### Tests

- Unit: `ExampleItemTests.Rename_*`
- Integration: `ExamplesEndpointTests.UpdateAndDeleteExample_Works`

## Матрица трассировки

| Requirement | OpenAPI | Backend | Frontend | Tests |
|-------------|---------|---------|----------|-------|
| Update Example Item | PUT `/examples/{id}` | `UpdateExampleCommandHandler` | `EditExampleModal` | `UpdateAndDeleteExample_Works` |
| Delete Example Item | DELETE `/examples/{id}` | `DeleteExampleCommandHandler` | `DeleteExampleButton` | `UpdateAndDeleteExample_Works` |
| Edit/Delete In SPA | — | — | `HomePage`, `ExampleTable` | — |

## Следующий change

Скопируйте этот pilot для новой сущности: `/opsx-propose add-{entity} --schema full-stack` и [add-entity.md](add-entity.md).
