using AwesomeShop.Services.Customers.Application.Commands;
using AwesomeShop.Services.Customers.Application.Queries;
using AwesomeShop.Services.Customers.Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AwesomeShop.Services.Customers.Api.Controller;

[Route("api/[controller]")]
public class CustomersController(IMediator mediator) : ControllerBase
{

    private readonly IMediator _mediator = mediator;

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] AddCustomer command) {
        var id = await _mediator.Send(command);

        return Created($"api/customers/{id}", value: null);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id) {
        var query = new GetCustomerById(id);

        var customerViewModel = await _mediator.Send(query);

        if (customerViewModel == null) {
            return NotFound();
        }

        return Ok(customerViewModel);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(Guid id, [FromBody] UpdateCustomerViewModel viewModel) {
        var command = new UpdateCustomer(
            Id: id, 
            PhoneNumber: viewModel.PhoneNumber, 
            Address: viewModel.Address
        );
        await _mediator.Send(command);

        return NoContent();
    }

}