namespace Projeto.WebApi.Setups;

public static class SwaggerSetup
{
    public static void AddSwagger(IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.ConfigureSwaggerGen(options =>
        {
            options.EnableAnnotations();
            options.SwaggerDoc("1.0.0", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Projeto API", Version = "1.0.0" });
        });
    }
}
