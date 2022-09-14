namespace SharedSources.Models;

/// <summary>
/// <see cref="StandarResponseModel{T}"/> model to response into status code object
/// </summary>
/// <typeparam name="T"></typeparam>
public class StandarResponseModel<T>
{
    /// <summary>
    /// If request is success or not
    /// </summary>
    public bool Success { get; set; } = false;

    /// <summary>
    /// Response data
    /// </summary>
    public T? Data { get; set; }

    /// <summary>
    /// Message if some error ocurred
    /// </summary>
    public object Message { get; set; } = string.Empty;

    public StandarResponseModel<T> SetError(object errorMessage)
    {
        this.Message = errorMessage;
        return this;
    }

    public StandarResponseModel<T> SetData(T data)
    {
        this.Success = true;
        this.Data = data;
        return this;
    }
}
