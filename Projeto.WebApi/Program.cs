using Microsoft.EntityFrameworkCore;
using Projeto.Repository.EntityFramework;
using Projeto.WebApi.Setups;

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();

ConfigureApp(app);
CheckAndRunMigrations(app);

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

static void CheckAndRunMigrations(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<ProjetoContext>();
    var pendingMigrations = db.Database.GetPendingMigrations();

    if (pendingMigrations.Any())
        db.Database.Migrate();
}
