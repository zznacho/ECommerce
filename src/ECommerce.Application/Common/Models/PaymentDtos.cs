namespace ECommerce.Application.Common.Models;

public record PaymentRequestDto(Guid OrderId, decimal Amount);

public record PaymentResponseDto(string Status, string TransactionId, Guid OrderId);