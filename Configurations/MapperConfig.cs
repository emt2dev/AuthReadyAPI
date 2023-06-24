using AuthReadyAPI.DataLayer.DTOs.APIUser;
using AuthReadyAPI.DataLayer.Models;
using AutoMapper;

namespace AuthReadyAPI.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            /* APIUser and DTO */
            CreateMap<APIUser, Full__APIUser>().ReverseMap();
            CreateMap<APIUser, Base__APIUser>().ReverseMap();
            
        }
        
    }
}
