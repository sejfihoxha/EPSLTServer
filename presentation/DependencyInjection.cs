using EPSLTServer.Domain.Interfaces;
using EPSLTTaskServer.Application.Interfaces;
using EPSLTTaskServer.Application.Services;
using EPSLTTaskServer.Infrastructure.EntityFramework;
using EPSLTTaskServer.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

public static class DependencyInjection
{
    public static IServiceCollection AddDiscountCodeDb(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DiscountDb");

        services.AddDbContext<DiscountDbContext>(options =>
            options.UseSqlServer(connectionString));

        return services;
    }

    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IDiscountRepository, DiscountRepository>();
        services.AddScoped<IDiscountService, DiscountService>();

        return services;
    }
}
