using Newtonsoft.Json;

namespace SharedSources.Extensions;

/// <summary>
/// <see cref="JsonSerializeExtension"/> json extensions
/// </summary>
public static class JsonSerializeExtension
{
    /// <summary>
    /// Deserialize object 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static T Deserialize<T>(this string value)
        => JsonConvert.DeserializeObject<T>(value);

    /// <summary>
    /// Serialize some value
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static string Serialize(this object obj)
        => JsonConvert.SerializeObject(obj);
}

