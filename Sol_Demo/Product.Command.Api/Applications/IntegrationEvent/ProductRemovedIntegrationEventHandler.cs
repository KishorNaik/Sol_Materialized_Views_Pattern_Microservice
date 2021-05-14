using MassTransit;
using MediatR;
using Product.Message.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Product.Command.Api.Applications.IntegrationEvent
{
    public class ProductRemovedIntegrationEvent : INotification
    {
        public Guid? ProductIdentity { get; set; }
    }

    public sealed class ProductRemovedIntegrationEventHandler : INotificationHandler<ProductRemovedIntegrationEvent>
    {
        private readonly IBus bus = null;

        public ProductRemovedIntegrationEventHandler(IBus bus)
        {
            this.bus = bus;
        }

        async Task INotificationHandler<ProductRemovedIntegrationEvent>.Handle(ProductRemovedIntegrationEvent notification, CancellationToken cancellationToken)
        {
            try
            {
                var endpoint = await bus.GetSendEndpoint(new Uri("queue:product-removed-event-queue"));
                await endpoint.Send<ProductMessageRequest>(new ProductMessageRequest()
                {
                    ProductIdentity = notification?.ProductIdentity,
                });
            }
            catch
            {
                throw;
            }
        }
    }
}