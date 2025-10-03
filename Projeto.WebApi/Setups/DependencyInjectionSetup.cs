using Projeto.Repository.EntityFramework;
using Projeto.Repository.EntityFramework.Repositories;
using Projeto.Services.Interfaces;
using Projeto.Services.Services;

namespace Projeto.WebApi.Setups;

public static class DependencyInjectionSetup
{
    public static void AddApplicationServices(IServiceCollection services)
    {
        services.AddHttpClient();

        services.AddScoped<IMotorcycleService, MotorcycleService>();
        services.AddScoped<IDelivererService, DelivererService>();
        services.AddScoped<IRentalService, RentalService>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IMotorcycleRepository, MotorcycleRepository>();
        services.AddScoped<IDelivererRepository, DelivererRepository>();
        services.AddScoped<IRentalRepository, RentalRepository>();
    }
}
