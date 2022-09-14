using Core.DTOs;
using Core.Models;
using Core.Helpers;
using Core.Entities;

using SharedSources.Errors;

using Core.Interfaces;

namespace Core.CQRS;

/// <summary>
/// <see cref="IAuthCommand"/> implementation
/// </summary>
public sealed class AuthCommand : BaseHandler, IAuthCommand
{
    #region Props & Ctor

    private readonly JwtSettings _jwtSettings;
    private readonly IJwtRepository _jwtRepository;

    /// <summary>
    /// <see cref="AuthCommand"/> ctor
    /// </summary>
    /// <param name="unit"></param>
    /// <param name="jwtSettings"></param>
    /// <param name="jwtRepository"></param>
    /// <param name="credentialRepository"></param>
    /// <param name="userRepository"></param>
    public AuthCommand(
            IServicesBase unit,
            IGenericRepository<Credential, CredentialDTO> credentialRepository,
            IGenericRepository<User, UserDTO> userRepository,
            JwtSettings jwtSettings,
            IJwtRepository jwtRepository) 
                : base(unit, credentialRepository, userRepository)
    {
        ArgumentNullException.ThrowIfNull(jwtSettings, nameof(jwtSettings));
        ArgumentNullException.ThrowIfNull(jwtRepository, nameof(jwtRepository));

        _jwtSettings = jwtSettings;
        _jwtRepository = jwtRepository;
    }

    #endregion

    /// <inheritdoc/>
    public async Task<JwtModel> Login(AccessModelDTO login)
    {
        Credential credential = await _credentialRepository.GetEntityByCustomQuery(f => f.Email.Equals(login.Email));
        if (credential is null)
            CommonExceptionsHandler.CredentialsNotFound();

        bool passwordsMath = login.Password.HashMatch(credential.Password);
        if (!passwordsMath)
        {
            CommonExceptionsHandler.WrongPassword();
        }

        User data = await _userRepository.GetEntityByCreatorId(credential.Id);
        _ = await _jwtRepository.GetUserAsync(credential.Email);

        return credential.GenerateJwt(_jwtSettings);
    }

    /// <inheritdoc/>
    public async Task<JwtModel> Signup(AccessModelDTO signup)
    {
        Credential find = await _credentialRepository.GetEntityByCustomQuery(f => f.Email.Equals(signup.Email));
        if (find is not null)
            CommonExceptionsHandler.CredentialsAlreadyExist();

        Credential credential = new ();
        credential.Email = signup.Email;
        credential.Password = signup.Password.Hash();

        Credential created = await _credentialRepository.Create(credential);
        if (created is null)
            CommonExceptionsHandler.ErrorWhileProccess();

        _ = await _jwtRepository.GetUserAsync(credential.Email);
        
        return credential.GenerateJwt(_jwtSettings);
    }
}
