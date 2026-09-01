using MediatR;

namespace ECommerce.Application.Features.Orders.Commands.CreateOrder;

public record CreateOrderCommand(decimal TotalAmount) : IRequest<CreateOrderResponse>;

public record CreateOrderResponse(Guid OrderId, decimal TotalAmount, string Status, string? TransactionId);