using AutoMapper;
using Customer.Command.Api.Appplications.DomainEvents;
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
    public class UpdateCustomerCommand : IRequest<bool>
    {
        [DataMember(EmitDefaultValue = false)]
        public Guid? CustomerIdentity { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public String FirstName { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public String LastName { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public String MobileNo { get; set; }
    }

    public sealed class UdpateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, bool>
    {
        private readonly IMediator mediator = null;
        private readonly IMapper mapper = null;

        public UdpateCustomerCommandHandler(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        async Task<bool> IRequestHandler<UpdateCustomerCommand, bool>.Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                UpdateCustomerResponseDTO updateCustomerResponse =
                    await mediator.Send<UpdateCustomerResponseDTO>(mapper.Map<UpdateCustomerRepository>(request));

                if (updateCustomerResponse != null)
                {
                    mediator.Enqueue(new CustomerUpdatedDomainEvent()
                    {
                        UpdateCustomerResponse = updateCustomerResponse
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