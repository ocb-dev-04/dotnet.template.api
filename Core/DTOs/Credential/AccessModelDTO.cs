namespace Core.DTOs;

/// <summary>
/// <see cref="AccessModelDTO"/> -> model to transfer data from controller to handlers
/// </summary>
public sealed class AccessModelDTO
{
    /// <summary>
    /// Email
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Password
    /// </summary>
    public string Password { get; set; }
}
