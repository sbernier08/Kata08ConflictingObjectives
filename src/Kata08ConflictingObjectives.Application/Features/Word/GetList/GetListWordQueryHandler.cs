using Kata08ConflictingObjectives.Application.Common.Data;
using Kata08ConflictingObjectives.Application.Common.Data.Repositories;
using Kata08ConflictingObjectives.Application.Features.Word.GetList.DTOs;
using MediatR;

namespace Kata08ConflictingObjectives.Application.Features.Word.GetList;

public class GetListWordQueryHandler: IRequestHandler<GetListWordQuery, List<WordDTO>>
{
    private readonly IWordRepository _wordRepository;

    public GetListWordQueryHandler(IUnitOfWork unitOfWork)
    {
        _wordRepository = unitOfWork.GetRepository<IWordRepository>();
    }
    
    public async Task<List<WordDTO>> Handle(GetListWordQuery request, CancellationToken cancellationToken)
    {
        var composedWords = await _wordRepository.GetListAsync(maxLength: 5, cancellationToken: cancellationToken);
        var wordsDictionary = new Dictionary<string, int>();
        foreach (var composedWord in composedWords)
        {
            wordsDictionary.TryAdd(composedWord.Value, 1);
        }

        var wordList = await _wordRepository.GetListAsync(length: 6, cancellationToken: cancellationToken);
        var wordsDtoList = new List<WordDTO>();
        foreach (var word in wordList)
        {
            for (int i = 1; i < word.Value.Length; i++)
            {
                var part1 = word.Value.Substring(0, i);
                var part2 = word.Value.Substring(i);
        
                if (wordsDictionary.ContainsKey(part1) && wordsDictionary.ContainsKey(part2))
                {
                    var wordDto = wordsDtoList.FirstOrDefault(x => x.Value == word.Value);
                    if (wordDto is null)
                    {
                        wordDto = new WordDTO(word.Value);
                        wordsDtoList.Add(wordDto);
                    }
                    
                    wordDto.ComposedWords.Add(new ComposedWord(new List<string>(){part1, part2}));
                }
            }
        }

        return wordsDtoList;
    }
}