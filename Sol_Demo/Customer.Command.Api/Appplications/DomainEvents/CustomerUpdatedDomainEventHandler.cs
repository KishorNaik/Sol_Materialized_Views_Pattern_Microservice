using Customer.Command.Api.Appplications.IntegrationEvents;
using Customer.Shared.DTO.Responses;
using Framework.State.EventPublisher.EventState;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Customer.Command.Api.Appplications.DomainEvents
{
    public class CustomerUpdatedDomainEvent : INotification
    {
        public UpdateCustomerResponseDTO UpdateCustomerResponse { get; set; }
    }

    public sealed class CustomerUpdatedDomainEventHandler : INotificationHandler<CustomerUpdatedDomainEvent>
    {
        private readonly IEventStateContext eventStateContext = null;

        public CustomerUpdatedDomainEventHandler(IEventStateContext eventStateContext)
        {
            this.eventStateContext = eventStateContext;
        }

        async Task INotificationHandler<CustomerUpdatedDomainEvent>.Handle(CustomerUpdatedDomainEvent notification, CancellationToken cancellationToken)
        {
            try
            {
                //List<Task> tasks = new List<Task>();

                //if (notification.UpdateCustomerResponse.UpdateNewCustomerResponse.FirstName != notification.UpdateCustomerResponse.UpdateOldCustomerResponse.FirstNameOldValue
                //    ||
                //    notification.UpdateCustomerResponse.UpdateNewCustomerResponse.LastName != notification.UpdateCustomerResponse.UpdateOldCustomerResponse.LastNameOldValue
                //    )
                //{
                //    tasks.Add(mediator.Publish<CustomerNameChangedIntegrationEvent>(new CustomerNameChangedIntegrationEvent()
                //    {
                //        CustomerIdentity = notification?.UpdateCustomerResponse?.UpdateNewCustomerResponse?.CustomerIdentity,
                //        FirstName = notification?.UpdateCustomerResponse?.UpdateNewCustomerResponse?.FirstName,
                //        LastName = notification.UpdateCustomerResponse.UpdateNewCustomerResponse?.LastName
                //    }));
                //}

                //if (notification.UpdateCustomerResponse.UpdateNewCustomerResponse.MobileNo != notification.UpdateCustomerResponse.UpdateOldCustomerResponse.MobileNoOldValue)
                //{
                //    tasks.Add(mediator.Publish<CustomerMobileNumberChangedIntegrationEvent>(new CustomerMobileNumberChangedIntegrationEvent()
                //    {
                //        CustomerIdentity = notification?.UpdateCustomerResponse?.UpdateNewCustomerResponse?.CustomerIdentity,
                //        MobileNo = notification?.UpdateCustomerResponse?.UpdateNewCustomerResponse?.MobileNo
                //    }));
                //}

                //await Task.WhenAll(tasks);

                await
                     eventStateContext
                     .AddNotification<CustomerUpdatedDomainEvent>(
                        (customerUpdateDomainEvent) =>
                             (
                                 customerUpdateDomainEvent.UpdateCustomerResponse.UpdateNewCustomerResponse.FirstName != customerUpdateDomainEvent.UpdateCustomerResponse.UpdateOldCustomerResponse.FirstNameOldValue
                                 ||
                                 customerUpdateDomainEvent.UpdateCustomerResponse.UpdateNewCustomerResponse.LastName != customerUpdateDomainEvent.UpdateCustomerResponse.UpdateOldCustomerResponse.LastNameOldValue
                             ),
                         new CustomerNameChangedIntegrationEvent()
                         {
                             CustomerIdentity = notification?.UpdateCustomerResponse?.UpdateNewCustomerResponse?.CustomerIdentity,
                             FirstName = notification?.UpdateCustomerResponse?.UpdateNewCustomerResponse?.FirstName,
                             LastName = notification.UpdateCustomerResponse.UpdateNewCustomerResponse?.LastName
                         }
                        )
                     .AddNotification<CustomerUpdatedDomainEvent>(
                         (customerUpdateDomainEvent) =>
                            (
                                customerUpdateDomainEvent.UpdateCustomerResponse.UpdateNewCustomerResponse.MobileNo != customerUpdateDomainEvent.UpdateCustomerResponse.UpdateOldCustomerResponse.MobileNoOldValue
                            ),
                            new CustomerMobileNumberChangedIntegrationEvent()
                            {
                                CustomerIdentity = notification?.UpdateCustomerResponse?.UpdateNewCustomerResponse?.CustomerIdentity,
                                MobileNo = notification?.UpdateCustomerResponse?.UpdateNewCustomerResponse?.MobileNo
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