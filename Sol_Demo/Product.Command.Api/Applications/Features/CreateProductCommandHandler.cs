using AutoMapper;
using Framework.HangFire.MediatR.Extension;
using MediatR;
using Product.Command.Api.Applications.IntegrationEvent;
using Product.Command.Api.Infrastructures.Repositories;
using Product.Shared.DTO;
using Product.Shared.DTO.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace Product.Command.Api.Applications.Features
{
    [DataContract]
    public class CreateProductCommand : IRequest<bool>
    {
        [DataMember(EmitDefaultValue = false)]
        public String ProductName { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public double? UnitPrice { get; set; }
    }

    public sealed class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, bool>
    {
        private readonly IMediator mediator = null;

        private readonly IMapper mapper = null;

        public CreateProductCommandHandler(IMediator mediator, IMapper mapper)
        {
            this.mapper = mapper;
            this.mediator = mediator;
        }

        async Task<bool> IRequestHandler<CreateProductCommand, bool>.Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                CreateProductResponseDTO CreateProductResponseDTO = (await mediator.Send<ICreateProductResponseDTO>(mapper.Map<CreateProductRepository>(request))) as CreateProductResponseDTO;

                if (CreateProductResponseDTO != null)
                {
                    mediator.Enqueue(new ProductCreatedIntegrationEvent()
                    {
                        Product = CreateProductResponseDTO
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