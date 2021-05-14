using Customer.Command.Api.Appplications.Features;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Customer.Command.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator mediator = null;

        public CustomerController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateCustomerCommand createCustomerCommand)
            => base.Ok(await mediator.Send<bool>(createCustomerCommand));

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] UpdateCustomerCommand updateCustomerCommand)
            => base.Ok(await mediator.Send<bool>(updateCustomerCommand));

        [HttpPost("delete")]
        public async Task<IActionResult> Remove([FromBody] RemoveCustomerCommand removeCustomerCommand)
            => base.Ok(await mediator.Send<bool>(removeCustomerCommand));
    }
}