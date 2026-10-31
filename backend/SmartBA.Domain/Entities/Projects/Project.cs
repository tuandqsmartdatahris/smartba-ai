namespace SmartBA.Domain.Entities.project;

public class Project
{
    public Guid Id { get; set; }

    public string Masothue { get; set; } = string.Empty;

    public string CustomerName { get; set; } = string.Empty;

    public string Industry { get; set; } = string.Empty;

    public string ContactName { get; set; } = string.Empty;

    public string ContactNumber { get; set; } = string.Empty;

    public string ContactEmail { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    public string CreatedName { get; set; } = string.Empty;

    public DateTime? UpdatedDate { get; set; }

    public string UpdatedName { get; set; } = string.Empty;

    public DateTime? DeletedDate { get; set; }

    public string DeletedName { get; set; } = string.Empty;
}