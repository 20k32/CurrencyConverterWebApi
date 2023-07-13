using Databases.DBContexts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Databases
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddFileDB(this IServiceCollection services) =>
            services.AddSingleton<DBContext, FileDBContext>();
    }
}
