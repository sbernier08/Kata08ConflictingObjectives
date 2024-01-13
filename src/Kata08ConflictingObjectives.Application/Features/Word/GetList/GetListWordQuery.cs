using Kata08ConflictingObjectives.Application.Features.Word.GetList.DTOs;
using MediatR;

namespace Kata08ConflictingObjectives.Application.Features.Word.GetList;

public record GetListWordQuery(): IRequest<List<WordDTO>>;