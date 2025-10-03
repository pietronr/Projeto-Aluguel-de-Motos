namespace Projeto.WebApi.Setups;

public static class SwaggerSetup
{
    public static void AddSwagger(IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }
}
