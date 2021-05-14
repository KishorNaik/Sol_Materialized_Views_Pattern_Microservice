using AutoMapper;
using Order.Command.Api.Applications.Features;
using Order.Command.Api.Infrastructures.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order.Command.Api.Applications.Mappers
{
    public class OrderMapperProfile : Profile
    {
        public OrderMapperProfile()
        {
            CreateOrderCommandSourceToCreateOrderRepositoryDestination();
        }

        private void CreateOrderCommandSourceToCreateOrderRepositoryDestination()
        {
            base.CreateMap<CreateOrderCommand, CreateOrderRepository>();
        }
    }
}