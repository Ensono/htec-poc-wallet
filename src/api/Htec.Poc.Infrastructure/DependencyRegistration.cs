using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Amido.Stacks.Application.CQRS.Commands;
using Amido.Stacks.Application.CQRS.Queries;
using Amido.Stacks.Configuration.Extensions;
using Amido.Stacks.Data.Documents.CosmosDB.Extensions;
using Amido.Stacks.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Htec.Poc.Application.CommandHandlers;
using Htec.Poc.Application.Integration;
using Htec.Poc.Application.QueryHandlers;
using Htec.Poc.Domain;
using Htec.Poc.Infrastructure.Fakes;
using Htec.Poc.Infrastructure.HealthChecks;
using Amido.Stacks.DynamoDB.Extensions;
using Amazon.DynamoDBv2;
using Htec.Poc.Infrastructure.Repositories;

namespace Htec.Poc.Infrastructure;

public static class DependencyRegistration
{
    static readonly ILogger log = Log.Logger;

    /// <summary>
    /// Register static services that does not change between environment or contexts(i.e: tests)
    /// </summary>
    /// <param name="services"></param>
    public static void ConfigureStaticDependencies(IServiceCollection services)
    {
        AddCommandHandlers(services);
        AddQueryHandlers(services);
    }

    /// <summary>
    /// Register dynamic services that changes between environments or context(i.e: tests)
    /// </summary>
    /// <param name="services"></param>
    public static void ConfigureProductionDependencies(WebHostBuilderContext context, IServiceCollection services)
    {
        services.AddSecrets();

            services.Configure<Amido.Stacks.Messaging.Azure.ServiceBus.Configuration.ServiceBusConfiguration>(context.Configuration.GetSection("ServiceBusConfiguration"));
            services.AddServiceBus();
            services.AddTransient<IApplicationEventPublisher, Amido.Stacks.Messaging.Azure.ServiceBus.Senders.Publishers.EventPublisher>();

            services.Configure<Amido.Stacks.Data.Documents.CosmosDB.CosmosDbConfiguration>(context.Configuration.GetSection("CosmosDb"));
            services.AddCosmosDB();
            services.AddTransient<IWalletRepository, CosmosDbWalletRepository>();
        var healthChecks = services.AddHealthChecks();
            healthChecks.AddCheck<CustomHealthCheck>("Sample"); //This is a sample health check, remove if not needed, more info: https://docs.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/monitor-app-health
            healthChecks.AddCheck<Amido.Stacks.Data.Documents.CosmosDB.CosmosDbDocumentStorage<Wallet>>("CosmosDB");
    }

    private static void AddCommandHandlers(IServiceCollection services)
    {
        log.Information("Loading implementations of {interface}", typeof(ICommandHandler<,>).Name);
        var definitions = typeof(CreateWalletCommandHandler).Assembly.GetImplementationsOf(typeof(ICommandHandler<,>));
        foreach (var definition in definitions)
        {
            log.Information("Registering '{implementation}' as implementation of '{interface}'",
                definition.implementation.FullName, definition.interfaceVariation.FullName);
            services.AddTransient(definition.interfaceVariation, definition.implementation);
        }
    }

    private static void AddQueryHandlers(IServiceCollection services)
    {
        log.Information("Loading implementations of {interface}", typeof(IQueryHandler<,>).Name);
        var definitions = typeof(GetWalletByIdQueryHandler).Assembly.GetImplementationsOf(typeof(IQueryHandler<,>));
        foreach (var definition in definitions)
        {
            log.Information("Registering '{implementation}' as implementation of '{interface}'",
                definition.implementation.FullName, definition.interfaceVariation.FullName);
            services.AddTransient(definition.interfaceVariation, definition.implementation);
        }
    }
}
