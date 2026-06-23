# ADR-0004: Ant Design для UI

## 📋 Метаданные

| Атрибут | Значение |
|---------|----------|
| **Статус** | Принято |
| **Дата** | 2026-06-23 |
| **Автор** | Sample Project Team |
| **Версия** | 1.0 |

## 🎯 Контекст

Нужна зрелая React UI-библиотека с таблицами, формами и модальными окнами для enterprise SPA.

## ✅ Решение

Использовать **Ant Design** в контейнере `CNT_SP_Web`.

## 📎 Последствия

- Компоненты UI в `entities/*/ui`, `features/*/ui`
- Тема и переопределения: `src/app/styles/ant-overrides.css`
- См. [12-frontend-app-architecture.md](../specs/frontend/12-frontend-app-architecture.md)
