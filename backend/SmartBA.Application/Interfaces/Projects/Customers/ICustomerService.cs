using SmartBA.Application.DTOs.Customer;

namespace SmartBA.Application.Interfaces.Projects.Customers;
public interface ICustomerService
{

    Task<CustomerResponseDto?> CreateCustomer(CreateCustomerDto customerDto);
    
    
}