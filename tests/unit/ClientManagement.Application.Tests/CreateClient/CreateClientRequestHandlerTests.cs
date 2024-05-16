using ClientManagement.Application.CreateClient;
using ClientManagement.Domain;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace ClientManagement.Application.Tests.CreateClient;

public class CreateClientRequestHandlerTests
{
    [Fact]
    public async Task New_Client_Is_Created()
    {
        // Arrange
        using var cts = new CancellationTokenSource();
        
        var name = "some_client_name";

        var repository = Substitute.For<IClientRepository>();
        var unitOfWork = Substitute.For<IUnitOfWork>();
        var logger = Substitute.For<ILogger<CreateClientRequestHandler>>();

        var request = new CreateClientRequest(name);
        
        var sut = new CreateClientRequestHandler(repository, unitOfWork, logger);

        // Act
        var response = await sut.Handle(request, cts.Token);

        // Assert
        response.TryGetValue(out var client).Should().BeTrue();
        Received.InOrder(() =>
        {
            repository.Received(1).Create(Arg.Is<Client>(c => c.Id == client.Id && c.Name == name));
            unitOfWork.Received(1).Commit(cts.Token).GetAwaiter().GetResult();
        });
    }
}