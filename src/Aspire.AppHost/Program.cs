var builder = DistributedApplication.CreateBuilder(args);

builder.AddContainer("graphdb", "neo4j", "5.19.0-community-bullseye")
    .WithEnvironment("NEO4J_AUTH", "none")
    .WithEndpoint(7474, targetPort: 7474, name: "neo4j-http")
    .WithEndpoint(7687, targetPort: 7687, name: "neo4j-bolt");

builder.AddRabbitMQ("messaging");

builder.Build().Run();