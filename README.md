# Шаблон проекта для AI-assisted разработки

Универсальная стартовая структура для новых проектов: документация-first, Clean Architecture backend, FSD frontend, интеграция с Cursor AI.

## Требования к окружению

| Инструмент | Версия | Назначение |
|------------|--------|------------|
| [.NET SDK](https://dotnet.microsoft.com/download) | 10.x | Backend WebAPI |
| [Node.js](https://nodejs.org/) | 22+ | Frontend (Vite), OpenSpec CLI |
| [PostgreSQL](https://www.postgresql.org/) | 16+ | База данных (опционально на старте) |
| [Docker](https://www.docker.com/) | актуальная | Сборка и запуск в контейнерах |
| PowerShell | 5.1+ / 7+ | Скрипт `init-project.ps1` |

---

## Быстрый старт

### 1. Создать проект из шаблона

```powershell
git clone https://github.com/shishkinsa/templates-for-ai-project.git my-new-project
cd my-new-project

.\scripts\init-project.ps1 -ProjectName "My Project" -ProjectPrefix "MP" -ProjectSlug "my-project"
```

| Параметр | Пример | Описание |
|----------|--------|----------|
| `ProjectName` | `My Project` | Человекочитаемое имя |
| `ProjectPrefix` | `MP` | Префикс сборок (`MP.WebApi.*`) |
| `ProjectSlug` | `my-project` | Slug для docker, БД, solution |

После инициализации префикс `SP` (Sample Project) заменяется на ваш.

### 2. Запуск локально

**Backend** (сборка и health-check работают без PostgreSQL; EF Core подключается, если задана строка подключения):

```powershell
dotnet run --project "src/webapi/cnt_mp_webapi/6 WebApp/MP.WebApi.WebApp.csproj"
```

Проверка: `http://localhost:5025/swagger` и `http://localhost:5025/api/v1/health`

**Frontend**:

```powershell
cd src/frontend/cnt_mp_web
npm install
npm run dev
```

Проверка: `http://localhost:5173` (страница «Главная» запрашивает backend по `/api`)

**Docker Compose** (PostgreSQL + backend + frontend):

```powershell
docker compose up --build
.\scripts\ef-migrate.ps1
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

Миграции: `src/webapi/cnt_sp_webapi/5 Infrastructure.Implementation/SP.WebApi.DataAccess.Postgres/Migrations/`

---

## Что уже есть в шаблоне

| Область | Содержимое |
|---------|------------|
| Документация | ADR (0001–0008), FR/NFR-эталоны, OpenAPI v0.4, схема БД |
| Backend | CQRS (Requestum): CRUD `ExampleItem`, read-only `Category`, auth skeleton |
| Frontend | FSD: `entities/{example,category}`, `features/example/*`, vitest |
| Тесты | Unit (домен, validators) + integration (CRUD, 400/404, categories) |
| OpenSpec | Capabilities: `examples`, `categories`, `auth` |
| AI | `AGENTS.md`, skills `/opsx-*`, [bootstrap-project.md](docs/ai/workflows/bootstrap-project.md) |
| Observability | OTEL → Prometheus/Loki/Tempo, Grafana dashboard API |
| DevOps | CI + CD (`.github/workflows/`), Dependabot, `verify.ps1`, auto-migrate в Docker |
| Диаграммы | LikeC4: context, containers, components WebAPI |

### Эталонные capability

| Capability | Тип | Путь |
|------------|-----|------|
| `examples` | CRUD | [openspec/specs/examples/spec.md](openspec/specs/examples/spec.md) |
| `categories` | read-only | [openspec/specs/categories/spec.md](openspec/specs/categories/spec.md) |
| `auth` | skeleton | [openspec/specs/auth/spec.md](openspec/specs/auth/spec.md) |

### ExampleItem (сквозной CRUD)

| Слой | Путь |
|------|------|
| OpenAPI | `docs/architecture/openapi/components/openapi.yaml` |
| UseCases | `Handlers/Example/` |
| API | `Controllers/ExamplesController.cs` |
| UI | `entities/example` → `features/example/*` → `pages/home` |

## Проверка перед коммитом

```powershell
.\scripts\verify.ps1
```

Подробнее: [CONTRIBUTING.md](CONTRIBUTING.md).

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
| `/src` | Исходный код (frontend, webapi, lib) |
| `/openspec` | OpenSpec: capability specs, changes, archive, schema `full-stack` |
| `/docs/ai` | Контекст и инструкции для AI |
| `/docs/requirements` | Бизнес-, функциональные и NFR-требования |
| `/docs` (стандарты) | Именование, coding-standards, git-flow — см. [docs/README.md](docs/README.md) |
| `/docs/architecture` | ADR, диаграммы, OpenAPI, спецификации, расчёт нагрузки |
| `/monitoring` | OTEL, Prometheus, Loki, Tempo, Grafana (локальная разработка) |
| `/scripts` | `init-project.ps1`, `ef-migrate.ps1`, `start-monitoring.ps1`, SQL для БД |
| `AGENTS.md` | Входная точка для AI-агентов |
| `.cursorrules` | Краткие правила для Cursor AI |
| `.cursor/rules/` | Scoped rules: backend, frontend, docs-first, TSDoc |

### Backend (Clean Architecture)

```
src/webapi/cnt_{prefix}_webapi/
├── 0 Utils/
├── 1 Entities/
├── 2 Infrastructure.Interfaces/
├── 3 UseCases/           # целевой слой CQRS (Requestum)
├── 5 Infrastructure.Implementation/
├── 6 WebApp/
└── 7 Tests/
```

Подробнее: [docs/architecture/specs/backend/11-backend-app-architecture.md](docs/architecture/specs/backend/11-backend-app-architecture.md)

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

Подробнее: [docs/architecture/specs/frontend/12-frontend-app-architecture.md](docs/architecture/specs/frontend/12-frontend-app-architecture.md)

---

## Порядок работы (docs-first + OpenSpec)

1. **OpenSpec change** → `/opsx-propose` или `npx openspec new change "<id>" --schema full-stack`
2. **Delta specs** → `openspec/changes/<id>/specs/` → validate
3. **Архитектура** → ADR + LikeC4 + OpenAPI (синхронно с delta)
4. **Код** → `src/` по `tasks.md`
5. **Archive** → `/opsx-archive` — merge в `openspec/specs/`
6. **AI-контекст** → `docs/FRAMEWORK.md`, `docs/manifest.yaml`, `openspec/project.md`

Подробнее: [openspec/AGENTS.md](openspec/AGENTS.md), [docs/ai/workflows/add-entity.md](docs/ai/workflows/add-entity.md), [bootstrap проекта](docs/ai/workflows/bootstrap-project.md)

### OpenSpec CLI

```powershell
npm install                    # @fission-ai/openspec в devDependencies
npx openspec list --specs      # capabilities
npx openspec validate examples --type spec --strict --no-interactive
```

Slash-команды Cursor (после restart IDE): `/opsx-propose`, `/opsx-apply`, `/opsx-archive`

---

## Что настроить в новом проекте

- [ ] Запустить `scripts/init-project.ps1`
- [ ] `npm install` — OpenSpec CLI
- [ ] Заполнить `openspec/project.md`, `docs/ai/context/containers.md`, `docs/requirements/business/goals.md`
- [ ] Заполнить `docs/architecture/capacity.md`
- [ ] Добавить бизнес-требования в `docs/requirements/business/`
- [ ] Принять ключевые ADR в `docs/architecture/adr/`
- [ ] Обновить OpenAPI в `docs/architecture/openapi/components/`
- [x] CI настроен (`.github/workflows/ci.yml`) — адаптируйте под свой remote

---

## Лицензия

Apache License 2.0 — см. [LICENSE](LICENSE).
