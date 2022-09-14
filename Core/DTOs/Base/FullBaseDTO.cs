namespace Core.DTOs;

/// <summary>
/// <see cref="FullBaseDTO"/> full base dto model
/// </summary>
public class FullBaseDTO : BaseDTO
{
    /// <summary>
    /// Created date
    /// </summary>
    public DateTimeOffset? CreatedDate { get; set; }

    /// <summary>
    /// Modified date
    /// </summary>
    public DateTimeOffset? ModifiedDate { get; set; }

    /// <summary>
    /// Owner data id
    /// </summary>
    public Guid CreatedBy { get; set; }
}
