using AutoMapper;
using MediatR;
using Order.Command.Api.Infrastructures.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace Order.Command.Api.Applications.Features
{
    [DataContract]
    public class CreateOrderCommand : IRequest<String>
    {
        [DataMember(EmitDefaultValue = false)]
        public Guid? CustomerIdentity { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Guid? ProductIdentity { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int? Quantity { get; set; }
    }

    public sealed class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, String>
    {
        private readonly IMediator mediator = null;
        private readonly IMapper mapper = null;

        public CreateOrderCommandHandler(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        async Task<string> IRequestHandler<CreateOrderCommand, string>.Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                bool flag = await mediator.Send<bool>(mapper.Map<CreateOrderRepository>(request));

                return (flag == true) ? "Your Order created successfully" : "Something went wrong..";
            }
            catch
            {
                throw;
            }
        }
    }
}