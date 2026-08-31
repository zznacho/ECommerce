using MediatR;

namespace ECommerce.Application.Features.Products.Commands.CreateProduct;

public record CreateProductCommand(
    string Name,
    string Description,
    decimal Price,
    int Stock,
    Guid CategoryId
) : IRequest<CreateProductResponse>;

public record CreateProductResponse(Guid Id, string Name, decimal Price, int Stock);