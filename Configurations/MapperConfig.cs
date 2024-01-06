using AuthReadyAPI.DataLayer.DTOs.Company;
using AuthReadyAPI.DataLayer.DTOs.Food;
using AuthReadyAPI.DataLayer.DTOs.PII;
using AuthReadyAPI.DataLayer.DTOs.PII.APIUser;
using AuthReadyAPI.DataLayer.DTOs.PII.Payments;
using AuthReadyAPI.DataLayer.DTOs.Product;
using AuthReadyAPI.DataLayer.DTOs.Services;
using AuthReadyAPI.DataLayer.Models.Companies;
using AuthReadyAPI.DataLayer.Models.FoodInfo;
using AuthReadyAPI.DataLayer.Models.PII;
using AuthReadyAPI.DataLayer.Models.ProductInfo;
using AuthReadyAPI.DataLayer.Models.ServicesInfo;
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
            CreateMap<APIUserClass, NewUserDTO>().ReverseMap();
            CreateMap<CompanyClass, CompanyDTO>().ReverseMap();
            CreateMap<CompanyClass, NewCompanyDTO>().ReverseMap();
            CreateMap<PointOfContactClass, PointOfContactDTO>().ReverseMap();
            CreateMap<PointOfContactClass, NewPointOfContactDTO>().ReverseMap();
            CreateMap<ProductClass, ProductDTO>().ReverseMap();
            CreateMap<ProductClass, NewProductDTO>().ReverseMap();
            CreateMap<ShoppingCartClass, ShoppingCartDTO>().ReverseMap();
            CreateMap<OrderClass, OrderDTO>().ReverseMap();
            CreateMap<CategoryClass, CategoryDTO>().ReverseMap();
            CreateMap<CategoryClass, NewCategoryDTO>().ReverseMap();
            CreateMap<CartItemClass, CartItemDTO>().ReverseMap();
            CreateMap<BidClass, BidDTO>().ReverseMap();
            CreateMap<AuctionProductCartClass, AuctionProductCartDTO>().ReverseMap();
            CreateMap<ServicesClass, ServicesDTO>().ReverseMap();
            CreateMap<ProductUpsellItemClass, ProductUpsellItemDTO>().ReverseMap();
            CreateMap<AppointmentClass, AppointmentDTO>().ReverseMap();
            CreateMap<ServicesCartClass, ServicesCartDTO>().ReverseMap();
            CreateMap<ServiceProductClass, ServiceProductDTO>().ReverseMap();
            CreateMap<ProductWithStyleClass, ProductWithStyleDTO>().ReverseMap();
            CreateMap<FoodCartClass, FoodCartDTO>().ReverseMap();
            CreateMap<FoodProductClass, FoodProductDTO>().ReverseMap();
            CreateMap<FoodOrderClass, FoodOrderDTO>().ReverseMap();
        }
    }
}
