using PaymentService.Domain.Enums;

namespace PaymentService.Domain.Entities;

public class PaymentTransaction
{
    public Guid Id { get; private set; }
    public Guid OrderId { get; private set; }
    public decimal Amount { get; private set; }
    public PaymentStatus Status { get; private set; }
    public string TransactionId { get; private set; } = string.Empty;
    public DateTime ProcessedAt { get; private set; }

    private PaymentTransaction() { } // Para EF Core

    public PaymentTransaction(Guid orderId, decimal amount)
    {
        Id = Guid.NewGuid();
        OrderId = orderId;
        Amount = amount;
        ProcessedAt = DateTime.UtcNow;
        
        // Regla de negocio explícita: Aprueba si el monto es menor a 100,000
        EvaluatePayment();
    }

    private void EvaluatePayment()
    {
        if (Amount <= 0)
        {
            throw new ArgumentException("El monto debe ser mayor a cero.");
        }

        if (Amount < 100000m)
        {
            Status = PaymentStatus.Approved;
            TransactionId = $"TX-APP-{Guid.NewGuid().ToString()[..8].ToUpper()}";
        }
        else
        {
            Status = PaymentStatus.Rejected;
            TransactionId = $"TX-REJ-{Guid.NewGuid().ToString()[..8].ToUpper()}";
        }
    }
}