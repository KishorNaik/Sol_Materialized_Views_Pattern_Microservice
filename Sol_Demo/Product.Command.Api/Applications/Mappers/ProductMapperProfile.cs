using AutoMapper;
using Product.Command.Api.Applications.Features;
using Product.Command.Api.Infrastructures.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Command.Api.Applications.Mappers
{
    public class ProductMapperProfile : Profile
    {
        public ProductMapperProfile()
        {
            CreateProductCommandSourceToCreateProductRepositoryDestination();
            UpdateProductCommandSourceToUpdateProductRepositoryDestination();
        }

        private void CreateProductCommandSourceToCreateProductRepositoryDestination()
        {
            base.CreateMap<CreateProductCommand, CreateProductRepository>();
        }

        private void UpdateProductCommandSourceToUpdateProductRepositoryDestination()
        {
            base.CreateMap<UpdateProductCommand, UpdateProductRepository>();
        }
    }
}