using AuthReadyAPI.DataLayer.DTOs.APIUser;
using AuthReadyAPI.DataLayer.DTOs.AuthResponse;
using AuthReadyAPI.DataLayer.DTOs.Cart;
using AuthReadyAPI.DataLayer.DTOs.Company;
using AuthReadyAPI.DataLayer.DTOs.Order;
using AuthReadyAPI.DataLayer.DTOs.Product;
using AuthReadyAPI.DataLayer.Interfaces;
using AuthReadyAPI.DataLayer.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;

namespace AuthReadyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize(Roles = ("API_Admin"))]
    public class AdminController : ControllerBase
    {
        private readonly ICompany _company;
        private readonly IUser _user;
        private readonly IProduct _product;
        private readonly ICart _cart;
        private readonly IOrder _order;
        private readonly IApiAdmin _apiAdmin;


        private readonly ILogger<AuthController> _LOGS;
        private readonly IAuthManager _IAM;
        private readonly IMapper _mapper;

        private readonly UserManager<APIUser> _UM;

        public AdminController(ICompany company, IUser user, IProduct product, ICart cart, IOrder order, IApiAdmin apiAdmin, ILogger<AuthController> LOGS, IAuthManager IAM, IMapper mapper, UserManager<APIUser> UM)
        {
            this._company = company;
            this._LOGS = LOGS;
            this._IAM = IAM;
            this._mapper = mapper;
            this._UM = UM;
            this._product = product;
            this._cart = cart;
            this._order = order;
            this._apiAdmin = apiAdmin;
            this._user = user;

        }

        [HttpPost]
        [Route("admin__create")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<ActionResult> CREATE__API__ADMIN(Base__APIUser DTO)
        {
           var errors = await _IAM.API__ADMIN__REGISTER(DTO);

            if (errors.Any())
            {
                foreach (var error in errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }

                _LOGS.LogInformation($"Failed Register Attempt for {DTO.Email}");

                return BadRequest(ModelState);
            }

            return Ok();
        }

        // api/admin/company__create 
        [HttpPost]
        [Route("company__create")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<string> CREATE__COMPANY(Base__Company DTO)
        {
           Base__Company createdCompany = await _apiAdmin.COMPANY__CREATE(DTO);

            return createdCompany.Name + " was created";
        }

        [HttpPost]
        [Route("company__admin__override")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<string> OVERRIDE__COMPANY__ADMIN(overrideDTO DTO)
        {
            APIUser userGivenPrivledges = await _UM.FindByEmailAsync(DTO.userEmail);
            userGivenPrivledges.CompanyId = DTO.companyId;
            userGivenPrivledges.IsStaff = true;
            await _UM.AddToRoleAsync(userGivenPrivledges, "Company_Admin");
            
            Company companyOverriddenAdmin = await _company.GetAsyncById(DTO.companyId);
            if(DTO.replaceAdminOneOrTwo == 1) companyOverriddenAdmin.Id_admin_one = userGivenPrivledges.Id;
            else companyOverriddenAdmin.Id_admin_two = userGivenPrivledges.Id;
            await _company.UpdateAsync(companyOverriddenAdmin);

            return DTO.userEmail + " replaced existing admin " + DTO.replaceAdminOneOrTwo + "for company: " + DTO.companyId;
        }

        [HttpGet]
        [Route("init")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<string> init()
        {
            Base__APIUser newCustomer = new Base__APIUser {
                Email = "customer1@customer.com",
                Password = "P@ssword1",
            };

            _ = await _IAM.USER__REGISTER(newCustomer);

            Base__APIUser newAdmin = new Base__APIUser {
                Email = "admin100@admin.com",
                Password = "P@ssword1",
            };

            _ = await _IAM.API__ADMIN__REGISTER(newAdmin);

            Base__Company newCompany = new Base__Company {
                Id = "0",
                Name = "La Imperial Bakery",
                Description = "A Puerto Rican Bakery",
                Address = "123 E Main St, Lakeland, FL 33801",
                PhoneNumber = "863-500-4411",
            };

            newCompany = await _apiAdmin.COMPANY__CREATE(newCompany);

            return newCompany.Id;
        }

    }
}
