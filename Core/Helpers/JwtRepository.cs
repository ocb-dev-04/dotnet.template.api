using System.Text;

using Newtonsoft.Json;

using Core.Models;
using Core.Entities;
using Core.Interfaces;
using Core.DTOs;

namespace Core.Helpers;

/// <summary>
/// Handle jwt to store in memory, also set to use in middleware
/// </summary>
public sealed class JwtRepository : IJwtRepository
{
    #region Props & Ctor

    private readonly IEncryptDecrypt _encryptDecrypt;
    protected readonly IGenericRepository<Credential, CredentialDTO> _credentialRepository;
    protected readonly IGenericRepository<User, UserDTO> _userRepository;

    /// <summary>
    /// <see cref="JwtRepository"/> contructor
    /// </summary>
    /// <param name="redis"></param>
    public JwtRepository(
        IEncryptDecrypt encryptDecrypt,
        IGenericRepository<Credential, CredentialDTO> credentialRepository,
        IGenericRepository<User, UserDTO> userRepository)
    {
        ArgumentNullException.ThrowIfNull(encryptDecrypt, nameof(encryptDecrypt));
        ArgumentNullException.ThrowIfNull(credentialRepository, nameof(credentialRepository));
        ArgumentNullException.ThrowIfNull(userRepository, nameof(userRepository));

        _encryptDecrypt = encryptDecrypt;
        _credentialRepository = credentialRepository;
        _userRepository = userRepository;
    }

    #endregion

    /// <inheritdoc/>
    public async Task<InCacheUser?> GetUserAsync(string email)
    {
        string cacheId = email.Split("@").First();

        Credential user = await _credentialRepository.GetEntityByCustomQuery(f => f.Email.Equals(email));
        User account = await _userRepository.GetEntityByCreatorId(user.Id);

        InCacheUser cachedUser = new ();
        cachedUser.Id = user.Id;
        cachedUser.Email = user.Email;
        cachedUser.RefreshDate = DateTime.UtcNow.AddDays(1);

        // serialize data
        var cachedDataString = JsonConvert.SerializeObject(cachedUser);
        string encrypt = _encryptDecrypt.Encrypt(cachedDataString);
        var newDataToCache = Encoding.UTF8.GetBytes(encrypt);

        return cachedUser;
    }
}

/// <summary>
/// <see cref="IJwtRepository"/> contracts
/// </summary>
public interface IJwtRepository
{
    /// <summary>
    /// Get user access data by email and set into cache
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    Task<InCacheUser> GetUserAsync(string email);
}
