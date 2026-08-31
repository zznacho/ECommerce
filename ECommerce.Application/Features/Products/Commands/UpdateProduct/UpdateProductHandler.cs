using ECommerce.Application.Interfaces;
using ECommerce.Domain.Exceptions;
using MediatR;

namespace ECommerce.Application.Features.Products.Commands.UpdateProduct;

public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, Unit>
{
    private readonly IProductRepository _productRepository;

    public UpdateProductHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new ProductNotFoundException(request.Id);

        product.UpdateDetails(request.Name, request.Description, request.Price, request.Stock);

        await _productRepository.UpdateAsync(product, cancellationToken);

        return Unit.Value;
    }
}