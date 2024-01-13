using FastEndpoints;
using MediatR;

namespace Kata08ConflictingObjectives.WebApi.Endpoints.Word.Import;

public class ImportWordsEndpoint: Endpoint<ImportWordsEndpointRequest>
{
    private readonly IMediator _mediator;

    public ImportWordsEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    public override void Configure()
    {
        AllowAnonymous();
        Post("/words/import");
        AllowFileUploads();
    }

    public override async Task HandleAsync(ImportWordsEndpointRequest request, CancellationToken cancellationToken)
    {
        await _mediator.Send(request.ToImportWordsCommand(), cancellationToken);
        await SendAsync(null, StatusCodes.Status200OK, cancellationToken);
    }
}