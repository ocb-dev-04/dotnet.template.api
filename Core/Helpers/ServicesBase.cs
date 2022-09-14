using Microsoft.AspNetCore.Http;

using AutoMapper;

using Core.Models;
using SharedSources.Errors;

namespace Core.Helpers;

/// <summary>
/// <see cref="IServicesBase"/> implementation
/// </summary>
public sealed class ServicesBase : IServicesBase
{
    #region Props & Ctor

    protected readonly IHttpContextAccessor _httpContextAccessor;
    protected readonly IMapper _mapper;
    protected readonly InCacheUser _cacheUser;

    /// <summary>
    /// Public <see cref="ServicesBase"/> constructor
    /// </summary>
    /// <param name="unitOfWork"></param>
    /// <param name="mapper"></param>
    public ServicesBase(
        IHttpContextAccessor httpContextAccessor,
        IMapper mapper)
    {
        _httpContextAccessor = httpContextAccessor;
        _mapper = mapper;

        string path = httpContextAccessor.HttpContext.Request.Path;
        bool accessPath = path.ToLower().Contains("login") || path.ToLower().Contains("signup");
        if (!accessPath)
        {
            _cacheUser = _httpContextAccessor.HttpContext.GetCurrentUser();
            if (_cacheUser is null)
                CommonExceptionsHandler.NeedAuthenticate();
        }
    }

    #endregion

    public IHttpContextAccessor HttpContextAccessor => _httpContextAccessor;

    public IMapper Mapper => _mapper;

    public InCacheUser CacheUser => _cacheUser;
}

/// <summary>
/// <see cref="IServicesBase"/> contracts
/// </summary>
public interface IServicesBase
{
    public IHttpContextAccessor HttpContextAccessor { get; }
    public IMapper Mapper { get; }
    public InCacheUser CacheUser { get; }
}
