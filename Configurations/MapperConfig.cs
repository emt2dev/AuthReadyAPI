using AuthReadyAPI.DataLayer.DTOs.APIUser;
using AuthReadyAPI.DataLayer.DTOs.Cart;
using AuthReadyAPI.DataLayer.DTOs.Company;
using AuthReadyAPI.DataLayer.DTOs.Order;
using AuthReadyAPI.DataLayer.DTOs.Product;
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

            CreateMap<Company, Full__Company>().ReverseMap();
            CreateMap<Company, Base__Company>().ReverseMap();

            CreateMap<Product, Full__Product>().ReverseMap();
            CreateMap<Product, Base__Product>().ReverseMap();

            CreateMap<Cart, Full__Cart>().ReverseMap();
            CreateMap<Cart, Base__Cart>().ReverseMap();

            CreateMap<Order, Full__Order>().ReverseMap();
            CreateMap<Order, Base__Order>().ReverseMap();
        }
        
    }
}
