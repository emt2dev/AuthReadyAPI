using AuthReadyAPI.DataLayer.DTOs.Food;
using AuthReadyAPI.DataLayer.Interfaces;

namespace AuthReadyAPI.DataLayer.Repositories
{
    public class FoodRepository : IFoodRepository
    {
        public Task<bool> DeleteFoodProduct(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<List<FoodProductDTO>> GetAPIFoodProducts()
        {
            throw new NotImplementedException();
        }

        public Task<List<FoodProductDTO>> GetCompanyFoodProducts(int CompanyId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> NewFoodCart(int FoodCartId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> NewFoodProduct(NewFoodProductDTO DTO)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateFoodCart(FoodCartDTO DTO)
        {
            throw new NotImplementedException();
        }
    }
}
