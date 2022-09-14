using Core.DTOs;

namespace Core.Interfaces;

/// <summary>
/// <see cref="IUserCommand"/> contracts
/// </summary>
public interface IUserCommand
{
    /// <summary>
    /// Create new user
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    Task<UserDTO> Create(CreateUserDTO create);

    /// <summary>
    /// Update user data
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    Task<UserDTO> UpdateGeneralData(UpdateUserGeneralDataDTO data);

    /// <summary>
    /// Update user role
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    Task<UserDTO> UpdateRole(UpdateUserRoleDTO data);

    /// <summary>
    /// Remove user
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task Delete(int id);
}