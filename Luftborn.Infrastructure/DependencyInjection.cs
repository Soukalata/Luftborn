using Luftborn.Application.Abstractions;
using Luftborn.Infrastructure.Data;
using Luftborn.Infrastructure.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Luftborn.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // NEW: Register the database context.
        services.AddDbContext<AppDBContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection")));

        // NEW: Register the data-access implementation.
        services.AddScoped<IProductDataAccess, ProductDataAccess>();

        return services;
    }
}