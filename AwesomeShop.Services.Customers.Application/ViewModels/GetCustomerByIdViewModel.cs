using AwesomeShop.Services.Customers.Application.Commands;

namespace AwesomeShop.Services.Customers.Application.ViewModels;

public record GetCustomerByIdViewModel(Guid Id, string FullName, DateTime BirthDate, AddressDTO? Address)
{
    
}