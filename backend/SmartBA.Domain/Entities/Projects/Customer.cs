namespace SmartBA.Domain.Entities.Customers;

public class Customer: BaseEntity
{
    public string Masothue { get; set; } = string.Empty;

    public string CustomerName { get; set; } = string.Empty;

    public string Industry { get; set; } = string.Empty;

    public string ContactName { get; set; } = string.Empty;

    public string ContactNumber { get; set; } = string.Empty;

    public string ContactEmail { get; set; } = string.Empty;

     public string ContactAddress { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

}