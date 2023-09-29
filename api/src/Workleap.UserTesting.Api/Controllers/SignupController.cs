using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;
using Workleap.DomainEventPropagation;
using Workleap.UserTesting.Api.DomainEvents;

namespace Workleap.UserTesting.Api.Controllers
{
    [ApiController]
    public class SignupController : ControllerBase
    {
        private readonly IEventPropagationClient _eventPropagationClient;

        public SignupController(IEventPropagationClient eventPropagationClient)
        {
            this._eventPropagationClient = eventPropagationClient;
        }

        [HttpPost]
        [Route("signup")]
        [AllowAnonymous]
        public async Task<IActionResult> Signup(CancellationToken cancellationToken)
        {
            var domainEvent = new SignedUpDomainEvent
            {
                MemberId = Guid.NewGuid()
            };

            await this._eventPropagationClient.PublishDomainEventAsync(domainEvent, cancellationToken);
            
            return this.Ok();
        }
    }
}
