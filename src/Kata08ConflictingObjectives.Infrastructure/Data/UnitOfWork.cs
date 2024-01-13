using Kata08ConflictingObjectives.Application.Common.Data;
using Microsoft.Extensions.DependencyInjection;

namespace Kata08ConflictingObjectives.Infrastructure.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _appDbContext;
    private readonly IServiceProvider _serviceProvider;

    public UnitOfWork(AppDbContext appDbContext, IServiceProvider serviceProvider)
    {
        _appDbContext = appDbContext;
        _serviceProvider = serviceProvider;
    }

    public T GetRepository<T>() where T : notnull
    {
        return _serviceProvider.GetRequiredService<T>();
    }

    public async Task BeginTransactionAsync()
    {
        await _appDbContext.Database.BeginTransactionAsync();
    }
    
    public async Task CommitTransactionAsync()
    {
        await _appDbContext.Database.CommitTransactionAsync();
    }
    
    public async Task RollbackTransactionAsync()
    {
        await _appDbContext.Database.RollbackTransactionAsync();
    }   
}