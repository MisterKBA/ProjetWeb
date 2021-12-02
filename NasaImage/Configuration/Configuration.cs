using Microsoft.Extensions.DependencyInjection;
using NasaImage.Interface;
using NasaImage.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace NasaImage.Configuration
{
    public static class Configuration
    {
        public static void UseServices(this IServiceCollection services)
        {
            services.AddHttpClient<ITodoServiceDate,TodoServiceDate>();
        }
    }
}
