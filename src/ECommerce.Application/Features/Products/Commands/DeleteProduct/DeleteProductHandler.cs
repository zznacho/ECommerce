using ECommerce.Application.Interfaces;
using ECommerce.Domain.Exceptions;
using MediatR;

namespace ECommerce.Application.Features.Products.Commands.DeleteProduct;

public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, Unit>
{
    private readonly IProductRepository _productRepository;

    public DeleteProductHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new ProductNotFoundException(request.Id);

        await _productRepository.DeleteAsync(product, cancellationToken);

        return Unit.Value;
    }
}