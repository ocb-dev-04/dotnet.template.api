using System.ComponentModel.DataAnnotations;

namespace Core.Entities;

/// <summary>
/// <see cref="Credential"/> entity model
/// </summary>
public sealed class Credential : AuditableEntity
{
    /// <summary>
    /// Email
    /// </summary>
    [Required]
    [EmailAddress]
    [MaxLength(100)]
    public string Email { get; set; }

    /// <summary>
    /// Password
    /// </summary>
    [Required]
    [MaxLength(400)]
    public string Password { get; set; }
}
