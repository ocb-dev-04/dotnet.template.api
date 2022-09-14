using Core.DTOs;

namespace Core.Interfaces;

/// <summary>
/// <see cref="IUserQueries"/> contract
/// </summary>
public interface IUserQueries
{
    /// <summary>
    /// Get user by jwt
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<UserDTO> GetSelfUserData();

    /// <summary>
    /// Get user by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<UserDTO> GetById(int id);
}
