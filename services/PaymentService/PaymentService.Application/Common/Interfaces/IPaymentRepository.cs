using PaymentService.Domain.Entities;

namespace PaymentService.Application.Common.Interfaces;

public interface IPaymentRepository
{
    Task AddAsync(PaymentTransaction transaction);
}