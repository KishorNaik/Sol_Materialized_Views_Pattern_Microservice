using AutoMapper;
using Framework.HangFire.MediatR.Extension;
using MediatR;
using Product.Command.Api.Applications.DomainEvents;
using Product.Command.Api.Infrastructures.Repositories;
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
    public class UpdateProductCommand : IRequest<bool>
    {
        [DataMember(EmitDefaultValue = false)]
        public Guid? ProductIdentity { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public String ProductName { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public double? UnitPrice { get; set; }
    }

    public sealed class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool>
    {
        private readonly IMediator mediator = null;
        private readonly IMapper mapper = null;

        public UpdateProductCommandHandler(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        async Task<bool> IRequestHandler<UpdateProductCommand, bool>.Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                UpdateProductResponseDTO updateProductResponse = await mediator.Send<UpdateProductResponseDTO>(mapper.Map<UpdateProductRepository>(request));

                if (updateProductResponse != null)
                {
                    mediator.Enqueue(new ProductUpdatedDomainEvent()
                    {
                        UpdateProductResponse = updateProductResponse
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