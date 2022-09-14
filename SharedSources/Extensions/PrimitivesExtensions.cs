namespace SharedSources.Extensions;

/// <summary>
/// <see cref="PrimitivesExtensions"/> extensions methods
/// </summary>
public static class PrimitivesExtensions
{
    /// <summary>
    /// Convert a string to lower and trim start - end
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static string LowerNormalize(this string data) 
        => data.TrimStart().TrimEnd().ToLower();

    /// <summary>
    /// Convert a string to upper and trim start - end
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static string UpperNormalize(this string data)
        => data.TrimStart().TrimEnd().ToUpper();
}
