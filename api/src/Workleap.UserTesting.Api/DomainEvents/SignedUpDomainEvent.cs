using System;
using Workleap.DomainEventPropagation;

namespace Workleap.UserTesting.Api.DomainEvents
{
    [DomainEvent("SignedUp")]
    public class SignedUpDomainEvent : IDomainEvent
    {
        public Guid MemberId { get; set; }
    }
}
