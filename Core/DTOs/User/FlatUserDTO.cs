namespace Core.DTOs;

/// <summary>
/// <see cref="FlatUserDTO"/> model
/// </summary>
public sealed class FlatUserDTO : FullBaseDTO
{
    /// <summary>
    /// The user name
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Last name
    /// </summary>

    public string LastName { get; set; }

    /// <summary>
    /// User name
    /// </summary>
    public string UserName { get; set; }
}
