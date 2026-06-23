# Инструкции для AI-агентов

**Sample Project** — шаблон full stack: React/TypeScript (FSD) + ASP.NET Core / .NET 10 + PostgreSQL.

**Карта документации:** [docs/FRAMEWORK.md](docs/FRAMEWORK.md) · [docs/manifest.yaml](docs/manifest.yaml)

<!-- OPENSPEC:START -->
## OpenSpec (изменения поведения)

1. Прочитай [openspec/project.md](openspec/project.md) и [openspec/AGENTS.md](openspec/AGENTS.md)
2. Создай change: `/opsx-propose <описание>` или `openspec new change "<id>"`
3. Full stack: schema `full-stack` — [openspec/schemas/full-stack/](openspec/schemas/full-stack/)
4. Валидируй: `npx openspec validate <change-id> --strict --no-interactive`
5. Реализуй: `/opsx-apply` после approve
6. Архивируй: `/opsx-archive` → merge в `openspec/specs/`

**Канон поведения:** `openspec/specs/<capability>/spec.md`

Keep this managed block so `openspec update` can refresh the instructions.
<!-- OPENSPEC:END -->

## Порядок работы

1. Слой: `frontend` / `backend` / `architecture` / `infra`
2. [docs/manifest.yaml](docs/manifest.yaml) → нужные артефакты capability
3. [docs/ai/context/containers.md](docs/ai/context/containers.md) + [openspec/project.md](openspec/project.md)
4. ADR: [docs/architecture/adr/](docs/architecture/adr/)
5. **Новая фича:** change → OpenAPI/ADR → код → тесты → archive → manifest
6. **Bugfix:** без change, если восстанавливает spec

## Ключевые документы

| Документ | Назначение |
|----------|------------|
| [docs/FRAMEWORK.md](docs/FRAMEWORK.md) | Модель SPDF, слои, lifecycle |
| [docs/manifest.yaml](docs/manifest.yaml) | Индекс capability и путей |
| [openspec/AGENTS.md](openspec/AGENTS.md) | SDD-workflow |
| [docs/ai/workflows/add-entity.md](docs/ai/workflows/add-entity.md) | Новая сущность |
| [docs/ai/workflows/change-lifecycle.md](docs/ai/workflows/change-lifecycle.md) | Lifecycle change |
| [docs/architecture/openapi/components/openapi.yaml](docs/architecture/openapi/components/openapi.yaml) | REST |

## Эталонные capability

| ID | Spec | Код |
|----|------|-----|
| `examples` | [spec.md](openspec/specs/examples/spec.md) | `Handlers/Example/`, `entities/example` |
| `categories` | [spec.md](openspec/specs/categories/spec.md) | `Handlers/Category/`, `entities/category` |
| `auth` | [spec.md](openspec/specs/auth/spec.md) | `Authentication/*`, `shared/auth` |

## Правила

- API ↔ OpenAPI ↔ spec синхронно
- Не добавляй зависимости без необходимости
- UseCases + критичная логика — с тестами
- Язык: русский

## Проверка

```powershell
.\scripts\verify.ps1
```

Slash-команды: `/opsx-propose`, `/opsx-apply`, `/opsx-archive`, `/opsx-explore`, `/opsx-sync`
