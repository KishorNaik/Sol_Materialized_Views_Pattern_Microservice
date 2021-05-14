using AutoMapper;
using OrderProduct.Message.Queue.Infrastructures.Repositories;
using Product.Message.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderProduct.Message.Queue.Applications.Mappers
{
    public class OrderProductMapperProfile : Profile
    {
        public OrderProductMapperProfile()
        {
            ProductMessageRequestSourceToProductCreatedRepositoryDestination();
        }

        private void ProductMessageRequestSourceToProductCreatedRepositoryDestination()
        {
            base.CreateMap<ProductMessageRequest, ProductCreatedRepository>();
        }
    }
}