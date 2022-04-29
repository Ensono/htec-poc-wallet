﻿using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Microsoft.Extensions.Logging;
using Htec.Poc.Application.CQRS.Events;

namespace Htec.Poc.BackgroundWorker.Handlers;

public class WalletCreatedEventHandler : IApplicationEventHandler<WalletCreatedEvent>
{
    private readonly ILogger<WalletCreatedEventHandler> log;

    public WalletCreatedEventHandler(ILogger<WalletCreatedEventHandler> log)
    {
        this.log = log;
    }

    public Task HandleAsync(WalletCreatedEvent appEvent)
    {
        log.LogInformation($"Executing WalletCreatedEventHandler...");
        return Task.CompletedTask;
    }
}