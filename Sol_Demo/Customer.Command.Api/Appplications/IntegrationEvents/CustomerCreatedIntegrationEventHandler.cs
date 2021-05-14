using Customer.Message.Requests;
using Customer.Shared.DTO.Requests;
using MassTransit;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Customer.Command.Api.Appplications.IntegrationEvents
{
    public class CustomerCreatedIntegrationEvent : INotification
    {
        public CustomerRequestDTO Customer { get; set; }
    }

    public class CustomerCreatedIntegrationEventHandler : INotificationHandler<CustomerCreatedIntegrationEvent>
    {
        private readonly IBus bus = null;

        public CustomerCreatedIntegrationEventHandler(IBus bus)
        {
            this.bus = bus;
        }

        async Task INotificationHandler<CustomerCreatedIntegrationEvent>.Handle(CustomerCreatedIntegrationEvent notification, CancellationToken cancellationToken)
        {
            try
            {
                var endpoint = await bus.GetSendEndpoint(new Uri("queue:customer-created-event-queue"));
                await endpoint.Send<CustomerMessageRequest>(new CustomerMessageRequest()
                {
                    CustomerIdentity = notification.Customer.CustomerIdentity,
                    FirstName = notification.Customer.FirstName,
                    LastName = notification.Customer.LastName,
                    MobileNo = notification.Customer.MobileNo
                });
            }
            catch
            {
                throw;
            }
        }
    }
}