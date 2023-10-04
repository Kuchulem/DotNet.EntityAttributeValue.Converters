using Kuchulem.DotNet.EntityAttributeValue.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuchulem.DotNet.EntityAttributeValue.Converters.Extensions
{
    /// <summary>
    /// Extensions methods to simplify the DI configuration for converters.
    /// </summary>
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the <see cref="EavConverter"/> to the DI and configures
        /// the <see cref="EavRawValueConverterProvider"/>.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configureProvider"></param>
        /// <returns></returns>
        public static IServiceCollection AddEavConverter(
            this IServiceCollection services, 
            Action<IServiceProvider, IEavRawValueConverterProvider> configureProvider)
        {
            services.AddScoped<IEavRawValueConverterProvider>(serviceProvider =>
            {
                var converter = new EavRawValueConverterProvider();
                configureProvider(serviceProvider, converter);

                return converter;
            });

            services.AddScoped<IEavConverter, EavConverter>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="EavConverter"/> to the DI with an implementation
        /// of <see cref="IEavRawValueConverter"/>
        /// </summary>
        /// <typeparam name="TConverterProvider"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddEavConverter<TConverterProvider>(this IServiceCollection services)
            where TConverterProvider : class, IEavRawValueConverterProvider
        {
            services.AddScoped<IEavRawValueConverterProvider, TConverterProvider>();
            services.AddScoped<IEavConverter, EavConverter>();

            return services;
        }
    }
}
