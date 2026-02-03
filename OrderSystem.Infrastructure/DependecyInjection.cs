using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderSystem.Domain.Repository;
using OrderSystem.Domain.Services;
using OrderSystem.Domain.UnitOfWork;
using OrderSystem.Infrastructure.Data;
using OrderSystem.Infrastructure.Repository.EntityFramework;
using OrderSystem.Infrastructure.Services;
using OrderSystem.Infrastructure.UnitOfWork;

namespace OrderSystem.Infrastructure;

public static class DependecyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly("OrderSystem.Infrastructure") // IMPORTANTE: Define onde as migrações serão salvas
            ));


        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IOrderUnitOfWork, OrderUnitOfWork>();
        services.AddScoped<IUnitOfWork, UnitOfWorkEf>();

        services.AddScoped<IPasswordService, PasswordService>();

        return services;
    }
}
