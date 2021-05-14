using AutoMapper;
using Customer.Command.Api.Appplications.IntegrationEvents;
using Customer.Command.Api.Infrastructures.Repositories;
using Customer.Shared.DTO.Responses;
using Framework.HangFire.MediatR.Extension;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace Customer.Command.Api.Appplications.Features
{
    [DataContract]
    public class RemoveCustomerCommand : IRequest<bool>
    {
        [DataMember(EmitDefaultValue = false)]
        public Guid? CustomerIdentity { get; set; }
    }

    public sealed class RemoveCustomerCommandHandler : IRequestHandler<RemoveCustomerCommand, bool>
    {
        private readonly IMediator mediator = null;
        private readonly IMapper mapper = null;

        public RemoveCustomerCommandHandler(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        async Task<bool> IRequestHandler<RemoveCustomerCommand, bool>.Handle(RemoveCustomerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                bool flag =
                     await mediator.Send<bool>(mapper.Map<RemoveCustomerRepository>(request));

                if (flag)
                {
                    mediator.Enqueue(new CustomerRemovedIntegrationEvent()
                    {
                        CustomerIdentity = request.CustomerIdentity
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