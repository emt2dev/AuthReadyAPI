using AuthReadyAPI.DataLayer.DTOs.Company;
using AuthReadyAPI.DataLayer.DTOs.Product;
using AuthReadyAPI.DataLayer.Interfaces;
using AuthReadyAPI.DataLayer.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System.ComponentModel.Design;

namespace AuthReadyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompany _company;
        private readonly IUser _user;
        private readonly IProduct _product;
        private readonly ICart _cart;
        private readonly IOrder _order;

        private readonly ILogger<AuthController> _LOGS;
        private readonly IAuthManager _IAM;
        private readonly IMapper _mapper;

        private readonly UserManager<APIUser> _UM;

        public CompanyController(ICompany company, IUser user, IProduct product, ICart cart, IOrder order, ILogger<AuthController> LOGS, IAuthManager IAM, IMapper mapper, UserManager<APIUser> UM)
        {
            this._company = company;
            this._LOGS = LOGS;
            this._IAM = IAM;
            this._mapper = mapper;
            this._UM = UM;
            this._product = product;
            this._cart = cart;
            this._order = order;
            this._user = user;

        }

                /* api/Company/details/ 
         * semantics, by not needed since this is going to be used by one company.
         * Can be add other companies to this.
         */

        [HttpGet]
        [Route("all")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<IList<Base__Company>> GET__ALL__COMPANY()
        {
            IList<Base__Company> companies = new List<Base__Company>();
            IList<Full__Company> companiesDTList = new List<Full__Company>();

            companies = await _company.GetAllAsync<Base__Company>();
            // companiesDTList = _mapper.Map<IList<Full__Company>>(companies);

            return companies;
        }

        /* api/Company/details/ 
         * semantics, by not needed since this is going to be used by one company.
         * Can be add other companies to this.
         */

        [HttpGet]
        [Route("details/{companyId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<Full__Company> GET__COMPANY__DETAILS(int? companyId)
        {
            
            Company searchedFor = await _company.GetAsyncById(companyId);

            Full__Company DTO = _mapper.Map<Full__Company>(searchedFor);

            return DTO;
        }

        // api/Company/new__admin/ 
        [HttpPost]
        [Route("new__admin")]
        [Authorize(Roles = ("API_Admin, Company_Admin"))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<IList<string>> GIVE__ADMIN__PRIV(string userEmail, int companyId, int adminOneOrTwo)
        {
            if (userEmail == null) return null;
            if (adminOneOrTwo < 1 || adminOneOrTwo > 2) return null;

            Company companyGiven = await _company.GetAsyncById(companyId);

            APIUser userGiven = await _user.USER__FIND__BY__EMAIL__ASYNC(userEmail);

            // here we assign new values to the entity
            userGiven.IsStaff = true;
            userGiven.CompanyId = companyGiven.Id;

            _ = await _UM.AddToRoleAsync(userGiven, "Company_Admin");
            _ = _user.UpdateAsync(userGiven);

            if (adminOneOrTwo == 1) companyGiven.Id_admin_one = userGiven.Id;
            if (adminOneOrTwo == 2) companyGiven.Id_admin_two = userGiven.Id;

            _ = _company.UpdateAsync(companyGiven);

            IList<string> adminList = new List<string>();

            adminList.Add("Admin one = " + companyGiven.Id_admin_one);
            adminList.Add("Admin two = " + companyGiven.Id_admin_two);

            return adminList;
        }

        // api/Company/new__product/ 
        [HttpPost]
        [Route("new__product")]
        [Authorize(Roles = ("Company_Admin"))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<Full__Company> CREATE__COMPANY__PRODUCT(Full__Product DTO, int companyId)
        {
            Company searchedFor = await _company.GetAsyncById(companyId);

            Full__Company _DTO = _mapper.Map<Full__Company>(searchedFor);

            return _DTO;

        }

        // api/Company/update__product 
        [HttpPut]
        [Route("update__product")]
        [Authorize(Roles = ("Company_Admin"))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<Full__Product> UPDATE__COMPANY__PRODUCT(Full__Product productObj)
        {
            Product searchedFor = _mapper.Map<Product>(productObj);
            await _product.UpdateAsync(searchedFor);

            Full__Product _DTO = _mapper.Map<Full__Product>(productObj);

            return _DTO;
        }

        // api/Company/delete__product/{productId} 
        [HttpDelete]
        [Route("delete__product")]
        [Authorize(Roles = ("Company_Admin"))]
        //[Authorize(Roles = "Company_Admin")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<string> DELETE__COMPANY__PRODUCT(int productId)
        {
            await _product.DeleteAsync(productId);

            return "This product has been deleted";
        }

        // api/Company/update__company/{companyId} 
        [HttpPut]
        [Route("update__company")]
        [Authorize(Roles = ("API_Admin, Company_Admin"))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<Full__Product> UPDATE__COMPANY__DETAILS(Full__Product productObj)
        {
            Product searchedFor = _mapper.Map<Product>(productObj);
            await _product.UpdateAsync(searchedFor);

            Full__Product _DTO = _mapper.Map<Full__Product>(productObj);

            return _DTO;

        }
    }
}
