# ADR-0001: ASP.NET Core для backend

## 📋 Метаданные

| Атрибут | Значение |
|---------|----------|
| **Статус** | Принято |
| **Дата** | 2026-06-23 |
| **Автор** | Sample Project Team |
| **Версия** | 1.0 |

## 🎯 Контекст

### Проблема

Нужен единый серверный стек для REST API с поддержкой DI, middleware, OpenAPI и экосистемы .NET.

### Рассмотренные альтернативы

- **ASP.NET Core** — зрелая платформа, единый деплой, OpenTelemetry, EF Core
- **Node.js (NestJS)** — другой язык в команде, меньше переиспользования с существующими .NET-навыками

## ✅ Решение

Использовать **ASP.NET Core на .NET 10** для `CNT_SP_WebAPI` и будущих фоновых сервисов.

## 📎 Последствия

- Структура проекта по [11-backend-app-architecture.md](../specs/backend/11-backend-app-architecture.md)
- Централизованные версии пакетов в `src/Directory.Packages.props`
