using MassTransit;
using MediatR;
using OrderProduct.Message.Queue.Infrastructures.Repositories;
using Product.Message.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderProduct.Message.Queue.Applications.Messages
{
    public sealed class ProductRemovedMessageConsumerHandler : IConsumer<ProductMessageRequest>
    {
        private readonly IMediator mediator = null;

        public ProductRemovedMessageConsumerHandler(IMediator mediator)
        {
            this.mediator = mediator;
        }

        async Task IConsumer<ProductMessageRequest>.Consume(ConsumeContext<ProductMessageRequest> context)
        {
            try
            {
                await mediator.Publish<ProductRemovedRepository>(new ProductRemovedRepository()
                {
                    ProductIdentity = context?.Message?.ProductIdentity
                });
            }
            catch
            {
                throw;
            }
        }
    }
}