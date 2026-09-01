using ECommerce.Application.Features.Products.Queries.GetProductById;
using ECommerce.Application.Interfaces;
using MediatR;

namespace ECommerce.Application.Features.Products.Queries.GetAllProducts;

public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, List<ProductDto>>
{
    private readonly IProductRepository _productRepository;

    public GetAllProductsHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<List<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetAllAsync(cancellationToken);

        return products.Select(p => new ProductDto(p.Id, p.Name, p.Description, p.Price, p.Stock, p.CategoryId)).ToList();
    }
}