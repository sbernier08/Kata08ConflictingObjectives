using MediatR;
using Microsoft.AspNetCore.Http;

namespace Kata08ConflictingObjectives.Application.Features.Word.Import;

public record ImportWordsCommand(IFormFile WordsFile): IRequest;