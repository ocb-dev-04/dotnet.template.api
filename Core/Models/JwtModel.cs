namespace Core.Models;

/// <summary>
/// <see cref="JwtModel"/> class
/// </summary>
public sealed class JwtModel
{
    /// <summary>
    /// JWT Token
    /// </summary>
    public string Token { get; set; }

    /// <summary>
    /// JWT expiration time
    /// </summary>
    public DateTimeOffset TokenExpiryTime { get; set; } = DateTimeOffset.UtcNow;
}
