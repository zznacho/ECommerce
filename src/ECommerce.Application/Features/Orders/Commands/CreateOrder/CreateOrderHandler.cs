using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using MediatR;

namespace ECommerce.Application.Features.Orders.Commands.CreateOrder;

public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, CreateOrderResponse>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IPaymentClient _paymentClient;

    public CreateOrderHandler(IOrderRepository orderRepository, IPaymentClient paymentClient)
    {
        _orderRepository = orderRepository;
        _paymentClient = paymentClient;
    }

    public async Task<CreateOrderResponse> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        // 1. Crear la orden localmente en el e-commerce
        var order = new Order(Guid.NewGuid(), request.TotalAmount);
        await _orderRepository.AddAsync(order);

        // 2. Comunicarse con el PaymentService vía HttpClient
        var paymentResult = await _paymentClient.ProcessPaymentAsync(order.Id, order.TotalAmount, cancellationToken);

        // 3. Evaluar respuesta del microservicio y aplicar la regla de negocio
        if (paymentResult == null)
        {
            order.MarkAsServiceUnavailable();
        }
        else if (paymentResult.Status == "Approved")
        {
            order.MarkAsPaid(paymentResult.TransactionId);
        }
        else
        {
            order.MarkAsPaymentRejected(paymentResult.TransactionId);
        }

        // 4. Actualizar estado final en la base de datos
        await _orderRepository.UpdateAsync(order);

        return new CreateOrderResponse(order.Id, order.TotalAmount, order.Status, order.TransactionId);
    }
}