# Контекст проекта: узлы диаграммы контейнеров

**Sample Project** — TBD: краткое описание системы и её назначения.

Ниже — стартовая сводка узлов уровня контейнеров (C4). Заполните таблицу под ваш домен; при изменении архитектуры обновляйте LikeC4-модель [`docs/architecture/diagram/context/01-model.c4`](../architecture/diagram/context/01-model.c4), затем этот документ.

| Идентификатор | Тип | Технология | Назначение |
| --- | --- | --- | --- |
| CNT_SP_Web | Клиентское приложение | React, TypeScript, Vite, Ant Design | Web SPA: UI, вызовы REST API |
| CNT_SP_WebAPI | Сервис API | ASP.NET Core, .NET 10 | REST API: бизнес-логика, метаданные в PostgreSQL |
| CNT_SP_DB | База данных | PostgreSQL | Транзакционные данные приложения |

**Идентификаторы домена:** TBD — зафиксируйте типы ключей (UUID, int, code) и согласуйте с OpenAPI и схемой БД.
