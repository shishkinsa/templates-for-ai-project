# Контейнеры (C4) — краткая сводка для AI

**Sample Project** — TBD: краткое описание системы.

Полная модель: [diagram/context/01-model.c4](../../architecture/diagram/context/01-model.c4). При изменении архитектуры — сначала LikeC4, затем эта таблица.

| Идентификатор | Тип | Технология | Назначение |
| --- | --- | --- | --- |
| CNT_SP_Web | Клиентское приложение | React, TypeScript, Vite, Ant Design | Web SPA: UI, вызовы REST API |
| CNT_SP_WebAPI | Сервис API | ASP.NET Core, .NET 10 | REST API: бизнес-логика, метаданные в PostgreSQL |
| CNT_SP_DB | База данных | PostgreSQL | Транзакционные данные приложения |

**Идентификаторы домена:** TBD — типы ключей (UUID, int, code), согласовать с OpenAPI и схемой БД.

См. также: [README.md](../../README.md), [manifest.yaml](../../../manifest.yaml).
