using AutoMapper;
using Customer.Command.Api.Appplications.Features;
using Customer.Command.Api.Infrastructures.Repositories;
using Customer.Shared.DTO.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Customer.Command.Api.Appplications.Mappers
{
    public class CustomerMapperProfile : Profile
    {
        public CustomerMapperProfile()
        {
            CreateCustomerCommandSourceToCreateCustomerRepositoryDestination();
            CreateCustomerResponseDTOSourceToCustomerRequestDTODestination();
            UpdateCustomerCommandSourceToUpdateCustomerRepositoryDestination();
            RemoveCustomerCommandSourceToRemoveCustomerRepositoryDestination();
        }

        private void CreateCustomerCommandSourceToCreateCustomerRepositoryDestination()
        {
            base.CreateMap<CreateCustomerCommand, CreateCustomerRepository>();
        }

        private void CreateCustomerResponseDTOSourceToCustomerRequestDTODestination()
        {
            base.CreateMap<CreateCustomerResponseDTO, CustomerRequestDTO>();
        }

        private void UpdateCustomerCommandSourceToUpdateCustomerRepositoryDestination()
        {
            base.CreateMap<UpdateCustomerCommand, UpdateCustomerRepository>();
        }

        private void RemoveCustomerCommandSourceToRemoveCustomerRepositoryDestination()
        {
            base.CreateMap<RemoveCustomerCommand, RemoveCustomerRepository>();
        }
    }
}