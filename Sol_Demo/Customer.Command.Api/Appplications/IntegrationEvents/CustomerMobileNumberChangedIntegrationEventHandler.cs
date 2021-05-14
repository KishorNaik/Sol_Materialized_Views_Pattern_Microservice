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
    public class CustomerMobileNumberChangedIntegrationEvent : INotification
    {
        public Guid? CustomerIdentity { get; set; }

        public String MobileNo { get; set; }
    }

    public sealed class CustomerMobileNumberChangedIntegrationEventHandler : INotificationHandler<CustomerMobileNumberChangedIntegrationEvent>
    {
        private readonly IBus bus = null;

        public CustomerMobileNumberChangedIntegrationEventHandler(IBus bus)
        {
            this.bus = bus;
        }

        async Task INotificationHandler<CustomerMobileNumberChangedIntegrationEvent>.Handle(CustomerMobileNumberChangedIntegrationEvent notification, CancellationToken cancellationToken)
        {
            try
            {
                var endpoint = await bus.GetSendEndpoint(new Uri("queue:customer-mobileno-changed-event-queue"));
                await endpoint.Send<CustomerMessageRequest>(new CustomerMessageRequest()
                {
                    CustomerIdentity = notification.CustomerIdentity,
                    MobileNo = notification.MobileNo
                });
            }
            catch
            {
                throw;
            }
        }
    }
}