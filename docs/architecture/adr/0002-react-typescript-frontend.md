# ADR-0002: React и TypeScript для frontend

## 📋 Метаданные

| Атрибут | Значение |
|---------|----------|
| **Статус** | Принято |
| **Дата** | TBD |
| **Автор** | TBD |
| **Версия** | 1.0 |

## 🎯 Контекст

Нужен типизированный SPA с предсказуемой структурой масштабирования и богатой экосистемой UI.

## ✅ Решение

**React + TypeScript + Vite** в проекте `cnt_sp_web`, организация по **Feature-Sliced Design**.

UI-библиотека: **Ant Design** (TBD: оформить отдельным ADR при необходимости).

## 📎 Последствия

- Структура слоёв: [12-frontend-app-architecture.md](../specs/frontend/12-frontend-app-architecture.md)
- Алиас импортов `@/` → `src/`
