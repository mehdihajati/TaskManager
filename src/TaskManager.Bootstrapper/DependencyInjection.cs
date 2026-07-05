using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Application;

namespace TaskManager.Bootstrapper
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAllServices(this IServiceCollection services)
        {
            services.AddApplication();
            return services;
        }
    }
}