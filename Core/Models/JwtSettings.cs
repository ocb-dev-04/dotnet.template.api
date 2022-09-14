namespace Core.Models;

/// <summary>
/// <see cref="JwtSettings"/> appSettings.json model
/// </summary>
public sealed class JwtSettings
{
    /// <summary>
    /// Issuer signing validation key
    /// </summary>
    public bool ValidateIssuerSigningKey { get; set; }

    /// <summary>
    /// Issuer signing key
    /// </summary>
    public string IssuerSigningKey { get; set; }

    /// <summary>
    /// Signing key validation key
    /// </summary>
    public bool ValidateIssuer { get; set; } = true;

    /// <summary>
    /// Valid issuer
    /// </summary>
    public string ValidIssuer { get; set; }

    /// <summary>
    /// Audience validation key
    /// </summary>
    public bool ValidateAudience { get; set; } = true;

    /// <summary>
    /// Valid audience
    /// </summary>
    public string ValidAudience { get; set; }

    /// <summary>
    /// Expiration time flag
    /// </summary>
    public bool RequireExpirationTime { get; set; }

    /// <summary>
    /// Lifetime validation flag
    /// </summary>
    public bool ValidateLifetime { get; set; } = true;
}