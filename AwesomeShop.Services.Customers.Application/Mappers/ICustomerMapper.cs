using AwesomeShop.Services.Customers.Application.Commands;
using AwesomeShop.Services.Customers.Application.ViewModels;
using AwesomeShop.Services.Customers.Core.Entities;

namespace AwesomeShop.Services.Customers.Application.Mappers;

public interface ICustomerMapper
{
    Customer ToCustomer(AddCustomer request);

    Customer ToCustomer(Customer customer, UpdateCustomer request);

    GetCustomerByIdViewModel ToViewModel(Customer customer);

}