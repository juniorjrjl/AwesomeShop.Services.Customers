using AwesomeShop.Services.Customers.Application.Commands;
using AwesomeShop.Services.Customers.Application.ViewModels;
using AwesomeShop.Services.Customers.Core.Entities;
using AwesomeShop.Services.Customers.Core.ValueObjects;

namespace AwesomeShop.Services.Customers.Application.Mappers;

public class CustomerMapper : ICustomerMapper
{
    public Customer ToCustomer(AddCustomer request) => 
        Customer.Create(fullName: request.FullName, birthDate: request.BirthDate, email: request.Email);

    public Customer ToCustomer(Customer customer, UpdateCustomer request) =>
        customer.Update(request.PhoneNumber, ToAddress(request.Address));

    public GetCustomerByIdViewModel ToViewModel(Customer? customer) => 
        customer is null ? 
        null :
        new (
            Id: customer.Id, 
            FullName: customer.FullName, 
            BirthDate: customer.BirthDate, 
            Address: ToAddress(customer.Address)
        );

    private static Address ToAddress(AddressDTO dto)
    {
        ArgumentNullException.ThrowIfNullOrWhiteSpace(dto.Street);
        ArgumentNullException.ThrowIfNullOrWhiteSpace(dto.Number);
        ArgumentNullException.ThrowIfNullOrWhiteSpace(dto.City);
        ArgumentNullException.ThrowIfNullOrWhiteSpace(dto.State);
        ArgumentNullException.ThrowIfNullOrWhiteSpace(dto.ZipCode);
        return new (
            street: dto.Street, 
            number: dto.Number, 
            city: dto.City, 
            state: dto.State, 
            zipCode: dto.ZipCode
        );
    }

    private static AddressDTO? ToAddress(Address? dto) => 
        dto is null ? 
        null :
        new (
            Street: dto.Street, 
            Number: dto.Number, 
            City: dto.City, 
            State: dto.State, 
            ZipCode: dto.ZipCode
        );

}