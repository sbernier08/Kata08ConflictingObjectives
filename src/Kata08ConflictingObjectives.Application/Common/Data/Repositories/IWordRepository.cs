using Kata08ConflictingObjectives.Domain.Entities;

namespace Kata08ConflictingObjectives.Application.Common.Data.Repositories;

public interface IWordRepository: IRepositoryBase<Word>
{
    Task<List<Word>> GetListAsync(int? length = null, int? maxLength = null, CancellationToken cancellationToken = default);
    Task<Word?> GetByValueAsync(string value, CancellationToken cancellationToken = default);
}