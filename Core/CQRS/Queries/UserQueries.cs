using Core.DTOs;
using Core.Models;
using Core.Helpers;
using Core.Entities;

using SharedSources.Errors;

using Core.Interfaces;

namespace Core.CQRS;

/// <summary>
/// <see cref="IUserQueries"/> implementation
/// </summary>
public sealed class UserQueries : BaseHandler, IUserQueries
{
    #region Ctor

    /// <summary>
    /// <see cref="UserQueries"/> ctor
    /// </summary>
    /// <param name="unit"></param>
    /// <param name="credentialRepository"></param>
    /// <param name="userRepository"></param>
    public UserQueries(
        IServicesBase unit,
        IGenericRepository<Credential, CredentialDTO> credentialRepository,
        IGenericRepository<User, UserDTO> userRepository) : base(unit, credentialRepository, userRepository)
    {
    }

    #endregion

    /// <inheritdoc/>
    public async Task<UserDTO> GetSelfUserData()
    {
        InCacheUser cacheUser = _base.CacheUser;
        User find = await _userRepository.GetEntityByCreatorId(cacheUser.Id);
        if (find is null)
            CommonExceptionsHandler.UserNotFound();

        return _base.Mapper.Map<UserDTO>(find);
    }

    /// <inheritdoc/>
    public async Task<UserDTO> GetById(int id)
    {
        UserDTO find = await _userRepository.GetDTOById(id);
        if (find is null)
            CommonExceptionsHandler.UserNotFound();

        return find;
    }
}
