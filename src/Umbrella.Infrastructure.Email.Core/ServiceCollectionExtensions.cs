using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Umbrella.Infrastructure.Email.Core
{
    /// <summary>
    /// 
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddUmbrellaEmail(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services);

            services.AddTransient<IEmailService, EmailService>();
            return services;
        }
    }
}