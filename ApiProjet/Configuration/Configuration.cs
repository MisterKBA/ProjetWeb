using ApiProjet.Interfaces;
using ApiProjet.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiProjet.Configuration
{
    public static class Configuration
    {
        public static void UseServices(this IServiceCollection services)
        {
            services.AddHttpClient<ITodoServiceApiProjet, TodoServiceApiProjet >();
        }
    }
}

