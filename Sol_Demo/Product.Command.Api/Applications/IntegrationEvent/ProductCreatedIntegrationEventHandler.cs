using MassTransit;
using MediatR;
using Product.Message.Request;
using Product.Shared.DTO;
using Product.Shared.DTO.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Product.Command.Api.Applications.IntegrationEvent
{
    public class ProductCreatedIntegrationEvent : INotification
    {
        public CreateProductResponseDTO Product { get; set; }
    }

    public sealed class ProductCreatedIntegrationEventHandler : INotificationHandler<ProductCreatedIntegrationEvent>
    {
        private readonly IBus bus = null;

        public ProductCreatedIntegrationEventHandler(IBus bus)
        {
            this.bus = bus;
        }

        async Task INotificationHandler<ProductCreatedIntegrationEvent>.Handle(ProductCreatedIntegrationEvent notification, CancellationToken cancellationToken)
        {
            try
            {
                var endpoint = await bus.GetSendEndpoint(new Uri("queue:product-created-event-queue"));
                await endpoint.Send<ProductMessageRequest>(new ProductMessageRequest()
                {
                    ProductIdentity = notification?.Product?.ProductIdentity,
                    ProductName = notification?.Product?.ProductName,
                    UnitPrice = notification?.Product?.UnitPrice
                });
            }
            catch
            {
                throw;
            }
        }
    }
}