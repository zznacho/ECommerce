using ECommerce.Application.Common.Models;

namespace ECommerce.Application.Interfaces; // Namespace según tu ubicación actual

public interface IPaymentClient
{
    Task<PaymentResponseDto?> ProcessPaymentAsync(Guid orderId, decimal amount, CancellationToken cancellationToken = default);
}