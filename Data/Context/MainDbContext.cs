using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

using Core.Models;
using Core.Helpers;
using Core.Entities;

using SharedSources.Errors;

namespace Data.Context;

/// <summary>
/// Main DB context
/// </summary>
public sealed class MainDbContext : DbContext
{
    #region Props

    private readonly IHttpContextAccessor _httpContextAccessor;

    #endregion

    #region Ctor

    /// <summary>
    /// <see cref="MainDbContext"/> ctor
    /// </summary>
    /// <param name="options"></param>
    public MainDbContext(
        DbContextOptions<MainDbContext> options,
        IHttpContextAccessor httpContextAccessor) : base(options)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    #endregion

    #region DbSets

    /// <summary>
    /// <see cref="Credential"/> entity table as model
    /// </summary>
    public DbSet<Credential> Credentials { get; set; }

    /// <summary>
    /// <see cref="User"/> entity table as model
    /// </summary>
    public DbSet<User> Users { get; set; }

    #endregion

    #region SaveChanges override

    /// <summary>
    /// Override save changes to set and update <see cref="AuditableEntity"/> props
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        InCacheUser? cacheUser = _httpContextAccessor.HttpContext.GetCurrentUser();

        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:

                    if (entry.Entity is not Credential)
                        entry.Entity.CreatedBy = cacheUser.Id;
                    else
                        entry.Entity.CreatedBy = entry.Entity.Id;

                    break;

                case EntityState.Modified:
                    if (entry.Entity is not Credential)
                        UserIsCreator(entry.Entity, cacheUser);

                    break;

                case EntityState.Deleted:
                    UserIsCreator(entry.Entity, cacheUser);
                    break;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Method to validate if http context current user id is equal to creator user id
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cacheUser"></param>
    /// <exception cref="ArgumentNullException"></exception>
    private void UserIsCreator(AuditableEntity entity, InCacheUser? cacheUser)
    {
        if (cacheUser is null || entity.CreatedBy != cacheUser.Id)
        {
            throw new ArgumentNullException(ExceptionsTranslated.notFound);
        }
    }

    #endregion
}
