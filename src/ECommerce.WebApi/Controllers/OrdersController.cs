using ECommerce.Application.Features.Orders.Commands.CreateOrder;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize] // Requiere estar autenticado con JWT
public class OrdersController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrdersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOrderCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}