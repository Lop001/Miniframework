using Microsoft.Extensions.DependencyInjection;

namespace MyApp.DependencyInjection
{
    public static class RepositoryRegistration
    {
        public static IServiceCollection AddGeneratedRepositories(this IServiceCollection services)
        {
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();

            return services;
        }
    }
}
