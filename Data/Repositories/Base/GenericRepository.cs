using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using Core.DTOs;
using Data.Context;
using Core.Entities;
using Core.Interfaces;
using SharedSources.Errors;

namespace Data.Repositories;

/// <summary>
/// <see cref="IGenericRepository{Entity, DTO}"/> implementation
/// </summary>
/// <typeparam name="Entity">The entity to manage</typeparam>
/// <typeparam name="DTO">The dto to manage</typeparam>
public sealed class GenericRepository<Entity, DTO> 
    : BaseRepository, IGenericRepository<Entity, DTO> 
        where Entity : AuditableEntity
        where DTO : BaseDTO
{
    #region Props & Ctor
    
    private readonly DbSet<Entity> _table;
    private readonly IMapper _mapper;

    /// <summary>
    /// <see cref="IGenericRepository{Entity, DTO}"/> ctor
    /// </summary>
    /// <param name="context"></param>
    public GenericRepository(
        MainDbContext context, 
        IMapper mapper) : base(context)
    {
        _table = _context.Set<Entity>();
        _mapper = mapper;
    }

    #endregion

    #region Queries

    #region Entities

    /// <inheritdoc/>
    public async Task<Entity> GetEntityById(int id)
        => await _table.FindAsync(id);

    /// <inheritdoc/>
    public async Task<Entity> GetEntityByCreatorId(int id)
        => await _table.FirstOrDefaultAsync(f => f.CreatedBy.Equals(id));

    /// <inheritdoc/>
    public async Task<HashSet<Entity>> GetEntitiesCollection()
    {
        IEnumerable<Entity> list = await _table.ToListAsync();
        return list.ToHashSet();
    }

    /// <inheritdoc/>
    public async Task<Entity> GetEntityByCustomQuery(Expression<Func<Entity, bool>> first)
        => await _table.FirstOrDefaultAsync(first);

    #endregion

    #region DTO's

    /// <inheritdoc/>
    public async Task<DTO> GetDTOById(int id)
    {
        Entity find = await _table.FindAsync(id);
        return _mapper.Map<DTO>(find);
    }

    /// <inheritdoc/>
    public async Task<HashSet<DTO>> GetDTOCollection()
    {
        IEnumerable<DTO> list = await _table.ProjectTo<DTO>(_mapper.ConfigurationProvider).ToListAsync();
        return list.ToHashSet();
    }

    #endregion

    #endregion

    #region Commands

    /// <inheritdoc/>
    public async Task<Entity> Create(Entity entity)
    {
        EntityEntry<Entity> created = await _context.AddAsync(entity);
        await Commit();

        return created.Entity;
    }

    /// <inheritdoc/>
    public async Task Update(Entity entity)
    {
        Entity finded = await _table.FindAsync(entity.Id);
        _context.Entry(finded).CurrentValues.SetValues(entity);

        await Commit();
    }

    /// <inheritdoc/>
    public async Task Delete(int id)
    {
        Entity finded = await _table.FindAsync(id);
        _context.Remove(finded);

        await Commit();
    }

    private async Task Commit()
    {
        int changes = await _context.SaveChangesAsync();
        if (changes == 0)
            CommonExceptionsHandler.ErrorWhileProccess();
    }

    #endregion
}
