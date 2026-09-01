using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PaymentService.Application.Common.Interfaces;
using PaymentService.Infrastructure.Persistence;
using PaymentService.Infrastructure.Repositories;

namespace PaymentService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<PaymentDbContext>(options =>
            options.UseInMemoryDatabase("PaymentServiceDb"));

        services.AddScoped<IPaymentRepository, PaymentRepository>();

        return services;
    }
}