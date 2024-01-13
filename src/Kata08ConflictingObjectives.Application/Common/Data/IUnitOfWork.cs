namespace Kata08ConflictingObjectives.Application.Common.Data;

public interface IUnitOfWork
{
    T GetRepository<T>();
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}