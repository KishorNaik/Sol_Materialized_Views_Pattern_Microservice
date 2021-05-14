using AutoMapper;
using Customer.Message.Requests;
using MassTransit;
using MediatR;
using OrderCustomer.Message.Queue.Infrastructures.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderCustomer.Message.Queue.Applications.Messages
{
    public class CustomerCreatedMessageConsumeHandler : IConsumer<CustomerMessageRequest>
    {
        private readonly IMediator mediator = null;
        private readonly IMapper mapper = null;

        public CustomerCreatedMessageConsumeHandler(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        async Task IConsumer<CustomerMessageRequest>.Consume(ConsumeContext<CustomerMessageRequest> context)
        {
            try
            {
                await mediator.Publish<CustomerCreatedRepository>(mapper.Map<CustomerCreatedRepository>(context.Message));
            }
            catch
            {
                throw;
            }
        }
    }
}