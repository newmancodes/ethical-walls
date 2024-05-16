using ClientManagement.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ClientManagement.Application.CreateClient;

public class CreateClientRequestHandler : IRequestHandler<CreateClientRequest, Maybe<CreateClientResponse>>
{
    private readonly IClientRepository _clientRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateClientRequestHandler> _logger;
    
    public CreateClientRequestHandler(
        IClientRepository clientRepository,
        IUnitOfWork unitOfWork,
        ILogger<CreateClientRequestHandler> logger)
    {
        _clientRepository = clientRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Maybe<CreateClientResponse>> Handle(CreateClientRequest request, CancellationToken cancellationToken)
    {
        var client = Client.Create(request.Name);
        _clientRepository.Create(client);
        await _unitOfWork.Commit(cancellationToken);
        return Maybe.Some(new CreateClientResponse(client.Id));
    }
}

public record CreateClientRequest(string Name) : IRequest<Maybe<CreateClientResponse>>;

public record CreateClientResponse(Guid Id);
