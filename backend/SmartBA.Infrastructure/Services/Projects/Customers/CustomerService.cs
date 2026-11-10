using Microsoft.EntityFrameworkCore;
using SmartBA.Application.DTOs.Customer;
using SmartBA.Domain.Entities; // Namespace chứa Entity Customer của bạn
using SmartBA.Domain.Entities.Customers;
using SmartBA.Infrastructure.Persistence; // Hoặc namespace chứa AppDbContext của bạn
using SmartBA.Application.Interfaces.Projects.Customers;

public class CustomerService : ICustomerService
{
    private readonly ApplicationDbContext _context;

    public CustomerService(ApplicationDbContext context)
    {
        _context = context; 
    }

    public async Task<CustomerResponseDto?> CreateCustomer(CreateCustomerDto dto)
    {   
        // Trước khi xuống db 

        // Kiểm tra trùng mã số thuế (Masothue)
        bool exists = await _context.Customers.AnyAsync(c => c.Masothue == dto.Masothue);
        if (exists)
        {
            // Ném exception (hoặc trả về kết quả lỗi tùy theo kiến trúc Exception Handling của dự án)
            throw new InvalidOperationException("Mã số thuế này đã tồn tại trong hệ thống, vui lòng kiểm tra lại!");
        }
        
        // 1. Map từ DTO sang Entity Customer
        var customer = new Customer
        {
            Masothue = dto.Masothue,
            CustomerName = dto.CustomerName,
            Industry = dto.Industry,
            ContactName = dto.ContactName,
            ContactNumber = dto.ContactNumber,
            ContactEmail = dto.ContactEmail,
            ContactAddress = dto.contactAddress,
            Description = dto.Description
        };

        // 2. Thêm vào DbSet của AppDbContext
        _context.Customers.Add(customer);

        // 3. Lệnh này mới chính thức thực thi câu lệnh SQL INSERT xuống Database
        await _context.SaveChangesAsync();

        // 4. Map ngược lại sang Response DTO để trả về cho Controller
        return new CustomerResponseDto
        {
            Id = customer.Id,
            Masothue = customer.Masothue,
            CustomerName = customer.CustomerName,
            Industry = customer.Industry,
            ContactName = customer.ContactName,
            ContactNumber = customer.ContactNumber,
            ContactEmail = customer.ContactEmail,
            contactAddress = customer.ContactAddress,
            Description = customer.Description,
            CreatedDate = customer.CreatedDate
        };
    }
}