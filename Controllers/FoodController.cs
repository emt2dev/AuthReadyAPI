using AuthReadyAPI.DataLayer.DTOs.Food;
using AuthReadyAPI.DataLayer.DTOs.Product;
using AuthReadyAPI.DataLayer.Interfaces;
using AuthReadyAPI.DataLayer.Models.PII;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuthReadyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodController : ControllerBase
    {
        private readonly IFoodRepository _food;
        private readonly IAuthRepository _authRepository;
        private readonly UserManager<APIUserClass> _userManager;
        APIUserClass _user;
        public FoodController(IFoodRepository food, UserManager<APIUserClass> userManager, IAuthRepository authRepository)
        {
            _food = food;
            _userManager = userManager;
            _authRepository = authRepository;
        }

        [HttpGet]
        [Route("all")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<FoodProductDTO>> GetAllAPIFoodProducts()
        {
            return await _food.GetAPIFoodProducts();
        }

        [HttpGet]
        [Route("company/all/{CompanyId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<FoodProductDTO>> GetAllCompanyFoodProducts([FromRoute] int CompanyId)
        {
            return await _food.GetCompanyFoodProducts(CompanyId);
        }

        [HttpPost]
        [Route("company/new")]
        [Authorize(Roles = "Company")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<bool> NewFoodProduct([FromBody] NewFoodProductDTO DTO)
        {
            return await _food.NewFoodProduct(DTO);
        }

        [HttpPost]
        [Route("update/cart")]
        [Authorize(Roles = "User")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<bool> UpdateCart([FromBody] FoodCartDTO DTO)
        {
            return await _food.UpdateFoodCart(DTO);
        }

        [HttpPost]
        [Route("company/delete/{ProductId}")]
        [Authorize(Roles = "Company")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<bool> DeleteFoodProduct([FromRoute] int ProductId)
        {
            return await _food.DeleteFoodProduct(ProductId);
        }
    }
}
