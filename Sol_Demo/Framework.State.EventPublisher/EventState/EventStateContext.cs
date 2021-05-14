using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.State.EventPublisher.EventState
{
    public interface IEventStateContext
    {
        IEventStateContext AddNotification<TEvent>(Func<TEvent, bool> condition, INotification notification);

        Task PublishNotification<TEvent>(TEvent @event);
    }

    public class EventStateContext : IEventStateContext
    {
        private List<dynamic> stateDatas = new();

        private readonly IMediator mediator = null;

        public EventStateContext(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public IEventStateContext AddNotification<TEvent>(Func<TEvent, bool> condition, INotification notification)
        {
            stateDatas.Add(new StateData<TEvent>()
            {
                Condition = condition,
                Notification = notification
            });

            return this;
        }

        public async Task PublishNotification<TEvent>(TEvent @event)
        {
            var notificationList =
                    stateDatas
                    .Where((stateData) => ((StateData<TEvent>)stateData).Condition.Invoke(@event) == true)
                    .Select((stateData) => ((StateData<TEvent>)stateData).Notification);

            var tasks =
                    notificationList
                    .Select((notification) => mediator.Publish(notification));

            await Task.WhenAll(tasks);
        }

        private class StateData<TEvent>
        {
            public Func<TEvent, bool> Condition { get; set; }
            public INotification Notification { get; set; }
        }
    }
}