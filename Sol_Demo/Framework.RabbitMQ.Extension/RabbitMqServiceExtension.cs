using MassTransit;
using MassTransit.ExtensionsDependencyInjectionIntegration;
using MassTransit.RabbitMqTransport;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Framework.RabbitMQ.Extension
{
    public static class RabbitMqServiceExtension
    {
        public static void AddRabbitMQService(this IServiceCollection services,
            string uri, string userName, string password,
            Action<IServiceCollectionBusConfigurator> addConsumer = null,
            Action<IRabbitMqBusFactoryConfigurator, IBusRegistrationContext> receiveEndPoints = null
            )
        {
            services.AddMassTransit((config) =>
            {
                if (addConsumer != null)
                {
                    addConsumer?.Invoke(config);
                }

                config.AddBus((busFactory) => Bus.Factory.CreateUsingRabbitMq((configRabbitMq) =>
                {
                    configRabbitMq.UseHealthCheck(busFactory);
                    configRabbitMq.Host(new Uri("rabbitmq://localhost"), (configHost) =>
                    {
                        configHost.Username("guest");
                        configHost.Password("guest");
                    }
                   );

                    if (receiveEndPoints != null)
                    {
                        receiveEndPoints?.Invoke(configRabbitMq, busFactory);
                    }
                }));
            });
            services.AddMassTransitHostedService();
        }
    }
}