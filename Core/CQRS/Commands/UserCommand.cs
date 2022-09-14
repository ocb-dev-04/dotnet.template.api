using Core.DTOs;
using Core.Models;
using Core.Helpers;
using Core.Entities;

using SharedSources.Errors;

using Core.Interfaces;

namespace Core.CQRS;

/// <summary>
/// <see cref="IUserCommand"/> implementation
/// </summary>
public sealed class UserCommand : BaseHandler, IUserCommand
{
    #region Props & Ctor

    /// <summary>
    /// <see cref="UserCommand"/> ctor
    /// </summary>
    /// <param name="unit"></param>
    /// <param name="credentialRepository"></param>
    /// <param name="userRepository"></param>
    public UserCommand(
        IServicesBase unit,
        IGenericRepository<Credential, CredentialDTO> credentialRepository,
        IGenericRepository<User, UserDTO> userRepository) : base(unit, credentialRepository, userRepository)
    {
    }

    #endregion

    /// <inheritdoc/>
    public async Task<UserDTO> Create(CreateUserDTO data)
    {
        InCacheUser cacheUser = _base.CacheUser;

        Credential credentialUser = await _credentialRepository.GetEntityById(cacheUser.Id);
        if (credentialUser is null)
            CommonExceptionsHandler.CredentialsNotFound();

        User exist = await _userRepository.GetEntityByCreatorId(cacheUser.Id);
        if (exist is not null)
            CommonExceptionsHandler.UserAlreadyExist();

        User newAccount = _base.Mapper.Map<User>(data);
        newAccount.Credential = credentialUser;

        User created = await _userRepository.Create(newAccount);
        if (created is null)
            CommonExceptionsHandler.ErrorWhileProccess();

        return _base.Mapper.Map<UserDTO>(created);
    }

    /// <inheritdoc/>
    public async Task<UserDTO> UpdateGeneralData(UpdateUserGeneralDataDTO data)
    {
        InCacheUser currentUser = _base.CacheUser;
        User find = await _userRepository.GetEntityByCreatorId(currentUser.Id);
        if (find is null)
            CommonExceptionsHandler.UserNotFound();

        find.Name = data.Name;
        find.LastName = data.LastName;
        find.UserName = data.UserName;

        await _userRepository.Update(find);
        return _base.Mapper.Map<UserDTO>(find);
    }

    /// <inheritdoc/>
    public async Task<UserDTO> UpdateRole(UpdateUserRoleDTO data)
    {
        InCacheUser currentUser = _base.CacheUser;
        User find = await _userRepository.GetEntityByCreatorId(currentUser.Id);
        if (find is null)
            CommonExceptionsHandler.UserNotFound();

        find.Role = data.Role;

        await _userRepository.Update(find);
        return _base.Mapper.Map<UserDTO>(find);
    }

    /// <inheritdoc/>
    public async Task Delete(int id)
        => await _userRepository.Delete(id);
}
