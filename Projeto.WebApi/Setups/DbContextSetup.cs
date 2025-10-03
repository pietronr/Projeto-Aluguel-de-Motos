using Microsoft.EntityFrameworkCore;
using Projeto.Repository.EntityFramework;

namespace Projeto.WebApi.Setups;

public static class DbContextSetup
{
    public static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ProjetoContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                o =>
                {
                    o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery).CommandTimeout(60).EnableRetryOnFailure();
                });
        });
    }
}
