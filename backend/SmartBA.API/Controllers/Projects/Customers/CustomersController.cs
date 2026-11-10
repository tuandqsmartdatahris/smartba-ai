using Microsoft.AspNetCore.Mvc;
using SmartBA.Application.DTOs.Customer;
using SmartBA.Application.Interfaces.Projects.Customers;
using Microsoft.AspNetCore.Authorization;
using SmartBA.Infrastructure.Persistence; // Thay namespace này bằng namespace chứa DbContext thực tế của bạn

//[Authorize] // <-- Khóa chặt Controller, yêu cầu phải đăng nhập mới được gọi API
[Route("api/[controller]")]
[ApiController]
public class CustomersController : ControllerBase
{
    private readonly ICustomerService _customerService;

    // private readonly ApplicationDbContext _context; // Khai báo biến _context
    // Tiêm ApplicationDbContext thông qua constructor

    public CustomersController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    /* [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        //var result = await _customerService.GetAllAsync();
        return Ok(result);
    } */

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCustomerDto dto)
    {   
        
        try
        {
            var result = await _customerService.CreateCustomer(dto);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            // Trả về mã 400 kèm thông điệp lỗi nghiệp vụ (trùng mã số thuế)
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            // Xử lý các lỗi hệ thống khác
            return StatusCode(500, new { message = "Lỗi hệ thống: " + ex.Message });
        }
    }
}