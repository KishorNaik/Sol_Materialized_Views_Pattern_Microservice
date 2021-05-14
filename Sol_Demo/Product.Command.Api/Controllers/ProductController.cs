using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product.Command.Api.Applications.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Command.Api.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator mediator = null;

        public ProductController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateProductAsync([FromBody] CreateProductCommand createProductCommand)
            => base.Ok(await mediator.Send<bool>(createProductCommand));

        [HttpPost("update")]
        public async Task<IActionResult> UpdateProductAsync([FromBody] UpdateProductCommand updateProductCommand)
            => base.Ok(await mediator.Send<bool>(updateProductCommand));

        [HttpPost("remove")]
        public async Task<IActionResult> RemoveProductAsync([FromBody] RemoveProductCommand removeProductCommand)
            => base.Ok(await mediator.Send<bool>(removeProductCommand));
    }
}