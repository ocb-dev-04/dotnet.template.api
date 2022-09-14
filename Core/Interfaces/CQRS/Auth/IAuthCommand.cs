using Core.DTOs;
using Core.Models;

namespace Core.Interfaces;

/// <summary>
/// <see cref="IAuthCommand"/> contracts
/// </summary>
public interface IAuthCommand
{
    /// <summary>
    /// Login with credentials
    /// </summary>
    /// <param name="login"></param>
    /// <returns></returns>
    Task<JwtModel> Login(AccessModelDTO login);

    /// <summary>
    /// Signup with credentials
    /// </summary>
    /// <param name="signup"></param>
    /// <returns></returns>
    Task<JwtModel> Signup(AccessModelDTO signup);
}
