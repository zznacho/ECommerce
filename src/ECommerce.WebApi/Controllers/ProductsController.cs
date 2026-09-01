using ECommerce.Application.Features.Products.Commands.CreateProduct;
using ECommerce.Application.Features.Products.Queries.GetAllProducts;
using ECommerce.Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // 1. CUALQUIERA PUEDE CONSULTAR PRODUCTOS (Incluso sin autenticar o siendo Customer)
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllProductsQuery());
        return Ok(result);
    }

    // 2. SOLO ADMINISTRADORES PUEDEN CREAR PRODUCTOS
    [HttpPost]
    [Authorize(Roles = Roles.Admin)] // <- AQUÍ ESTÁ LA PROTECCIÓN
    public async Task<IActionResult> Create([FromBody] CreateProductCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetAll), new { id = result.Id }, result);
    }

    // 3. SOLO ADMINISTRADORES PUEDEN ELIMINAR PRODUCTOS
    [HttpDelete("{id}")]
    [Authorize(Roles = Roles.Admin)] // <- AQUÍ TAMBIÉN
    public async Task<IActionResult> Delete(Guid id)
    {
        // await _mediator.Send(new DeleteProductCommand(id));
        return NoContent();
    }
}