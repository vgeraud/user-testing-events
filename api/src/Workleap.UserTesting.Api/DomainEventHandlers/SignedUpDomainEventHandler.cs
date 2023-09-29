using System;
using System.Threading;
using System.Threading.Tasks;
using Workleap.DomainEventPropagation;
using Workleap.UserTesting.Api.DomainEvents;

namespace Workleap.UserTesting.Api.DomainEventHandlers
{
    public class SignedUpDomainEventHandler : IDomainEventHandler<SignedUpDomainEvent>
    {
        public Task HandleDomainEventAsync(SignedUpDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            Console.WriteLine("Hello from handler");

            return Task.CompletedTask;
        }
    }
}
