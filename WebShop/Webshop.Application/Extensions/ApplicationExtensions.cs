using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Webshop.Application.Interfaces;
using Webshop.Application.Services;
using Webshop.Domain.Interfaces;

namespace Webshop.Application.Extensions;

public static class ApplicationExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IProduktService, ProduktService>();
        services.AddScoped<IProduktOrderService, ProduktOrderService>();
        services.AddScoped<IKategoriService, KategoriService>();
        services.AddScoped<IKundService, KundService>();
        services.AddScoped<ILeverantörService, LeverantörService>();
        services.AddScoped<IFraktOmbudService, FraktOmbudService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IProduktKampanjService, ProduktKampanjService>();
        services.AddScoped<IValutaService, ValutaService>();
        

        return services;
    }
}