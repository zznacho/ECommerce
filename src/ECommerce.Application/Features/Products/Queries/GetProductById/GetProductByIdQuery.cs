using MediatR;

namespace ECommerce.Application.Features.Products.Queries.GetProductById;

public record GetProductByIdQuery(Guid Id) : IRequest<ProductDto>;

public record ProductDto(Guid Id, string Name, string Description, decimal Price, int Stock, Guid CategoryId);