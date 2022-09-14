using Core.DTOs;
using Core.Entities;
using Core.Helpers;
using Core.Interfaces;

namespace Core.CQRS;

/// <summary>
/// <see cref="BaseHandler"/> class
/// </summary>
public class BaseHandler
{
    protected readonly IServicesBase _base;
    protected readonly IGenericRepository<Credential, CredentialDTO> _credentialRepository;
    protected readonly IGenericRepository<User, UserDTO> _userRepository;

    /// <summary>
    /// <see cref="BaseHandler"/> ctor
    /// </summary>
    /// <param name="unit"></param>
    /// <param name="credentialRepository"></param>
    /// <param name="userRepository"></param>
    public BaseHandler(
        IServicesBase unit,
        IGenericRepository<Credential, CredentialDTO> credentialRepository,
        IGenericRepository<User, UserDTO> userRepository)
    {
        ArgumentNullException.ThrowIfNull(unit, nameof(unit));
        ArgumentNullException.ThrowIfNull(credentialRepository, nameof(credentialRepository));
        ArgumentNullException.ThrowIfNull(userRepository, nameof(userRepository));

        _base = unit;
        _credentialRepository = credentialRepository;
        _userRepository = userRepository;
    }
}
