using ECommerce.Application.Interfaces;
using ECommerce.Domain.Exceptions;
using MediatR;

namespace ECommerce.Application.Features.Products.Queries.GetProductById;

public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
{
    private readonly IProductRepository _productRepository;

    public GetProductByIdHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new ProductNotFoundException(request.Id);

        return new ProductDto(product.Id, product.Name, product.Description, product.Price, product.Stock, product.CategoryId);
    }
}