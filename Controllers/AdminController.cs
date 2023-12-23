using AuthReadyAPI.DataLayer.DTOs.Company;
using AuthReadyAPI.DataLayer.DTOs.PII.APIUser;
using AuthReadyAPI.DataLayer.Interfaces;
using AuthReadyAPI.DataLayer.Models.PII;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
        private readonly IOrder _order;
        private readonly IApiAdmin _apiAdmin;


        private readonly ILogger<AuthController> _LOGS;
        private readonly IAuthManager _IAM;
        private readonly IMapper _mapper;

        private readonly UserManager<APIUserClass> _UM;

        public AdminController(ICompany company, IUser user, IProduct product, IOrder order, IApiAdmin apiAdmin, ILogger<AuthController> LOGS, IAuthManager IAM, IMapper mapper, UserManager<APIUserClass> UM)
        {
            this._company = company;
            this._LOGS = LOGS;
            this._IAM = IAM;
            this._mapper = mapper;
            this._UM = UM;
            this._product = product;
            this._order = order;
            this._apiAdmin = apiAdmin;
            this._user = user;

        }

        [HttpPost]
        [Route("admin/new")]
        [Authorize(Roles ="Admin")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<ActionResult> NewAdmin([FromBody] NewUserDTO IncomingDTO)
        {
            /*
           var errors = await _IAM.API__ADMIN__REGISTER(IncomingDTO);

            if (errors.Any())
            {
                foreach (var error in errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }

                //_LOGS.LogInformation($"Failed Register Attempt for {IncomingDTO.Email}");

                return BadRequest(ModelState);
            }
            */
            return Ok();
        }

        // api/admin/company__create 
        [HttpPost]
        [Route("company/new")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<string> NewCompany([FromBody] CompanyDTO IncomingDTO)
        {

            return $"{IncomingDTO.Name} was created";
        }

        // api/admin/company__create 
        [HttpPost]
        [Route("company/deactivate")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<string> DeactivateCompany([FromBody] CompanyDTO IncomingDTO)
        {

            return $"{IncomingDTO.Name} was deactivated";
        }
    }
}
