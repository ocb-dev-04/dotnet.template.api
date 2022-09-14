using Data.Context;

namespace Data.Repositories;

/// <summary>
/// <see cref="BaseRepository"/> class with a <see cref="IDisposable"/> implementation
/// </summary>
public class BaseRepository : IDisposable
{
    #region Props & Ctor

    protected readonly MainDbContext _context;

    /// <summary>
    /// <see cref="BaseRepository"/> ctor
    /// </summary>
    /// <param name="context"></param>
    public BaseRepository(MainDbContext context)
    {
        _context = context;
    }

    #endregion

    /// <summary>
    /// <see cref="Dispose"/> method
    /// </summary>
    public void Dispose()
    {
        if(_context != null)
        {
            _context.Dispose();
        }
    }
}
