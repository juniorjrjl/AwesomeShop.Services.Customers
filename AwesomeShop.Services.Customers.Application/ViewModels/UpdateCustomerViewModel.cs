using AwesomeShop.Services.Customers.Application.Commands;

namespace AwesomeShop.Services.Customers.Application.ViewModels;

public record UpdateCustomerViewModel(string PhoneNumber, AddressDTO Address)
{
    
}