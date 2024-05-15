using System.Net.Sockets;

var builder = DistributedApplication.CreateBuilder(args);

builder.AddContainer("graphdb", "neo4j", "5.19.0-community-bullseye")
    .WithEnvironment("NEO4J_AUTH", "none")
    .WithAnnotation(new ServiceBindingAnnotation(ProtocolType.Tcp, port: 7474, containerPort: 7474))
    .WithAnnotation(new ServiceBindingAnnotation(ProtocolType.Tcp, port: 7687, containerPort: 7687));

builder.Build().Run();