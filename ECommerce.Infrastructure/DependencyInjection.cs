using ECommerce.Application.Interfaces;
using ECommerce.Application.Common.Interfaces;
using ECommerce.Infrastructure.Authentication;
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
        // 1. Configurar DbContext (usando InMemory para desarrollo)
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseInMemoryDatabase("ECommerceDb"));

        // 2. Registrar el Repositorio de Productos
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        // 3. Registrar JWT Settings y el servicio de Tokens
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        return services;
    }
}