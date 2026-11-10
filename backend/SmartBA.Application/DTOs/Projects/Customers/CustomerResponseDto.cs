namespace SmartBA.Application.DTOs.Customer;

public class CustomerResponseDto
{
    public Guid Id { get; set; }
    public string Masothue { get; set; } = string.Empty;
    public string CustomerName { get; set; } = string.Empty;
    public string Industry { get; set; } = string.Empty;
    public string ContactName { get; set; } = string.Empty;
    public string ContactNumber { get; set; } = string.Empty;
    public string ContactEmail { get; set; } = string.Empty;

    public string contactAddress { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
}