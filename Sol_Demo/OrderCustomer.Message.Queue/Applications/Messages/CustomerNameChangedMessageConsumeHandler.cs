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
    public sealed class CustomerNameChangedMessageConsumeHandler : IConsumer<CustomerMessageRequest>
    {
        private readonly IMediator mediator = null;

        public CustomerNameChangedMessageConsumeHandler(IMediator mediator)
        {
            this.mediator = mediator;
        }

        async Task IConsumer<CustomerMessageRequest>.Consume(ConsumeContext<CustomerMessageRequest> context)
        {
            try
            {
                await mediator.Publish<CustomerUpdatedRepository>(new CustomerUpdatedRepository()
                {
                    CustomerIdentity = context?.Message?.CustomerIdentity,
                    FirstName = context?.Message?.FirstName,
                    LastName = context?.Message?.LastName,
                    UpdateState = UpdateState.Customer_Name_Changed
                });
            }
            catch
            {
                throw;
            }
        }
    }
}