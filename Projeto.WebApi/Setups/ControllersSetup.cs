using System.Text.Json.Serialization;

namespace Projeto.WebApi.Setups;

public static class ControllersSetup
{
    public static void AddContollersWithJson(IServiceCollection services)
    {
        services
            .AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.JsonSerializerOptions.WriteIndented = true;
            });
    }
}
