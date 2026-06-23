using Microsoft.AspNetCore.Authorization;

namespace SP.WebApi.WebApp.Authentication;

/// <summary>
/// Требование: аутентификация обязательна только при <c>Auth:Enabled=true</c>.
/// </summary>
public sealed class AuthWhenEnabledRequirement : IAuthorizationRequirement;
