using System.ComponentModel.DataAnnotations;

namespace Core.Entities;

/// <summary>
/// <see cref="AuditableEntity"/> is a base entity and have prop to audit all entities
/// </summary>
public class AuditableEntity
{
    /// <summary>
    /// Primary key
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Created date
    /// </summary>
    [Required]
    public DateTimeOffset? CreatedDate { get; set; }

    /// <summary>
    /// Modified date
    /// </summary>
    [Required]
    public DateTimeOffset ModifiedDate { get; set; }

    /// <summary>
    /// Owner data id
    /// </summary>
    [Required]
    public int CreatedBy { get; set; }

    /// <summary>
    /// <see cref="AuditableEntity"/> ctor
    /// </summary>
    public AuditableEntity()
    {
        if(CreatedDate is null)
            CreatedDate = DateTimeOffset.UtcNow;

        ModifiedDate = DateTimeOffset.UtcNow;
    }
}
