# Участие в разработке

## Порядок изменений (docs-first)

1. Требование → `docs/requirements/`
2. Архитектура → ADR, LikeC4, OpenAPI
3. Код → `src/`
4. Тесты
5. Обновление `docs/ai/` при смене паттернов

## Перед Pull Request

```powershell
.\scripts\verify.ps1
```

Чеклист:

- [ ] Требования / OpenAPI синхронизированы с кодом
- [ ] `dotnet test` проходит
- [ ] `npm run lint` и `npm run build` проходят
- [ ] Новые публичные API отражены в `openapi.yaml`
- [ ] ADR добавлен при архитектурных решениях

## Ветки

См. [docs/git-flow.md](docs/git-flow.md): `feature/*`, `fix/*`, `docs/*`.

## Инициализация нового проекта из шаблона

```powershell
.\scripts\init-project.ps1 -ProjectName "My Project" -ProjectPrefix "MP" -ProjectSlug "my-project"
```

Dry-run:

```powershell
.\scripts\init-project.ps1 -ProjectName "My Project" -ProjectPrefix "MP" -ProjectSlug "my-project" -WhatIf
```
