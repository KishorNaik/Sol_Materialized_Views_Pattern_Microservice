using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order.Query.Api.Applications.Features;
using Order.Shared.DTO.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order.Query.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        public readonly IMediator mediator = null;

        public OrderController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("get-order-history")]
        public async Task<IActionResult> GetOrderHistoryAsync([FromBody] GetOrderHistoryQuery getOrderHistoryQuery)
            => base.Ok(await mediator.Send<IReadOnlyList<OrderHistoryResponseDTO>>(getOrderHistoryQuery));
    }
}