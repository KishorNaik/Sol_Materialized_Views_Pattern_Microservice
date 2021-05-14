using AutoMapper;
using Customer.Command.Api.Appplications.IntegrationEvents;
using Customer.Command.Api.Infrastructures.Repositories;
using Customer.Shared.DTO.Requests;
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
    public class CreateCustomerCommand : IRequest<bool>
    {
        [DataMember(EmitDefaultValue = false)]
        public String FirstName { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public String LastName { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public String MobileNo { get; set; }
    }

    public sealed class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, bool>
    {
        private readonly IMediator mediator = null;
        private readonly IMapper mapper = null;

        public CreateCustomerCommandHandler(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        async Task<bool> IRequestHandler<CreateCustomerCommand, bool>.Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                CreateCustomerResponseDTO createCustomerResponse = (await mediator.Send<ICreateCustomerResponseDTO>(mapper.Map<CreateCustomerRepository>(request))) as CreateCustomerResponseDTO;

                if (createCustomerResponse != null)
                {
                    mediator.Enqueue(new CustomerCreatedIntegrationEvent()
                    {
                        Customer = mapper.Map<CustomerRequestDTO>(createCustomerResponse)
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