using AutoMapper;
using Sakani.Contracts.Dto;
using Sakani.Data.Models;

namespace Sakani.Services.Profiles
{
    public class ApplicationUserMapping : Profile
    {
        public ApplicationUserMapping()
        {
            CreateMap<RegisterDto, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.password))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));
        }
    }
}
