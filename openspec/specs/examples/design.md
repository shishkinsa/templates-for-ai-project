# Examples — Technical Design

## Backend

- **Entity**: `ExampleItem` в `1 Entities/` с фабрикой `Create()`
- **Repository**: `IExampleItemRepository` → `ExampleItemRepository` (EF Core)
- **UseCases**: `Handlers/Example/` — Create, List, GetById, Update, Delete
- **API**: `ExamplesController` — GET/POST/PUT/DELETE
- **Persistence**: таблица `example_items`, миграция EF

## Frontend (FSD)

- `entities/example/model/types.ts` — типы из OpenAPI
- `entities/example/api/` — вызовы REST
- `entities/example/ui/ExampleTable.tsx` — таблица
- `features/example/create-item/` — форма создания
- `features/example/edit-item/` — модальное редактирование
- `features/example/delete-item/` — удаление с подтверждением
- `pages/home/` — подключение на главной

## Tests

- Unit: `ExampleItemTests` (домен)
- Integration: `ExamplesEndpointTests` (create + list, health)

## Traceability

| Requirement | OpenAPI | Backend | Frontend | Tests |
|-------------|---------|---------|----------|-------|
| Health Check | `/health` | `HealthController` | — | `GetHealth_ReturnsOk` |
| Create | POST `/examples` | `CreateExampleCommandHandler` | `CreateExampleForm` | `CreateAndListExample_Works` |
| List | GET `/examples` | `ListExamplesQueryHandler` | `ExampleTable` | `CreateAndListExample_Works` |
| Get By Id | GET `/examples/{id}` | `GetExampleByIdQueryHandler` | — | — |
| Update | PUT `/examples/{id}` | `UpdateExampleCommandHandler` | `EditExampleModal` | `UpdateAndDeleteExample_Works` |
| Delete | DELETE `/examples/{id}` | `DeleteExampleCommandHandler` | `DeleteExampleButton` | `UpdateAndDeleteExample_Works` |
| Display In SPA | — | — | `HomePage`, `ExampleTable` | — |
