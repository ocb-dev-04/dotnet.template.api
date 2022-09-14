using System.Security.Claims;

using Microsoft.Extensions.Primitives;

using Core.Models;
using Core.Helpers;
using SharedSources.Errors;
using SharedSources.Constants;

namespace API.Extensions;

/// <summary>
/// <see cref="AuthMiddleware"/> class
/// </summary>
public sealed class AuthMiddleware
{
    private readonly IHttpContextAccessor _contextAccesor;
    private readonly RequestDelegate _next;

    /// <summary>
    /// <see cref="AuthMiddleware"/> contructor
    /// </summary>
    /// <param name="contextAccesor"></param>
    /// <param name="next"></param>
    public AuthMiddleware(
        IHttpContextAccessor contextAccesor,
        RequestDelegate next)
    {
        _contextAccesor = contextAccesor;
        _next = next;
    }

    /// <summary>
    /// Method in self to apply auth middleware
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task Invoke(HttpContext context)
    {
        string path = context.Request.Path;

        string action = context.Request.RouteValues["action"].ToString();
        if (action.ToLower().Equals("login") || action.ToLower().Equals("signup"))
        {
            await _next(context);
            return;
        }

        ClaimsPrincipal user = _contextAccesor.HttpContext?.User;
        if (user.Identity.IsAuthenticated)
        {
            KeyValuePair<string, StringValues> authHeader = context.Request.Headers.FirstOrDefault(f => f.Key.Equals("Authorization"));
            if (authHeader.Key is null)
                CommonExceptionsHandler.NeedAuthenticate();

            Claim? emailClaim = user.Claims.FirstOrDefault(f => f.Type.Equals(ClaimTypes.Email));
            if(emailClaim is null)
                CommonExceptionsHandler.NeedAuthenticate();

            IJwtRepository _jwtStore = context.RequestServices.GetService(typeof(IJwtRepository)) as IJwtRepository;
            InCacheUser currentUser = await _jwtStore.GetUserAsync(emailClaim.Value);
            context.Items.Add(AppConstants.HttpCurrentUser, currentUser);

            await _next(context);
            return;
        }

        context.Response.ContentType = "application/json";
        string result = System.Text.Json.JsonSerializer.Serialize(new {});
        context.Response.StatusCode = 401;

        await context.Response.WriteAsync(result);
    }
}
