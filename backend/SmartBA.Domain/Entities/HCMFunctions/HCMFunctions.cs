namespace SmartBA.Domain.Entities.HCMFunctions;
using System.ComponentModel.DataAnnotations;
public class HCMFunction: BaseEntity
{

    [Required]
    [MaxLength(10)]
    public string FunctionCode { get; set; } = string.Empty;

    public string FunctionName { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

}