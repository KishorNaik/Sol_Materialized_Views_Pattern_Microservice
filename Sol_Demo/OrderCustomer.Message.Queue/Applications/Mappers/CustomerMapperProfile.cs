using AutoMapper;
using Customer.Message.Requests;
using OrderCustomer.Message.Queue.Infrastructures.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderCustomer.Message.Queue.Applications.Mappers
{
    public class CustomerMapperProfile : Profile
    {
        public CustomerMapperProfile()
        {
            base.CreateMap<CustomerMessageRequest, CustomerCreatedRepository>();
        }
    }
}