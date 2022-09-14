namespace Core.Models;

/// <summary>
/// Model to manage user in HttpContext
/// </summary>
public sealed class InCacheUser
{
    /// <summary>
    /// Credential id
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Credential email
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Date to refresh data
    /// </summary>
    public DateTimeOffset RefreshDate { get; set; }
}
