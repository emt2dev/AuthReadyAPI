using AuthReadyAPI.DataLayer.DTOs.Company;
using AuthReadyAPI.DataLayer.DTOs.PII;
using AuthReadyAPI.DataLayer.DTOs.PII.APIUser;
using AuthReadyAPI.DataLayer.DTOs.PII.Payments;
using AuthReadyAPI.DataLayer.DTOs.Product;
using AuthReadyAPI.DataLayer.Models.Companies;
using AuthReadyAPI.DataLayer.Models.PII;
using AuthReadyAPI.DataLayer.Models.ProductInfo;
using AutoMapper;

namespace AuthReadyAPI.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<DigitalOwnershipClass, DigitalOwnershipDTO>().ReverseMap();
            CreateMap<ShippingInfoClass, ShippingInfoDTO>().ReverseMap();
            CreateMap<StyleClass, StyleDTO>().ReverseMap();
            CreateMap<StyleClass, NewStyleDTO>().ReverseMap();
            CreateMap<APIUserClass, APIUserDTO>().ReverseMap();
            CreateMap<CompanyClass, CompanyDTO>().ReverseMap();
            CreateMap<ProductClass, ProductDTO>().ReverseMap();
            CreateMap<ProductClass, NewProductDTO>().ReverseMap();
            CreateMap<ShoppingCartClass, ShoppingCartDTO>().ReverseMap();
            CreateMap<OrderClass, OrderDTO>().ReverseMap();
            CreateMap<CategoryClass, CategoryDTO>().ReverseMap();
            CreateMap<CategoryClass, NewCategoryDTO>().ReverseMap();
        }
        
    }
}
