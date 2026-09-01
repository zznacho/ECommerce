using MediatR;
using PaymentService.Application.Common.Interfaces;
using PaymentService.Domain.Entities;

namespace PaymentService.Application.Features.Payments.Commands.ProcessPayment;

public class ProcessPaymentHandler : IRequestHandler<ProcessPaymentCommand, ProcessPaymentResponse>
{
    private readonly IPaymentRepository _repository;

    public ProcessPaymentHandler(IPaymentRepository repository)
    {
        _repository = repository;
    }

    public async Task<ProcessPaymentResponse> Handle(ProcessPaymentCommand request, CancellationToken cancellationToken)
    {
        // La regla de negocio se ejecuta dentro del dominio al instanciar la entidad
        var transaction = new PaymentTransaction(request.OrderId, request.Amount);

        await _repository.AddAsync(transaction);

        return new ProcessPaymentResponse(
            transaction.Status.ToString(),
            transaction.TransactionId,
            transaction.OrderId
        );
    }
}