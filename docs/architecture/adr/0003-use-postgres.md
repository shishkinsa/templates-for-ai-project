# ADR-0003: PostgreSQL как основное хранилище

## 📋 Метаданные

| Атрибут | Значение |
|---------|----------|
| **Статус** | Принято |
| **Дата** | TBD |
| **Автор** | TBD |
| **Версия** | 1.0 |

## 🎯 Контекст

Нужна транзакционная СУБД для метаданных приложения с ACID, миграциями и зрелой поддержкой в EF Core.

## ✅ Решение

**PostgreSQL** для `CNT_SP_DB`; доступ через **EF Core + Npgsql**.

## 📎 Последствия

- Именование объектов БД: [psql-naming-conventions.md](../../psql-naming-conventions.md)
- Миграции в `SP.WebApi.DataAccess.Postgres/Migrations/`
- Скрипт создания роли/БД: `scripts/create-cnt-db.sql`
