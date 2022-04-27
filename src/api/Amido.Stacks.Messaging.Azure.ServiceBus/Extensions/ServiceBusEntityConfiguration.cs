using Amido.Stacks.Configuration;
using Amido.Stacks.Messaging.Azure.ServiceBus.Configuration;
using Amido.Stacks.Messaging.Azure.ServiceBus.Consumer;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace Amido.Stacks.Messaging.Azure.ServiceBus.Extensions;

public static class ServiceBusEntityConfiguration
{
    public static IServiceCollection AddServiceBus(this IServiceCollection services)
    {
        var configuration = GetConfiguration(services);

        var publishersRegistered = services.AddServiceBusPublishers(configuration.Publisher);
        var consumersRegistered = services.AddServiceBusConsumers(configuration.Consumer);

        if (!publishersRegistered && !consumersRegistered)
        {
            throw new Exception("Unable to register any publishers or consumers for service bus. Make sure the configuration has been setup correctly.");
        }

        return services;
    }

    private static ServiceBusConfiguration GetConfiguration(IServiceCollection services)
    {
        var config = services.BuildServiceProvider()
            .GetService<IOptions<ServiceBusConfiguration>>()
            .Value;

        if (config == null || (config.Publisher == null && config.Consumer == null))
        {
            throw new Exception($"Configuration for '{nameof(IOptions<ServiceBusConfiguration>)}' not found. Ensure the call to 'service.Configure<{nameof(ServiceBusConfiguration)}>(configuration)' was called and the appsettings contains at least a definition for Publisher or Consumer. ");
        }

        return config;
    }

    private static bool AddServiceBusPublishers(this IServiceCollection services, ServiceBusPublisherConfiguration configuration)
    {
        if (configuration == null)
        {
            return false;
        }

        var secretResolver = services.BuildServiceProvider().GetService<ISecretResolver<string>>();
        services.AddSingleton(s => new ServiceBusClient(connectionString: secretResolver.ResolveSecretAsync(configuration.ConnectionString).Result)
            .CreateSender(configuration.QueueName));

        return true;
    }

    private static bool AddServiceBusConsumers(this IServiceCollection services, ServiceBusConsumerConfiguration configuration)
    {
        if (configuration == null)
        {
            return false;
        }

        var secretResolver = services.BuildServiceProvider().GetService<ISecretResolver<string>>();
        services.AddSingleton(s => new ServiceBusClient(connectionString: secretResolver.ResolveSecretAsync(configuration.ConnectionString).Result)
            .CreateReceiver(configuration.QueueName));

        services.AddTransient<IEventConsumer, EventConsumer>();

        return true;
    }
}
