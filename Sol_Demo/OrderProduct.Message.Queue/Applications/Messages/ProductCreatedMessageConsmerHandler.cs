using AutoMapper;
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
    public sealed class ProductCreatedMessageConsmerHandler : IConsumer<ProductMessageRequest>
    {
        private readonly IMediator mediator = null;
        private readonly IMapper mapper = null;

        public ProductCreatedMessageConsmerHandler(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        async Task IConsumer<ProductMessageRequest>.Consume(ConsumeContext<ProductMessageRequest> context)
        {
            try
            {
                await mediator.Publish<ProductCreatedRepository>(mapper.Map<ProductCreatedRepository>(context.Message));
            }
            catch
            {
                throw;
            }
        }
    }
}