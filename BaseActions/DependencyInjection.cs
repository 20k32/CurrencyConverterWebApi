using BaseActions.Queries.GetSpecificCurrency;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Models;
using System.Reflection;

namespace BaseActions
{
    public static class DependencyInjection
    {
        
        public static IServiceCollection AddServicesForApplicationLayer(this IServiceCollection services)
        {
            var executingAssembly = Assembly.GetExecutingAssembly();

            services.AddMediatR(config => config.RegisterServicesFromAssembly(executingAssembly));
            services.AddValidatorsFromAssembly(executingAssembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            return services;
        }

        public static ApiSettings BindApiConfigurationFromFile(this IConfigurationBuilder builder, string fileName)
        {
            var apiSettings = new ApiSettings();
            builder
                .AddJsonFile(fileName)
                .Build()
                .GetSection("ApiSettings")
                .Bind(apiSettings);
            return apiSettings;
        }
    }
}
