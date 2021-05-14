using Framework.State.EventPublisher.EventState;
using MediatR;
using Product.Command.Api.Applications.IntegrationEvent;
using Product.Shared.DTO.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Product.Command.Api.Applications.DomainEvents
{
    public class ProductUpdatedDomainEvent : INotification
    {
        public UpdateProductResponseDTO UpdateProductResponse { get; set; }
    }

    public sealed class ProductUpdatedDomainEventHandler : INotificationHandler<ProductUpdatedDomainEvent>
    {
        private readonly IEventStateContext eventStateContext = null;

        public ProductUpdatedDomainEventHandler(IEventStateContext eventStateContext)
        {
            this.eventStateContext = eventStateContext;
        }

        async Task INotificationHandler<ProductUpdatedDomainEvent>.Handle(ProductUpdatedDomainEvent notification, CancellationToken cancellationToken)
        {
            try
            {
                await
                    eventStateContext
                    .AddNotification<ProductUpdatedDomainEvent>
                    (
                        (productUpdatedDomainEvent) =>
                            (
                                productUpdatedDomainEvent.UpdateProductResponse.UpdateNewProductResponse.ProductName != productUpdatedDomainEvent.UpdateProductResponse.UpdateOldProductResponse.ProductNameOldValue
                            )
                        ,
                        new ProductNameChangedIntegrationEvent()
                        {
                            ProductIdentity = notification?.UpdateProductResponse?.UpdateNewProductResponse?.ProductIdentity,
                            ProductName = notification?.UpdateProductResponse?.UpdateNewProductResponse?.ProductName
                        }
                    )
                    .AddNotification<ProductUpdatedDomainEvent>
                    (
                        (productUpdatedDomainEvent) =>
                        (
                            productUpdatedDomainEvent.UpdateProductResponse.UpdateNewProductResponse.UnitPrice != productUpdatedDomainEvent.UpdateProductResponse.UpdateOldProductResponse.UnitPriceOldValue
                        ),
                        new ProductUnitPriceChangedIntegrationEvent()
                        {
                            ProductIdentity = notification?.UpdateProductResponse.UpdateNewProductResponse.ProductIdentity,
                            UnitPrice = notification?.UpdateProductResponse?.UpdateNewProductResponse?.UnitPrice
                        }
                    )
                    .PublishNotification(notification);
            }
            catch
            {
                throw;
            }
        }
    }
}