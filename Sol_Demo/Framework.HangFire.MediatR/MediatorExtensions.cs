using Hangfire;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Framework.HangFire.MediatR.Extension
{
    public static class MediatorExtensions
    {
        public static void Enqueue(this IMediator mediator, string jobName, IRequest request)
        {
            var client = new BackgroundJobClient();
            client.Enqueue<MediatorHangFireMessageBridge>(bridge => bridge.Send(jobName, request));
        }

        public static void Enqueue(this IMediator mediator, IRequest request)
        {
            var client = new BackgroundJobClient();
            client.Enqueue<MediatorHangFireMessageBridge>(bridge => bridge.Send(request));
        }

        public static void Enqueue(this IMediator mediator, string jobName, INotification notification)
        {
            var client = new BackgroundJobClient();
            client.Enqueue<MediatorHangFireMessageBridge>(bridge => bridge.Publish(jobName, notification));
        }

        public static void Enqueue(this IMediator mediator, INotification notification)
        {
            var client = new BackgroundJobClient();
            client.Enqueue<MediatorHangFireMessageBridge>(bridge => bridge.Publish(notification));
        }

        public static void Schedule(this IMediator mediator, INotification notification, TimeSpan timeSpan)
        {
            var client = new BackgroundJobClient();
            client.Schedule<MediatorHangFireMessageBridge>(bridge => bridge.Publish(notification), timeSpan);
        }

        public static void Schedule(this IMediator mediator, IRequest request, TimeSpan timeSpan)
        {
            var client = new BackgroundJobClient();
            client.Schedule<MediatorHangFireMessageBridge>(bridge => bridge.Send(request), timeSpan);
        }
    }
}