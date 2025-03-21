using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var username = builder.AddParameter("username", secret: true);
var password = builder.AddParameter("password", secret: true);
var rabbitmq = builder.AddRabbitMQ("messagebus", username, password)
    .WithManagementPlugin();

var apiCatalogo = builder.AddProject<NSE_Catalogo_API>("apicatalogo");
var apiIdentidade = builder.AddProject<NSE_Identidade_API>("apiidentidade");
var apiCliente = builder.AddProject<NSE_Cliente_API>("apicliente");

builder.AddProject<NSE_WebApp_MVC>("webFrontEnd")
    .WithExternalHttpEndpoints()
    .WithReference(apiCatalogo)
    .WithReference(apiIdentidade)
    .WithReference(apiCliente);

builder.Build().Run();