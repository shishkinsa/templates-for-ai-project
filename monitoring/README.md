# Мониторинг (локальная разработка)

Стек observability для шаблона: **OpenTelemetry Collector** → **Prometheus** (метрики), **Loki** (логи), **Tempo** (трейсы), **Grafana** (визуализация).

Backend экспортирует телеметрию через `SP.Shared.Observability` (OTLP) — см. `OTEL_EXPORTER_OTLP_ENDPOINT`.

## Запуск

```powershell
# Из корня репозитория
docker compose -f docker-compose.monitoring.yml up -d
```

| Сервис | URL | Учётные данные |
|--------|-----|----------------|
| Grafana | http://localhost:3000 | admin / admin |
| Prometheus | http://localhost:9090 | — |
| OTLP gRPC | localhost:4317 | — |
| OTLP HTTP | localhost:4318 | — |

## Связь с приложением

**Backend в Docker** (`docker-compose.yml`) шлёт OTLP на `http://host.docker.internal:4317`. Перед запуском приложения поднимите monitoring-стек (порт 4317 на хосте).

```powershell
.\scripts\start-monitoring.ps1
docker compose up --build -d
```

**Backend локально** (`dotnet run`):

```powershell
$env:OTEL_EXPORTER_OTLP_ENDPOINT = "http://localhost:4317"
dotnet run --project "src/webapi/cnt_sp_webapi/6 WebApp/SP.WebApi.WebApp.csproj"
```

После нескольких запросов к API в Grafana: дашборд **Sample Project API** (`sp-api-overview`) или Explore → Tempo / Loki / Prometheus, фильтр по `service.name` = `SP.WebApi.WebApp`.

## Файлы

| Файл | Назначение |
|------|------------|
| `otel-collector-config.yml` | Приём OTLP, маршрутизация в Tempo/Loki/Prometheus |
| `prometheus.yml` | Scrape метрик с collector (`:8889`) |
| `loki-config.yml` | Хранение логов (filesystem, dev) |
| `tempo.yml` | Хранение трейсов (local, dev) |
| `grafana/provisioning/datasources/` | Автоподключение источников данных |
