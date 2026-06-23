# Документация для AI

Каталог содержит контекст проекта и инструкции для Cursor / других AI-ассистентов.

| Файл | Назначение |
|------|------------|
| [../AGENTS.md](../../AGENTS.md) | Краткая входная точка для любого AI-агента |
| [../../openspec/project.md](../../openspec/project.md) | Контекст проекта для OpenSpec (стек, соглашения) |
| [../../openspec/AGENTS.md](../../openspec/AGENTS.md) | SDD-workflow: propose → apply → archive |
| [project-context.md](project-context.md) | Узлы C4, границы контейнеров, идентификаторы домена |
| [tech-stack.md](tech-stack.md) | Технологии и обоснование выбора (со ссылками на ADR) |
| [cursor-rules.md](cursor-rules.md) | Детальные инструкции по работе с документацией |
| [workflows/add-entity.md](workflows/add-entity.md) | Чеклист добавления новой сущности (OpenSpec + full stack) |
| [workflows/pilot-update-delete.md](workflows/pilot-update-delete.md) | **Pilot:** update/delete через OpenSpec |
| [snippets/good-examples.md](snippets/good-examples.md) | Примеры «хорошего» кода |
| [snippets/bad-examples.md](snippets/bad-examples.md) | Антипаттерны |

**Порядок обновления:** OpenSpec change → ADR → LikeC4 → OpenAPI → `project-context.md` / `tech-stack.md` → `.cursorrules` / `AGENTS.md` → archive.

**Slash-команды Cursor:** `/opsx-propose`, `/opsx-apply`, `/opsx-archive`, `/opsx-explore`, `/opsx-sync`
