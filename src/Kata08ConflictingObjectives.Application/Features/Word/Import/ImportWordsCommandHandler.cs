using Kata08ConflictingObjectives.Application.Common.Data;
using Kata08ConflictingObjectives.Application.Common.Data.Repositories;
using MediatR;

namespace Kata08ConflictingObjectives.Application.Features.Word.Import;

public class ImportWordsCommandHandler: IRequestHandler<ImportWordsCommand>
{
    private readonly IWordRepository _wordRepository;

    public ImportWordsCommandHandler(IUnitOfWork unitOfWork)
    {
        _wordRepository = unitOfWork.GetRepository<IWordRepository>();
    }

    public async Task Handle(ImportWordsCommand request, CancellationToken cancellationToken)
    {
        var words = new List<Domain.Entities.Word>();
        using (var reader = new StreamReader(request.WordsFile.OpenReadStream()))
        {
            while (reader.Peek() >= 0)
            {
                var newWordValue = await reader.ReadLineAsync(cancellationToken);
                if (newWordValue is not null)
                {
                    var word = await _wordRepository.GetByValueAsync(newWordValue, cancellationToken);
                    if (word is null)
                    {
                        words.Add(new Domain.Entities.Word(newWordValue.Trim().ToLower())); 
                    }
                }
            }
        }

        await _wordRepository.AddRangeAsync(words, cancellationToken);
    }
}