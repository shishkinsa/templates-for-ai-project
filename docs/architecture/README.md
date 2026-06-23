# Архитектура

Модель SPDF: [README.md](../README.md) · [manifest.yaml](../../manifest.yaml)

| Документ | Описание |
|----------|----------|
| [planning/capacity.md](planning/capacity.md) | Расчёт CCU/RPS, хранение, HA |
| [specs/assemblies.md](specs/assemblies.md) | Именование сборок |
| [specs/backend.md](specs/backend.md) | Слои backend |
| [specs/frontend.md](specs/frontend.md) | FSD frontend |
| [adr/](adr/) | Architecture Decision Records |
| [diagram/](diagram/) | Диаграммы C4, данные, инфраструктура |
| [openapi/](openapi/) | REST-контракты |

## Следующие шаги

1. Заполнить [planning/capacity.md](planning/capacity.md) — расчёт нагрузки
2. Добавить контекстную диаграмму в `diagram/context/`
3. Описать контейнеры в `diagram/containers/cnt_sp_web/` и `cnt_sp_webapi/`
