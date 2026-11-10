using System.ComponentModel.DataAnnotations;

namespace SmartBA.Application.DTOs.Customer;

public class CreateCustomerDto
{
    [Required]
    [MaxLength(50, ErrorMessage = "Required")]
    public string Masothue { get; set; } = string.Empty;

    [Required]
    [MaxLength(250, ErrorMessage = "Required")]
    public string CustomerName { get; set; } = string.Empty;

    [MaxLength(150)]
    public string Industry { get; set; } = string.Empty;

    [MaxLength(250, ErrorMessage = "Required")]
    public string ContactName { get; set; } = string.Empty;

    [MaxLength(250, ErrorMessage = "Required")]
    public string ContactNumber { get; set; } = string.Empty;

    [EmailAddress(ErrorMessage = "Định dạng Email không hợp lệ")]
    [MaxLength(150, ErrorMessage = "Required")]
    public string ContactEmail { get; set; } = string.Empty;

    public string contactAddress { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

internal class ErrorMessageAttribute : Attribute
{
}