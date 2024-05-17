using AuthReadyAPI.DataLayer.DTOs.PII.APIUser;
using AuthReadyAPI.DataLayer.Models.PII;
using AutoMapper;

namespace AuthReadyAPI.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<APIUserClass, APIUserDTO>().ReverseMap();
            CreateMap<APIUserClass, NewUserDTO>().ReverseMap();
        }
    }
}
