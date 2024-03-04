using AutoMapper;
using Setsis.Core.Models;
using Setsis.Infrastructure.CQRS.Commands.Categories.Request;
using Setsis.Infrastructure.CQRS.Commands.Products.Request;
using Setsis.Infrastructure.CQRS.Queries.Category.Response;
using Setsis.Infrastructure.CQRS.Queries.Product.Response;

namespace Setsis.Api.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Product, GetProductQueryResponse>().ReverseMap();
            CreateMap<Product, CreateProductCommandRequest>().ReverseMap();
            CreateMap<Product, UpdateProductCommandRequest>().ReverseMap();
            CreateMap<Category, GetCategoryQueryResponse>().ReverseMap();     
            CreateMap<Category, CreateCategoryCommandRequest>().ReverseMap();
            CreateMap<Category, UpdateCategoryCommandRequest>().ReverseMap();
        }
    }
}
