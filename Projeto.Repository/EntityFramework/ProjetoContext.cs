using Microsoft.EntityFrameworkCore;
using Projeto.Domain.Entities;
using Projeto.Domain.ValueObjects;
using System.Reflection;
using Projeto.Repository.Helpers;

namespace Projeto.Repository.EntityFramework;

/// <summary>
/// Classe orquestradora da integração com o banco de dados relacional.
/// </summary>
/// <param name="options"></param>
public class ProjetoContext(DbContextOptions<ProjetoContext> options) : DbContext(options)
{
    public virtual DbSet<Motorcycle> Motorcycles { get; set; }
    public virtual DbSet<Deliverer> Deliverers { get; set; }
    public virtual DbSet<Rental> Rentals { get; set; }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        OnSaveTraceableEntities();
        return await base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        foreach (var relation in modelBuilder.Model.GetEntityTypes().Where(x => !x.IsOwned()).SelectMany(e => e.GetForeignKeys()))
        {
            relation.DeleteBehavior = DeleteBehavior.Restrict;
        }

        ApplyEntitiesConfigurations(modelBuilder);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<RegistrationPlate>().HaveConversion<RegistrationPlateConverter>();
        configurationBuilder.Properties<Registry>().HaveConversion<RegistryConverter>();
    }

    /// <summary>
    /// Persiste as entidades que herdam de Traceable, atualizando os campos CreatedAt e UpdatedAt conforme necessário.
    /// </summary>
    private void OnSaveTraceableEntities()
    {
        Parallel.ForEach(
            ChangeTracker.Entries()
            .Where(e => e.Entity is Traceable && (e.State == EntityState.Added || e.State == EntityState.Modified)),
            e =>
            {
                var entity = (Traceable)e.Entity;
                var now = DateTime.UtcNow;

                if (e.State == EntityState.Added)
                {
                    entity.CreatedAt = now;

                    // Se for Rental, gerar ID sequencial
                    if (entity is Rental rental && string.IsNullOrEmpty(rental.Id))
                    {
                        rental.Id = GenerateRentalId();
                    }
                }

                entity.UpdatedAt = now;
            });
    }

    /// <summary>
    /// Obtém o próximo ID de locação no formato "locacaoX", onde X é um número sequencial.
    /// Achei isso estranho, mas está no requisito. 
    /// No Get de locação, ele retorna um ID nesse formato de string, mas ele não é passado via POST como em motos ou entregadores, logo fiz isso.
    /// </summary>
    /// <returns></returns>
    private string GenerateRentalId()
    {
        int lastNumber = 0;

        var lastRentalId = Rentals
            .OrderByDescending(r => r.Id)
            .Select(r => r.Id)
            .FirstOrDefault();

        if (!string.IsNullOrEmpty(lastRentalId) && lastRentalId.StartsWith("locacao"))
        {
            var numberPart = lastRentalId["locacao".Length..];
            if (int.TryParse(numberPart, out var n))
                lastNumber = n;
        }

        return $"locacao{lastNumber + 1}";
    }

    /// <summary>
    /// Aplica todas as configurações de entidades encontradas na assembly atual que implementam IEntityTypeConfiguration<T> para configuração do modelo relacional.
    /// </summary>
    /// <param name="modelBuilder"></param>
    private static void ApplyEntitiesConfigurations(ModelBuilder modelBuilder)
    {
        var assemblies = new[] { Assembly.GetExecutingAssembly() };

        foreach (var assembly in assemblies)
        {
            var configurations = assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.Name.EndsWith("EntityTypeConfiguration"))
                .SelectMany(t => t.GetInterfaces().Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>))
                .Select(i => (Type: t, EntityType: i.GenericTypeArguments[0])));

            foreach (var (configType, entityType) in configurations)
            {
                var configurationInstance = Activator.CreateInstance(configType)!;

                var method = typeof(ModelBuilder)
                    .GetMethod(nameof(ModelBuilder.ApplyConfiguration))?
                    .MakeGenericMethod(entityType);

                method?.Invoke(modelBuilder, [configurationInstance]);
            }
        }
    }
}
