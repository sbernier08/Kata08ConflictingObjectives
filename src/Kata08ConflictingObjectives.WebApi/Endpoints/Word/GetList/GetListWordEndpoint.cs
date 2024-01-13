using FastEndpoints;
using Kata08ConflictingObjectives.Application.Features.Word.GetList;
using MediatR;

namespace Kata08ConflictingObjectives.WebApi.Endpoints.Word.GetList;

public class GetListWordEndpoint: EndpointWithoutRequest
{
    private readonly IMediator _mediator;

    public GetListWordEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    public override void Configure()
    {
        AllowAnonymous();
        Get("/words");
    }

    public override async Task HandleAsync(CancellationToken cancellationToken)
    {
        var wordList = await _mediator.Send(new GetListWordQuery(), cancellationToken);
        await SendAsync(wordList, StatusCodes.Status200OK, cancellationToken);
    }
}