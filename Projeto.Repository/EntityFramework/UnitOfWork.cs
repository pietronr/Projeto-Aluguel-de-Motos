using Projeto.Services.Interfaces;

namespace Projeto.Repository.EntityFramework;

/// <summary>
/// Classe para lidar com persistência de dados.
/// Dependency Injection lida bem com a disposição, mas importante demonstrar aqui.
/// É disposable para garantir o fechamento da conexão com o banco, além da liberação de recursos.
/// </summary>
/// <param name="context">DbContext da aplicação.</param>
public class UnitOfWork(ProjetoContext context) : IUnitOfWork
{
    private bool _disposed;

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return context.SaveChangesAsync(cancellationToken);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
                context.Dispose();

            _disposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~UnitOfWork()
    {
        Dispose(false);
    }
}
