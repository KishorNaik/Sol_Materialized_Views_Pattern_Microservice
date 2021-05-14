using MediatR;
using Order.Query.Api.Infrastructures.Repositories;
using Order.Shared.DTO.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace Order.Query.Api.Applications.Features
{
    [DataContract]
    public class GetOrderHistoryQuery : IRequest<IReadOnlyList<OrderHistoryResponseDTO>>
    {
        [DataMember(EmitDefaultValue = false)]
        public Guid? CustomerIdentity { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public DateTime FromOrderDate { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public DateTime ToOrderDate { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int? PageNumber { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int? RowsOfPage { get; set; }
    }

    public sealed class GetOrderHistoryQueryHandler : IRequestHandler<GetOrderHistoryQuery, IReadOnlyList<OrderHistoryResponseDTO>>
    {
        private readonly IMediator mediator = null;

        public GetOrderHistoryQueryHandler(IMediator mediator)
        {
            this.mediator = mediator;
        }

        Task<IReadOnlyList<OrderHistoryResponseDTO>> IRequestHandler<GetOrderHistoryQuery, IReadOnlyList<OrderHistoryResponseDTO>>.Handle(GetOrderHistoryQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return mediator.Send<IReadOnlyList<OrderHistoryResponseDTO>>(new GetOrderHistoryRepository()
                {
                    CustomerIdentity = request.CustomerIdentity,
                    FromOrderDate = request.FromOrderDate,
                    ToOrderDate = request.ToOrderDate,
                    Pagination = new Shared.DTO.Requests.OrderPagination()
                    {
                        PageNumber = request.PageNumber,
                        RowsOfPage = request.RowsOfPage
                    }
                });
            }
            catch
            {
                throw;
            }
        }
    }
}