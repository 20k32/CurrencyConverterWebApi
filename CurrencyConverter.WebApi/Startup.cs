using BaseActions;
using BaseActions.Queries;
using BaseActions.Queries.GetCurrencyList;
using Databases;
using Models;
using System.Reflection;

namespace CurrencyConverter.WebApi
{
    internal sealed class Startup
    {
        private IConfiguration Configuration = null!;

        public Startup()
        {
            var builder = new ConfigurationBuilder();

            Configuration = builder
                .AddJsonFile("appsettings.json")
                .AddJsonFile("CachedCurrency.json")
                .Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddScoped<HttpClient>();
            services.Configure<ApiSettings>(Configuration.GetSection(nameof(ApiSettings)));
            services.AddMemoryCache();
            services.AddFileDB();

            services.AddServicesForApplicationLayer();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting();
            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
