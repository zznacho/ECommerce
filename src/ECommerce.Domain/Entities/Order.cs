namespace ECommerce.Domain.Entities;

public class Order
{
    public Guid Id { get; private set; }
    public Guid CustomerId { get; private set; }
    public decimal TotalAmount { get; private set; }
    public string Status { get; private set; } = "Pending";
    public string? TransactionId { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private Order() { } // Para EF Core

    public Order(Guid customerId, decimal totalAmount)
    {
        Id = Guid.NewGuid();
        CustomerId = customerId;
        TotalAmount = totalAmount > 0 ? totalAmount : throw new ArgumentException("El monto debe ser mayor a 0.");
        CreatedAt = DateTime.UtcNow;
    }

    public void MarkAsPaid(string transactionId)
    {
        Status = "Paid";
        TransactionId = transactionId;
    }

    public void MarkAsPaymentRejected(string transactionId)
    {
        Status = "PaymentRejected";
        TransactionId = transactionId;
    }

    public void MarkAsServiceUnavailable()
    {
        Status = "PaymentServiceUnavailable";
    }
}