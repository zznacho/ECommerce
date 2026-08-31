using ECommerce.Application.Interfaces;
using ECommerce.Infrastructure.Persistence;
using ECommerce.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Usamos InMemory database para simplificar la ejecución local
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseInMemoryDatabase("ECommerceDb"));

        services.AddScoped<IProductRepository, ProductRepository>();

        return services;
    }
}