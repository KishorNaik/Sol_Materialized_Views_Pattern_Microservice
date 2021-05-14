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
    public sealed class CustomerRemovedMesageConsumerHandler : IConsumer<CustomerMessageRequest>
    {
        private readonly IMediator mediator = null;

        public CustomerRemovedMesageConsumerHandler(IMediator mediator)
        {
            this.mediator = mediator;
        }

        async Task IConsumer<CustomerMessageRequest>.Consume(ConsumeContext<CustomerMessageRequest> context)
        {
            try
            {
                await mediator.Publish<CustomerRemovedRepository>(new CustomerRemovedRepository()
                {
                    CustomerIdentity = context.Message.CustomerIdentity
                });
            }
            catch
            {
                throw;
            }
        }
    }
}