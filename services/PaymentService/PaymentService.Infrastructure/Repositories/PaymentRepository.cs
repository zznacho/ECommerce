using PaymentService.Application.Common.Interfaces;
using PaymentService.Domain.Entities;
using PaymentService.Infrastructure.Persistence;

namespace PaymentService.Infrastructure.Repositories;

public class PaymentRepository : IPaymentRepository
{
    private readonly PaymentDbContext _context;

    public PaymentRepository(PaymentDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(PaymentTransaction transaction)
    {
        await _context.Transactions.AddAsync(transaction);
        await _context.SaveChangesAsync();
    }
}