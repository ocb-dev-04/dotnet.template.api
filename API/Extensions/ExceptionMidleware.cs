using SharedSources.Extensions;
using SharedSources.Models;
using System.Net;

namespace API.Extensions;

/// <summary>
/// <see cref="ExceptionHandlerMiddleware"/> class
/// </summary>
public sealed class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (ArgumentException ex)
        {
            await HandleCodeException(context, ex.Message).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            await HandleServerException(context, ex.Message).ConfigureAwait(false);
        }
    }

    private static Task HandleCodeException(HttpContext context, string fullMessage)
    {
        string message = fullMessage.Split(",").Last();
        int statusCode = int.Parse(fullMessage.Split(",").First());

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        string response = new StandarResponseModel<string>().SetError(message.Trim()).Serialize();
        return context.Response.WriteAsync(response);
    }

    private static Task HandleServerException(HttpContext context, string message)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = 500;

        string response = new StandarResponseModel<string>().SetError(string.Empty).Serialize();
        return context.Response.WriteAsync(response);
    }
}
