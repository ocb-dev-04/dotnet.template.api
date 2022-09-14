using Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities;

/// <summary>
/// <see cref="User"/> entity model
/// </summary>
public sealed class User : AuditableEntity
{
    /// <summary>
    /// The user name
    /// </summary>
    [Required, MaxLength(100)]
    public string Name { get; set; }

    /// <summary>
    /// Last name
    /// </summary>

    [Required, MaxLength(100)]
    public string LastName { get; set; }

    /// <summary>
    /// User name
    /// </summary>
    [Required, MaxLength(100)]
    public string UserName { get; set; }

    /// <summary>
    /// User active status (Active, Inactive, Paused, etc...)
    /// </summary>
    [Required]
    public UserStatus Active { get; set; } = UserStatus.Active;

    /// <summary>
    /// Role assigned to user  
    /// </summary>
    [Required, MaxLength(50)]
    public string Role { get; set; }

    /// <summary>
    /// Foreing Key to credetials
    /// </summary>
    [Required]
    public Credential Credential { get; set; }
}
