using Kata08ConflictingObjectives.Application.Common.Data.Repositories;
using Kata08ConflictingObjectives.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kata08ConflictingObjectives.Infrastructure.Data.Repositories;

public class WordRepository: RepositoryBase<Word>, IWordRepository
{
    private readonly AppDbContext _appDbContext;
    
    public WordRepository(AppDbContext appDbContext) : base(appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<List<Word>> GetListAsync(int? length = null, int? maxLength = null, CancellationToken cancellationToken = default)
    {
        if (maxLength is not null)
        {
            return await _appDbContext.Words.Where(x => x.Value.Length <= maxLength).ToListAsync(cancellationToken);
        }
        if (length is not null)
        {
            return await _appDbContext.Words.Where(x => x.Value.Length == length).ToListAsync(cancellationToken);
        }
        
        return await _appDbContext.Words.ToListAsync(cancellationToken);
    }

    public async Task<Word?> GetByValueAsync(string value, CancellationToken cancellationToken = default)
    {
        return await _appDbContext.Words.FirstOrDefaultAsync(x => x.Value == value, cancellationToken);
    }
}