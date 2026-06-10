# Workflow: добавление новой сущности

Чеклист для AI и разработчика. Эталон — `ExampleItem`.

## 1. Требования

- [ ] Создать FR по [docs/requirements/template.md](../../requirements/template.md)
- [ ] Критерии приёмки в Gherkin
- [ ] Связать с бизнес-целями при необходимости

## 2. Контракт

- [ ] Добавить paths/schemas в [docs/architecture/openapi/components/openapi.yaml](../../architecture/openapi/components/openapi.yaml)
- [ ] Согласовать типы id (UUID / int / code) с [docs/ai/project-context.md](../project-context.md)

## 3. Backend

- [ ] `1 Entities` — доменная сущность с фабрикой `Create()`
- [ ] `2 Infrastructure.Interfaces` — `I{Name}Repository`
- [ ] `5 Infrastructure.Implementation` — репозиторий + EF-конфигурация
- [ ] `3 UseCases` — Command/Query + Handler + Validator + Dto + Mappings
- [ ] `6 WebApp` — Controller (тонкий, Requestum)
- [ ] Миграция EF: `.\scripts\ef-migrate.ps1 -Action add -MigrationName Add{Name}`
- [ ] Unit-тест handler + integration test endpoint

## 4. Frontend (FSD)

- [ ] `entities/{name}/model/types.ts` — типы из OpenAPI
- [ ] `entities/{name}/api/` — функции API
- [ ] `entities/{name}/ui/` — таблица / карточка
- [ ] `features/{name}/` — сценарии (create, edit, …)
- [ ] `pages/` — подключить на страницу
- [ ] TSDoc для экспортируемых API и компонентов

## 5. Документация

- [ ] `docs/architecture/diagram/data/` — схема БД при новых таблицах
- [ ] ADR — при новой технологии или контейнере
- [ ] `docs/ai/snippets/good-examples.md` — добавить эталонный фрагмент

## 6. Проверка

```powershell
.\scripts\verify.ps1
dotnet test
```
