# ADR-0007: FluentValidation для входных DTO

## 📋 Метаданные

| Атрибут | Значение |
|---------|----------|
| **Статус** | Принято |
| **Дата** | 2026-06-23 |
| **Автор** | Sample Project Team |
| **Версия** | 1.0 |

## 🎯 Контекст

Валидация REST-запросов должна быть декларативной и тестируемой отдельно от домена.

## ✅ Решение

Использовать **FluentValidation** в `Validators/` рядом с командами UseCases.

## 📎 Последствия

- Ошибки → `ValidationException` → `ApiExceptionHandler` (400 Problem Details)
- Unit-тесты валидаторов в `SP.WebApi.Tests/Validators/`
- Доменные инварианты остаются в Entities (`Create()`, `Rename()`)
