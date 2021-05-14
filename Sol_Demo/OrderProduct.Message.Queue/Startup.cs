using Framework.RabbitMQ.Extension;
using Framework.SqlClient.Extensions;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using OrderProduct.Message.Queue.Applications.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderProduct.Message.Queue
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddMediatR(typeof(Startup));
            services.AddAutoMapper(typeof(Startup));

            services.AddSqlProvider(Configuration.GetConnectionString("DefaultConnection"));

            services.AddRabbitMQService("rabbitmq://localhost", "guest", "guest",
                addConsumer: (config) =>
                {
                    config.AddConsumer<ProductCreatedMessageConsmerHandler>();
                    config.AddConsumer<ProductNameChangedMessageConsumerHandler>();
                    config.AddConsumer<ProductUnitPriceChangedMessageConsumerHandler>();
                    config.AddConsumer<ProductRemovedMessageConsumerHandler>();
                },
                receiveEndPoints: (endPoint, busFactory) =>
                {
                    endPoint.ReceiveEndpoint("product-created-event-queue", (configReceiveEndPoint) =>
                    {
                        configReceiveEndPoint.ConfigureConsumer<ProductCreatedMessageConsmerHandler>(busFactory);
                    });
                    endPoint.ReceiveEndpoint("product-name-changed-event-queue", (configReceiveEndPoint) =>
                    {
                        configReceiveEndPoint.ConfigureConsumer<ProductNameChangedMessageConsumerHandler>(busFactory);
                    });
                    endPoint.ReceiveEndpoint("product-unitprice-changed-event-queue", (configReceiveEndPoint) =>
                    {
                        configReceiveEndPoint.ConfigureConsumer<ProductUnitPriceChangedMessageConsumerHandler>(busFactory);
                    });
                    endPoint.ReceiveEndpoint("product-removed-event-queue", (configReceiveEndPoint) =>
                    {
                        configReceiveEndPoint.ConfigureConsumer<ProductRemovedMessageConsumerHandler>(busFactory);
                    });
                }
                );

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "OrderProduct.Message.Queue", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OrderProduct.Message.Queue v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}