using ClientManagement.Domain;
using FluentValidation;
using LanguageExt;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ClientManagement.Application.CreateClient;

public class CreateClientRequestHandler : IRequestHandler<CreateClientRequest, Option<CreateClientResponse>>
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

    public async Task<Option<CreateClientResponse>> Handle(CreateClientRequest request, CancellationToken cancellationToken)
    {
        var client = Client.Create(request.Name);
        _clientRepository.Create(client);

        try
        {
            await _unitOfWork.Commit(cancellationToken);
            return Option<CreateClientResponse>.Some(new CreateClientResponse(client.Id));
        }
        catch (Exception e)
        {
            _logger.LogError(e,"Unable to create client with name '{ClientName}'.", request.Name);
            return Option<CreateClientResponse>.None;
        }
    }
}

public record CreateClientRequest(string Name) : IRequest<Option<CreateClientResponse>>;

public sealed class CreateClientRequestValidator : AbstractValidator<CreateClientRequest>
{
    public CreateClientRequestValidator()
    {
        RuleFor(r => r.Name)
            .NotNull()
            .NotEmpty()
            .MaximumLength(50);
    }
}

public record CreateClientResponse(Guid Id);
