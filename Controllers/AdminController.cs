using AuthReadyAPI.DataLayer.DTOs.APIUser;
using AuthReadyAPI.DataLayer.DTOs.Company;
using AuthReadyAPI.DataLayer.Interfaces;
using AuthReadyAPI.DataLayer.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;

namespace AuthReadyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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

        /* api/admin/admin__create */
        [HttpGet]
        [Route("admin__create")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<string> CREATE__API__ADMIN([FromBody] Full__APIUser DTO)
        {
            var result = await _apiAdmin.API__ADMIN__CREATE(DTO);

            return result;
        }

        /* api/admin/company__create */
        [HttpGet]
        [Route("company__create")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<string> CREATE__COMPANY([FromBody] Full__Company DTO)
        {
            Full__Company createdCompany = await _apiAdmin.COMPANY__CREATE(DTO);

            return createdCompany.Name + " was created";
        }

        /* api/admin/company__admin__override */
        [HttpPost]
        [Route("company__admin__override")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<string> OVERRIDE__COMPANY__ADMIN([FromBody] string userEmail, int companyId)
        {
            if (userEmail == null) return null;
            if (companyId < 0) return null;

            Company companyGiven = await _company.GetAsyncById(companyId);

            APIUser userGiven = await _user.USER__FIND__BY__EMAIL__ASYNC(userEmail);

            userGiven.IsStaff = true;
            userGiven.CompanyId = companyGiven.Id;

            _ = await _UM.AddToRoleAsync(userGiven, "Company_Admin");
            _ = _user.UpdateAsync(userGiven);

            companyGiven.Id_admin_one = userGiven.Id;

            _ = _company.UpdateAsync(companyGiven);            

            return companyGiven.Id_admin_one + " replaced existing admin";
        }
    }
}
