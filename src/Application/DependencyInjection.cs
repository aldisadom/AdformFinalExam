﻿using Application.Interfaces;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IClientService, ClientService>();
        services.AddScoped<IItemService, ItemService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<ISellerService, SellerService>();
        services.AddScoped<IUserService, UserService>();
    }
}