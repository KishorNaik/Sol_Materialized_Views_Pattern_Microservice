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
    public sealed class ProductNameChangedMessageConsumerHandler : IConsumer<ProductMessageRequest>
    {
        private readonly IMediator mediator = null;

        public ProductNameChangedMessageConsumerHandler(IMediator mediator)
        {
            this.mediator = mediator;
        }

        async Task IConsumer<ProductMessageRequest>.Consume(ConsumeContext<ProductMessageRequest> context)
        {
            try
            {
                await mediator.Publish<ProductNameChangedReposiotry>(new ProductNameChangedReposiotry()
                {
                    ProductIdentity = context?.Message?.ProductIdentity,
                    ProductName = context?.Message?.ProductName
                });
            }
            catch
            {
                throw;
            }
        }
    }
}