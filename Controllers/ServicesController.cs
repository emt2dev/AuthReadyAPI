using AuthReadyAPI.DataLayer.DTOs.PII.Payments;
using AuthReadyAPI.DataLayer.DTOs.Product;
using AuthReadyAPI.DataLayer.DTOs.Services;
using AuthReadyAPI.DataLayer.Interfaces;
using AuthReadyAPI.DataLayer.Models.PII;
using AuthReadyAPI.DataLayer.Models.ServicesInfo;
using AuthReadyAPI.DataLayer.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace AuthReadyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly IServicesRepository _services;
        private readonly IAuthRepository _authRepository;
        private readonly UserManager<APIUserClass> _userManager;
        APIUserClass _user;

        public ServicesController(IServicesRepository services, UserManager<APIUserClass> userManager, IAuthRepository authRepository)
        {
            _services = services;
            _authRepository = authRepository;
            _userManager = userManager;
        }

        // This controller is for service companies
        [HttpGet]
        [Route("list/all")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<ServicesDTO>> GetCompanyServices(int CompanyId)
        {
            return await _services.GetCompanyServicesOffered(CompanyId);
        }

        [HttpPost]
        [Route("list/category/{Description}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<ServicesDTO>> FindCompanyServices([FromBody] string Description, int CompanyId)
        {
            return await _services.FindCompanyServicesOffered(CompanyId, Description);
        }

        [HttpPost]
        [Route("new")]
        [Authorize(Roles = "Company")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<bool> AddService([FromBody] NewServicesDTO DTO, int CompanyId)
        {
            string Token = (string)HttpContext.Request.Headers["Authorization"];
            Token = Token.Replace("Bearer ", "");

            _user = await _userManager.FindByIdAsync(await _authRepository.ReadUserId(Token));
            if (_user is null) return false;

            return await _services.AddService(DTO);
        }

        [HttpPut]
        [Route("update")]
        [Authorize(Roles = "Company")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<bool> UpdateOffering([FromBody] ServicesDTO DTO)
        {
            string Token = (string)HttpContext.Request.Headers["Authorization"];
            Token = Token.Replace("Bearer ", "");

            _user = await _userManager.FindByIdAsync(await _authRepository.ReadUserId(Token));
            if (_user is null) return false;
            return await _services.UpdateService(DTO);
        }

        [HttpDelete]
        [Route("delete")]
        [Authorize(Roles = "Company")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<bool> DeleteOffering([FromBody] int ServiceId)
        {
            string Token = (string)HttpContext.Request.Headers["Authorization"];
            Token = Token.Replace("Bearer ", "");

            _user = await _userManager.FindByIdAsync(await _authRepository.ReadUserId(Token));
            if (_user is null) return false;

            return await _services.DeleteService(ServiceId);
        }
    }
}
