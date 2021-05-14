using MassTransit;
using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Customer.Command.Api.Appplications.DomainEvents.State
{
    public class CustomerUpdateStateContext
    {
        private List<StateData> stateDatas = new();

        private readonly IMediator mediator = null;

        public CustomerUpdateStateContext(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public CustomerUpdateStateContext AddNotification(Func<CustomerUpdatedDomainEvent, bool> condition, INotification notification)
        {
            stateDatas.Add(new StateData()
            {
                Condition = condition,
                Notification = notification
            });

            return this;
        }

        public async Task PublishNotification(CustomerUpdatedDomainEvent customerUpdatedDomainEvent)
        {
            var notificationList =
                    stateDatas
                    .Where((stateData) => stateData.Condition.Invoke(customerUpdatedDomainEvent) == true)
                    .Select((stateData) => stateData.Notification);

            var tasks =
                    notificationList
                    .Select((notification) => mediator.Publish(notification));

            await Task.WhenAll(tasks);
        }

        private class StateData
        {
            public Func<CustomerUpdatedDomainEvent, bool> Condition { get; set; }
            public INotification Notification { get; set; }
        }
    }
}