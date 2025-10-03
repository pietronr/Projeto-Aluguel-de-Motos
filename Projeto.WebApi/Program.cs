using Projeto.WebApi.Setups;

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();

ConfigureApp(app);

app.Run();

static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    ControllersSetup.AddContollersWithJson(services);
    SwaggerSetup.AddSwagger(services);
    DependencyInjectionSetup.AddApplicationServices(services);
    DbContextSetup.AddDbContext(services, configuration);

    services.AddMemoryCache();
    services.AddCors();
}

static void ConfigureApp(WebApplication app)
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(access => access.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
    app.UseRouting();
    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
    app.MapDefaultControllerRoute();
}
