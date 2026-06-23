# Шаблон проекта для AI-assisted разработки

Универсальная стартовая структура для новых full stack проектов: **docs-first (SPDF)** + **OpenSpec** + Clean Architecture backend + FSD frontend + интеграция с Cursor AI.

**Карта документации:** [docs/README.md](docs/README.md) · [manifest.yaml](manifest.yaml) · [AGENTS.md](AGENTS.md) · [openspec/project.md](openspec/project.md)

| Слой | Назначение | Путь |
|------|------------|------|
| Поведение (WHAT) | OpenSpec specs + scenarios | [openspec/specs/](openspec/specs/) |
| Намерение (WHY) | Бизнес-цели, capability | [docs/requirements/](docs/requirements/) |
| Ограничения | Perf, security | [docs/requirements/constraints/](docs/requirements/constraints/) |
| Структура (HOW) | ADR, OpenAPI, C4, layer specs | [docs/architecture/](docs/architecture/) |
| Процесс | Workflows, контекст для AI | [docs/process/](docs/process/) |
| Стандарты | Именование, стиль, Git | [docs/standards/](docs/standards/) |

**При расхождении приоритет:** `openspec/specs` → OpenAPI → ADR → `requirements/constraints`.

---

## Требования к окружению

| Инструмент | Версия | Назначение |
|------------|--------|------------|
| [.NET SDK](https://dotnet.microsoft.com/download) | 10.x | Backend WebAPI |
| [Node.js](https://nodejs.org/) | 22+ | Frontend (Vite), OpenSpec CLI |
| [PostgreSQL](https://www.postgresql.org/) | 16+ | База данных (опционально на старте) |
| [Docker](https://www.docker.com/) | актуальная | Сборка и запуск в контейнерах |
| PowerShell | 5.1+ / 7+ | Скрипты `scripts/*.ps1` |

---

## Быстрый старт

### 1. Создать проект из шаблона

```powershell
git clone https://github.com/shishkinsa/templates-for-ai-project.git my-new-project
cd my-new-project

npm install   # OpenSpec CLI (@fission-ai/openspec)

.\scripts\init-project.ps1 -ProjectName "My Project" -ProjectPrefix "MP" -ProjectSlug "my-project"
```

| Параметр | Пример | Описание |
|----------|--------|----------|
| `ProjectName` | `My Project` | Человекочитаемое имя |
| `ProjectPrefix` | `MP` | Префикс сборок (`MP.WebApi.*`) |
| `ProjectSlug` | `my-project` | Slug для docker, БД, solution |

Dry-run без изменений файлов:

```powershell
.\scripts\init-project.ps1 -ProjectName "My Project" -ProjectPrefix "MP" -ProjectSlug "my-project" -WhatIf
```

После инициализации префикс `SP` (Sample Project) заменяется на ваш. До `init-project.ps1` в путях ниже используйте `cnt_sp_*` и `SP.WebApi.*`.

Полный чеклист старта: [docs/process/workflows/bootstrap-project.md](docs/process/workflows/bootstrap-project.md).

### 2. Запуск локально

**Backend** (health-check работает без PostgreSQL; EF Core подключается при заданной строке подключения):

```powershell
# до init-project.ps1 (префикс SP)
dotnet run --project "src/webapi/cnt_sp_webapi/6 WebApp/SP.WebApi.WebApp.csproj"

# после init с префиксом MP
dotnet run --project "src/webapi/cnt_mp_webapi/6 WebApp/MP.WebApi.WebApp.csproj"
```

Проверка: `http://localhost:5025/swagger` и `http://localhost:5025/api/v1/health`

**Frontend:**

```powershell
cd src/frontend/cnt_sp_web   # или cnt_mp_web после init
npm install
npm run dev
```

Проверка: `http://localhost:5173` (страница «Главная» запрашивает backend по `/api`)

**Docker Compose** (PostgreSQL + backend + frontend; миграции применяются автоматически — `Database__AutoMigrate=true`):

```powershell
docker compose up --build
```

### 3. Мониторинг (опционально)

Стек OTLP → Prometheus / Loki / Tempo → Grafana. Подробнее: [monitoring/README.md](monitoring/README.md).

```powershell
.\scripts\start-monitoring.ps1
# или: docker compose -f docker-compose.monitoring.yml up -d
```

| Сервис | URL |
|--------|-----|
| Grafana | http://localhost:3000 (admin / admin) |
| Prometheus | http://localhost:9090 |
| OTLP | localhost:4317 |

Backend в Docker уже настроен на `OTEL_EXPORTER_OTLP_ENDPOINT=http://host.docker.internal:4317`. Для локального `dotnet run`:

```powershell
$env:OTEL_EXPORTER_OTLP_ENDPOINT = "http://localhost:4317"
```

### 4. База данных

Локально без Docker:

```powershell
psql -U postgres -f scripts/create-cnt-db.sql
.\scripts\ef-migrate.ps1
```

Миграции EF: `src/webapi/cnt_sp_webapi/5 Infrastructure.Implementation/SP.WebApi.DataAccess.Postgres/Migrations/`

---

## Что уже есть в шаблоне

| Область | Содержимое |
|---------|------------|
| Документация (SPDF) | ADR 0001–0008, business/constraints, OpenAPI, LikeC4, [manifest.yaml](manifest.yaml) |
| Backend | CQRS (Requestum): CRUD `ExampleItem`, read-only `Category`, auth skeleton |
| Frontend | FSD: `entities/{example,category}`, `features/example/*`, Vitest |
| Тесты | Unit (домен, validators) + integration (CRUD, 400/404, categories) |
| OpenSpec | Capabilities: `examples`, `categories`, `auth`; schema `full-stack` |
| AI | [AGENTS.md](AGENTS.md), `.cursorrules`, skills `/opsx-*`, [add-entity.md](docs/process/workflows/add-entity.md) |
| Observability | OTEL → Prometheus/Loki/Tempo, Grafana dashboard API |
| DevOps | `verify.ps1`, Docker Compose, шаблон `.github/workflows/` (CI/CD) |
| Диаграммы | LikeC4: context, containers, components WebAPI |

### Эталонные capability

| Capability | Тип | Spec |
|------------|-----|------|
| `examples` | CRUD | [openspec/specs/examples/spec.md](openspec/specs/examples/spec.md) |
| `categories` | read-only | [openspec/specs/categories/spec.md](openspec/specs/categories/spec.md) |
| `auth` | skeleton | [openspec/specs/auth/spec.md](openspec/specs/auth/spec.md) |

### ExampleItem (сквозной CRUD)

| Слой | Путь |
|------|------|
| OpenSpec | `openspec/specs/examples/spec.md` |
| OpenAPI | `docs/architecture/openapi/components/openapi.yaml` |
| UseCases | `src/webapi/.../Handlers/Example/` |
| API | `Controllers/ExamplesController.cs` |
| UI | `entities/example` → `features/example/*` → `pages/home` |

---

## Проверка перед коммитом

```powershell
npm install
.\scripts\verify.ps1
```

`verify.ps1` запускает: OpenSpec validate → backend tests → manifest check → frontend lint/test/build.

Подробнее: [CONTRIBUTING.md](CONTRIBUTING.md).

---

## Чего пока нет

| Область | Статус |
|---------|--------|
| Полноценная аутентификация (OIDC/IdP) | Только skeleton (`Auth:Enabled`, DevBearer) |
| E2E / Playwright | Нет |
| OpenAPI → TypeScript codegen в CI | Скрипт `npm run generate:api` (ручной запуск) |
| Production deploy | CD workflow — шаблон сборки образов |

---

## Структура проекта

| Папка | Назначение |
|-------|------------|
| `src/` | Исходный код (frontend, webapi, lib) |
| `openspec/` | Specs, changes, archive, schema `full-stack` |
| `manifest.yaml` | Машиночитаемый индекс артефактов (SPDF) |
| `docs/process/` | Workflows и контекст для AI |
| `docs/requirements/` | Бизнес-контекст и ограничения |
| `docs/standards/` | Именование, coding-standards, git-flow |
| `docs/architecture/` | ADR, диаграммы, OpenAPI, specs слоёв |
| `monitoring/` | OTEL, Prometheus, Loki, Tempo, Grafana |
| `scripts/` | `init-project.ps1`, `ef-migrate.ps1`, `verify.ps1`, SQL |
| `AGENTS.md` | Входная точка для AI-агентов |
| `.cursorrules` | Краткие правила для Cursor |
| `.cursor/rules/` | Scoped rules: backend, frontend, docs-first, TSDoc |
| `.cursor/skills/` | OpenSpec skills: propose, apply, archive, … |

### Backend (Clean Architecture)

```
src/webapi/cnt_{prefix}_webapi/
├── 0 Utils/
├── 1 Entities/
├── 2 Infrastructure.Interfaces/
├── 3 UseCases/           # CQRS (Requestum)
├── 5 Infrastructure.Implementation/
├── 6 WebApp/
└── 7 Tests/
```

Подробнее: [docs/architecture/specs/backend.md](docs/architecture/specs/backend.md)

### Frontend (Feature-Sliced Design)

```
src/frontend/cnt_{prefix}_web/src/
├── app/       # Точка входа, роутер, провайдеры
├── pages/     # Страницы
├── widgets/   # Крупные блоки UI
├── features/  # Пользовательские сценарии
├── entities/  # Бизнес-сущности UI
└── shared/    # API-клиент, UI-kit, конфиг
```

Подробнее: [docs/architecture/specs/frontend.md](docs/architecture/specs/frontend.md)

---

## Порядок работы (docs-first + OpenSpec)

```text
Идея → /opsx-propose → delta spec → OpenAPI → ADR/LikeC4 (если нужно)
     → код + тесты → verify.ps1 → /opsx-archive → manifest.yaml
```

1. **OpenSpec change** — `/opsx-propose` или `npx openspec new change "<id>" --schema full-stack`
2. **Delta specs** — `openspec/changes/<id>/specs/` → validate
3. **Архитектура** — ADR + LikeC4 + OpenAPI (синхронно с delta)
4. **Код** — `src/` по `tasks.md`
5. **Archive** — `/opsx-archive` → merge в `openspec/specs/`

Подробнее: [AGENTS.md](AGENTS.md) · [change-lifecycle.md](docs/process/workflows/change-lifecycle.md) · [add-entity.md](docs/process/workflows/add-entity.md)

### OpenSpec CLI

```powershell
npx openspec list --specs
npx openspec validate examples --type spec --strict --no-interactive
npx likec4 validate docs/architecture/diagram
```

Slash-команды Cursor: `/opsx-propose`, `/opsx-apply`, `/opsx-archive`, `/opsx-explore`, `/opsx-sync`

---

## Что настроить в новом проекте

- [ ] `.\scripts\init-project.ps1` + `npm install`
- [ ] [openspec/project.md](openspec/project.md), [containers.md](docs/process/context/containers.md), [goals.md](docs/requirements/business/goals.md)
- [ ] [capacity.md](docs/architecture/planning/capacity.md)
- [ ] Бизнес-контекст в [docs/requirements/business/](docs/requirements/business/)
- [ ] Ключевые ADR в [docs/architecture/adr/](docs/architecture/adr/)
- [ ] OpenAPI в [docs/architecture/openapi/components/](docs/architecture/openapi/components/)
- [ ] CI/CD в `.github/workflows/` — адаптировать под свой remote

---

## Лицензия

Apache License 2.0 — см. [LICENSE](LICENSE).
