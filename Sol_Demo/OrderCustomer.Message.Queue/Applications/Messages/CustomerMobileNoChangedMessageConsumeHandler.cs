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
    public sealed class CustomerMobileNoChangedMessageConsumeHandler : IConsumer<CustomerMessageRequest>
    {
        private readonly IMediator mediator = null;

        public CustomerMobileNoChangedMessageConsumeHandler(IMediator mediator)
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
                    MobileNo = context?.Message?.MobileNo,
                    UpdateState = UpdateState.Customer_MobileNo_Changed
                });
            }
            catch
            {
                throw;
            }
        }
    }
}