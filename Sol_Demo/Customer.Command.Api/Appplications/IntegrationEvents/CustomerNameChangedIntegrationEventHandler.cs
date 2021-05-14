using Customer.Message.Requests;
using MassTransit;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Customer.Command.Api.Appplications.IntegrationEvents
{
    public class CustomerNameChangedIntegrationEvent : INotification
    {
        public Guid? CustomerIdentity { get; set; }

        public String FirstName { get; set; }

        public String LastName { get; set; }
    }

    public sealed class CustomerNameChangedIntegrationEventHandler : INotificationHandler<CustomerNameChangedIntegrationEvent>
    {
        private readonly IBus bus = null;

        public CustomerNameChangedIntegrationEventHandler(IBus bus)
        {
            this.bus = bus;
        }

        async Task INotificationHandler<CustomerNameChangedIntegrationEvent>.Handle(CustomerNameChangedIntegrationEvent notification, CancellationToken cancellationToken)
        {
            try
            {
                var endpoint = await bus.GetSendEndpoint(new Uri("queue:customer-name-changed-event-queue"));
                await endpoint.Send<CustomerMessageRequest>(new CustomerMessageRequest()
                {
                    CustomerIdentity = notification.CustomerIdentity,
                    FirstName = notification.FirstName,
                    LastName = notification.LastName,
                });
            }
            catch
            {
                throw;
            }
        }
    }
}