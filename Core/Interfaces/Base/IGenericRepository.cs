using System.Linq.Expressions;

using Core.DTOs;
using Core.Entities;

namespace Core.Interfaces;

/// <summary>
/// <see cref="IGenericRepository{Entity, DTO}"/> contract to implement
/// </summary>
/// <typeparam name="Entity"></typeparam>
public interface IGenericRepository<Entity, DTO> 
    where Entity : AuditableEntity 
    where DTO : BaseDTO
{
    #region Queries

    #region Entities

    /// <summary>
    /// Get <see cref="Entity"/> by Id
    /// </summary>
    /// <param name="id">The id to find the entity</param>
    /// <returns></returns>
    Task<Entity> GetEntityById(int id);

    /// <summary>
    /// Get <see cref="Entity"/> by Credential Id
    /// </summary>
    /// <param name="id">The id to find the entity</param>
    /// <returns></returns>
    Task<Entity> GetEntityByCreatorId(int id);

    /// <summary>
    /// Get <see cref="Entity"/> collection
    /// </summary>
    /// <returns></returns>
    Task<HashSet<Entity>> GetEntitiesCollection();

    /// <summary>
    /// Get <see cref="Entity"/> by custom query
    /// </summary>
    /// <returns></returns>
    Task<Entity> GetEntityByCustomQuery(Expression<Func<Entity, bool>> first);

    #endregion

    #region DTO's

    /// <summary>
    /// Get <see cref="DTO"/> by Id
    /// </summary>
    /// <param name="id">The id to find the entity</param>
    /// <returns></returns>
    Task<DTO> GetDTOById(int id);

    /// <summary>
    /// Get <see cref="DTO"/> collection
    /// </summary>
    /// <returns></returns>
    Task<HashSet<DTO>> GetDTOCollection();

    #endregion

    #endregion

    #region Commands

    /// <summary>
    /// Add <see cref="Entity"/> to database
    /// </summary>
    /// <param name="entity">Some entity with inherit of <see cref="AuditableEntity"/></param>
    /// <returns></returns>
    Task<Entity> Create(Entity entity);

    /// <summary>
    /// Update <see cref="Entity"/> in database
    /// </summary>
    /// <param name="entity">Some entity with inherit of <see cref="AuditableEntity"/></param>
    /// <returns></returns>
    Task Update(Entity entity);

    /// <summary>
    /// Remove <see cref="Entity"/> from database
    /// </summary>
    /// <param name="entity">Some entity with inherit of <see cref="AuditableEntity"/></param>
    /// <returns></returns>
    Task Delete(int id);

    #endregion
}
