using ECommerce.Application.Features.Products.Queries.GetProductById;
using MediatR;

namespace ECommerce.Application.Features.Products.Queries.GetAllProducts;

public record GetAllProductsQuery() : IRequest<List<ProductDto>>;