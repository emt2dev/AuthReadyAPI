using AuthReadyAPI.DataLayer.DTOs.Food;

namespace AuthReadyAPI.DataLayer.Interfaces
{
    public interface IFoodRepository
    {
        public Task<bool> NewFoodProduct(NewFoodProductDTO DTO);
        public Task<bool> NewFoodCart(int FoodCartId);
        public Task<bool> UpdateFoodCart(FoodCartDTO DTO);
        public Task<bool> DeleteFoodProduct(int Id);
        public Task<List<FoodProductDTO>> GetAPIFoodProducts();
        public Task<List<FoodProductDTO>> GetCompanyFoodProducts(int CompanyId);
    }
}
