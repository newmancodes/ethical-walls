using ClientManagement.Application.CreateClient;
using ClientManagement.Domain;
using FluentAssertions;
using LanguageExt;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

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
        response.IsSome.Should().BeTrue();
        response.IfSome(r =>
        {
            Received.InOrder(async () =>
            {
                repository.Received(1).Create(Arg.Is<Client>(c => c.Id == r.Id && c.Name == name));
                await unitOfWork.Received(1).Commit(cts.Token);
            });
        });
    }

    [Fact]
    public async Task Client_Creation_Failures_Return_None()
    {
        // Arrange
        using var cts = new CancellationTokenSource();
        
        var name = "some_client_name";
        var failure = new Exception();

        var repository = Substitute.For<IClientRepository>();
        var unitOfWork = Substitute.For<IUnitOfWork>();
        var logger = Substitute.For<ILogger<CreateClientRequestHandler>>();

        unitOfWork.Commit(cts.Token).ThrowsAsync(failure);

        var request = new CreateClientRequest(name);
        
        var sut = new CreateClientRequestHandler(repository, unitOfWork, logger);
        
        // Act
        var response = await sut.Handle(request, cts.Token);
        
        // Assert
        response.IsSome.Should().BeFalse();
    }
}