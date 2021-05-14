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
    public class ProductNameChangedIntegrationEvent : INotification
    {
        public Guid? ProductIdentity { get; set; }

        public String ProductName { get; set; }
    }

    public sealed class ProductNameChangedIntegrationEventHandler : INotificationHandler<ProductNameChangedIntegrationEvent>
    {
        private readonly IBus bus = null;

        public ProductNameChangedIntegrationEventHandler(IBus bus)
        {
            this.bus = bus;
        }

        async Task INotificationHandler<ProductNameChangedIntegrationEvent>.Handle(ProductNameChangedIntegrationEvent notification, CancellationToken cancellationToken)
        {
            try
            {
                var endpoint = await bus.GetSendEndpoint(new Uri("queue:product-name-changed-event-queue"));
                await endpoint.Send<ProductMessageRequest>(new ProductMessageRequest()
                {
                    ProductIdentity = notification?.ProductIdentity,
                    ProductName = notification.ProductName,
                });
            }
            catch
            {
                throw;
            }
        }
    }
}