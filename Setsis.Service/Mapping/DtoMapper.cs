using AutoMapper;

using Setsis.Core.Models;
using Setsis.Service.Services.User.Dto.Result;

namespace Setsis.Service.Mapping
{
    internal class DtoMapper : Profile
    {
        public DtoMapper()
        {
            CreateMap<ApplicationUserDto, ApplicationUser>().ReverseMap();
        }
    }
}
