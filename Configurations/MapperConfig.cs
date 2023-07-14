using AuthReadyAPI.DataLayer.DTOs.APIUser;
using AuthReadyAPI.DataLayer.DTOs.Product;
using AuthReadyAPI.DataLayer.Models;
using AutoMapper;

namespace AuthReadyAPI.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            // V2 Product
            CreateMap<v2_ProductStripe, v2_ProductDTO>().ReverseMap();
            CreateMap<v2_ProductDTO, v2_newProductDTO>().ReverseMap();

            // V2 Company
            CreateMap<v2_Company, v2_CompanyDTO>().ReverseMap();

            // V2 scart
            CreateMap<v2_ShoppingCart, v2_ShoppingCartDTO>().ReverseMap();

            // V2 order
            CreateMap<v2_Order, v2_OrderDTO>().ReverseMap();

            // V2 customer
            CreateMap<v2_UserStripe, v2_CustomerDTO>().ReverseMap();

            // v2 staff
            CreateMap<v2_UserStripe, v2_StaffDTO>().ReverseMap();
            
        }
        
    }
}
