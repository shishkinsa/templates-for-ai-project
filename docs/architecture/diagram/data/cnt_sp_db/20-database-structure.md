# CNT_SP_DB: структура метаданных

База данных PostgreSQL для `CNT_SP_WebAPI`. Идентификаторы доменных сущностей — **UUID**.

## Таблица `example_items`

| Колонка | Тип | Ограничения | Описание |
|---------|-----|-------------|----------|
| `id` | `uuid` | PK | Идентификатор |
| `name` | `varchar(256)` | NOT NULL | Наименование |
| `created_at` | `timestamptz` | NOT NULL | Время создания (UTC) |

Миграция EF Core: `src/webapi/cnt_sp_webapi/5 Infrastructure.Implementation/SP.WebApi.DataAccess.Postgres/Migrations/`
