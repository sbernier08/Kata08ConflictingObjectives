using Kata08ConflictingObjectives.Application.Features.Word.Import;

namespace Kata08ConflictingObjectives.WebApi.Endpoints.Word.Import;

public record ImportWordsEndpointRequest(IFormFile WordsFile)
{
    public ImportWordsCommand ToImportWordsCommand()
    {
        return new ImportWordsCommand(WordsFile);
    }
};