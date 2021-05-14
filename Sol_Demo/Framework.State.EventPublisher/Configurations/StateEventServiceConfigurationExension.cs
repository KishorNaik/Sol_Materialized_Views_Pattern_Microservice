using Framework.State.EventPublisher.EventState;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.State.EventPublisher.Configurations
{
    public static class StateEventServiceConfigurationExension
    {
        public static void AddStateEvent(this IServiceCollection services, Func<IServiceProvider, IMediator> config)
        {
            services.AddTransient<IEventStateContext, EventStateContext>((innerConfig) =>
            {
                IMediator mediator = config(innerConfig);
                return new EventStateContext(mediator);
            });
        }
    }
}