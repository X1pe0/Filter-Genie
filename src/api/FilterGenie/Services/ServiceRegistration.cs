using FilterGenie.Interfaces;
using FilterGenie.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace FilterGenie.Services
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IAgentRepository, AgentRepository>();
            services.AddTransient<IOrgRepository, OrgRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}