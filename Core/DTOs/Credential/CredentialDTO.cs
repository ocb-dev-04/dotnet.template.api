namespace Core.DTOs;

/// <summary>
/// <see cref="CredentialDTO"/> model to transfer data from controller to handlers
/// </summary>
public sealed class CredentialDTO : BaseDTO
{
    public string Email { get; set; }
}
