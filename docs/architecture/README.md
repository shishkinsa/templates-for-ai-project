# Архитектура

Модель SPDF: [FRAMEWORK.md](../FRAMEWORK.md) · [manifest.yaml](../manifest.yaml)

| Документ | Описание |
|----------|----------|
| [capacity.md](capacity.md) | Расчёт CCU/RPS, хранение, HA |
| [specs/assemblies.md](specs/assemblies.md) | Именование сборок |
| [specs/backend/11-backend-app-architecture.md](specs/backend/11-backend-app-architecture.md) | Слои backend |
| [specs/frontend/12-frontend-app-architecture.md](specs/frontend/12-frontend-app-architecture.md) | FSD frontend |
| [adr/](adr/) | Architecture Decision Records |
| [diagram/](diagram/) | Диаграммы C4, данные, инфраструктура |
| [openapi/](openapi/) | REST-контракты |

## Следующие шаги

1. Заполнить [capacity.md](capacity.md) — расчёт нагрузки
2. Добавить контекстную диаграмму в `diagram/context/`
3. Описать контейнеры в `diagram/containers/cnt_sp_web/` и `cnt_sp_webapi/`
