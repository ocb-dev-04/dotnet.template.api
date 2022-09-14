using Microsoft.AspNetCore.Http;

using Core.Models;
using SharedSources.Constants;

namespace Core.Helpers;

/// <summary>
/// <see cref="HttpContext"/> extensions
/// </summary>
public static class HttpContextExtensions
{
    /// <summary>
    /// Return current user
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static InCacheUser? GetCurrentUser(this HttpContext context)
    {
        if (!context.Items.ContainsKey(AppConstants.HttpCurrentUser))
            return null;

        return context.Items[AppConstants.HttpCurrentUser] as InCacheUser;
    }

}
