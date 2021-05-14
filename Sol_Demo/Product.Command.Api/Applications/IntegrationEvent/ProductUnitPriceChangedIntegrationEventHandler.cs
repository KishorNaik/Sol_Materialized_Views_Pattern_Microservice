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
    public class ProductUnitPriceChangedIntegrationEvent : INotification
    {
        public Guid? ProductIdentity { get; set; }

        public double? UnitPrice { get; set; }
    }

    public sealed class ProductUnitPriceChangedIntegrationEventHandler : INotificationHandler<ProductUnitPriceChangedIntegrationEvent>
    {
        private readonly IBus bus = null;

        public ProductUnitPriceChangedIntegrationEventHandler(IBus bus)
        {
            this.bus = bus;
        }

        async Task INotificationHandler<ProductUnitPriceChangedIntegrationEvent>.Handle(ProductUnitPriceChangedIntegrationEvent notification, CancellationToken cancellationToken)
        {
            try
            {
                var endpoint = await bus.GetSendEndpoint(new Uri("queue:product-unitprice-changed-event-queue"));
                await endpoint.Send<ProductMessageRequest>(new ProductMessageRequest()
                {
                    ProductIdentity = notification?.ProductIdentity,
                    UnitPrice = notification.UnitPrice,
                });
            }
            catch
            {
                throw;
            }
        }
    }
}