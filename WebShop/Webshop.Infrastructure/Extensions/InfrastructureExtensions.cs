using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Webshop.Domain.Interfaces;
using Webshop.Infrastructure.EF.Repositories;

namespace Webshop.Infrastructure.Extensions;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IProduktRepository, ProduktRepository>();
        services.AddScoped<IProduktOrderRepository, ProduktOrderRepository>();
        services.AddScoped<IKategoriRepository, KategoriRepository>();
        services.AddScoped<IKundRepository, KundRepository>();
        services.AddScoped<ILeverantörRepository, LeverantörRepository>();
        services.AddScoped<IFraktOmbudRepository, FraktOmbudRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IProduktKampanjRepository, ProduktKampanjRepository>();

        return services;
    }
}