namespace PaymentService.Application.Features.Payments.Commands.ProcessPayment;

public record ProcessPaymentCommand(Guid OrderId, decimal Amount) : MediatR.IRequest<ProcessPaymentResponse>;

public record ProcessPaymentResponse(string Status, string TransactionId, Guid OrderId);