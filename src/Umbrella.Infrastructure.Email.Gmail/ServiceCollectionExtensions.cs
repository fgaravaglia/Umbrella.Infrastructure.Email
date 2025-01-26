using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Umbrella.Infrastructure.Email.Core;

namespace Umbrella.Infrastructure.Email.Gmail
{
    /// <summary>
    /// 
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// registers in the DI the provider for Gmail Api
        /// </summary>
        /// <param name="services"></param>
        /// <param name="clientId"></param>
        /// <param name="secretId"></param>
        /// <returns></returns>
        public static IServiceCollection UsingGmailApi(this IServiceCollection services, string clientId, string secretId)
        {
            ArgumentNullException.ThrowIfNull(services);
            ArgumentNullException.ThrowIfNullOrEmpty(clientId);
            ArgumentNullException.ThrowIfNullOrEmpty(secretId);

            services.AddScoped<IEmailSender>(x => new GmailApiSender(null, clientId, secretId));
            return services;
        }

    }
}