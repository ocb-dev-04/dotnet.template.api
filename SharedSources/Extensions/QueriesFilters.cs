namespace SharedSources.Extensions;

/// <summary>
/// <see cref="QueriesFilters"/> extension methods class
/// </summary>
public static class QueriesFilters
{
    /// <summary>
    /// Extension method to paginate queries
    /// </summary>
    /// <typeparam name="T">The type of entity that will be paginated</typeparam>
    /// <param name="query">Query in self</param>
    /// <param name="pageNumber">Page number to paginate</param>
    /// <param name="pageGrow">The grow of result</param>
    /// <returns></returns>
    public static IQueryable<T> Paginate<T>(this IQueryable<T> query, int pageNumber = 1, int pageGrow = 10)
        => query.Skip(pageNumber <= 1 ? 0 : pageNumber * pageGrow).Take(pageGrow).AsQueryable();
}
