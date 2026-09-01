using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using ECommerce.Infrastructure.Persistence;

namespace ECommerce.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly ApplicationDbContext _context;

    public OrderRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Order order)
    {
        await _context.Set<Order>().AddAsync(order);
        await _context.SaveChangesAsync();
    }

    public async Task<Order?> GetByIdAsync(Guid id)
    {
        return await _context.Set<Order>().FindAsync(id);
    }

    public async Task UpdateAsync(Order order)
    {
        _context.Set<Order>().Update(order);
        await _context.SaveChangesAsync();
    }
}