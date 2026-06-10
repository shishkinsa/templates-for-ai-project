# Технологический стек

Свод основан на ADR в `docs/architecture/adr/`. При смене стека сначала обновляют архитектурные модели и требования, затем этот файл.

## Клиент

| Технология | Где | ADR |
| --- | --- | --- |
| React, TypeScript, Vite | `CNT_SP_Web` | [0002](../architecture/adr/0002-react-typescript-frontend.md) |
| Ant Design | UI-компоненты | TBD |
| React Router | Маршрутизация SPA | — |

## Сервер

| Технология | Где | ADR |
| --- | --- | --- |
| ASP.NET Core, .NET 10 | `CNT_SP_WebAPI` | [0001](../architecture/adr/0001-dotnet-aspnet-core-backend.md) |
| EF Core + Npgsql | Доступ к PostgreSQL | [0003](../architecture/adr/0003-use-postgres.md) |
| Requestum | CQRS-диспетчеризация | — |
| FluentValidation | Валидация команд/запросов | — |
| OpenTelemetry | Трейсы, метрики, логи (OTLP → Collector) | — |
| Prometheus / Loki / Tempo / Grafana | Локальный observability-стек (`monitoring/`) | — |

## Данные

| Технология | Назначение | ADR |
| --- | --- | --- |
| PostgreSQL | Основное хранилище | [0003](../architecture/adr/0003-use-postgres.md) |

## TBD

Добавьте строки по мере принятия ADR: кэш, брокер сообщений, object storage, identity provider и т.д.
