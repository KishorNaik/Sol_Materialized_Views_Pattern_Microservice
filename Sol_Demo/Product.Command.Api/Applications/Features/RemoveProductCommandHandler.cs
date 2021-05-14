using Framework.HangFire.MediatR.Extension;
using MediatR;
using Product.Command.Api.Applications.IntegrationEvent;
using Product.Command.Api.Infrastructures.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace Product.Command.Api.Applications.Features
{
    [DataContract]
    public class RemoveProductCommand : IRequest<bool>
    {
        [DataMember(EmitDefaultValue = false)]
        public Guid? ProductIdentity { get; set; }
    }

    public sealed class RemoveProductCommandHandler : IRequestHandler<RemoveProductCommand, bool>
    {
        private readonly IMediator mediator = null;

        public RemoveProductCommandHandler(IMediator mediator)
        {
            this.mediator = mediator;
        }

        async Task<bool> IRequestHandler<RemoveProductCommand, bool>.Handle(RemoveProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                bool flag = await mediator.Send<bool>(new RemoveProductRepository()
                {
                    ProductIdentity = request.ProductIdentity
                });

                if (flag)
                {
                    mediator.Enqueue(new ProductRemovedIntegrationEvent()
                    {
                        ProductIdentity = request.ProductIdentity
                    });
                    return true;
                }

                return false;
            }
            catch
            {
                throw;
            }
        }
    }
}