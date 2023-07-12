using AuthReadyAPI.DataLayer.DTOs.APIUser;
using AuthReadyAPI.DataLayer.DTOs.AuthResponse;
using AuthReadyAPI.DataLayer.Interfaces;
using AuthReadyAPI.DataLayer.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AuthReadyAPI.Controllers
{
    [Route("api/v{version:apiVersion}/entry")]
    [ApiVersion("2.0")]
    [ApiController]
    public class v2_EntryController : ControllerBase
    {
        private readonly ILogger<v2_EntryController> _LOGS;
        private readonly IMapper _mapper;
        private readonly IV2_AuthManager _IAM;
        private readonly v2_CustomerStripe? _customer;
        public v2_EntryController(ILogger<v2_EntryController> LOGS, IMapper mapper, IV2_AuthManager IAM)
        {
            this._LOGS = LOGS;
            this._mapper = mapper;
            this._IAM = IAM;
        }

        [HttpPost]
        [Route("register/customer")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<ActionResult> registerNewCustomer(v2_CustomerDTO DTO)
        {
            var errors = await _IAM.registerCustomer(DTO);

            if (errors.Any())
            {
                foreach (var error in errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }

                _LOGS.LogInformation($"Failed Register Attempt for {DTO.Email}");

                return BadRequest(ModelState);
            }

            Stripe.Customer result = await _IAM.addStripeCustomer(DTO.Email);

            return Ok(result);
        }

        [HttpPost]
        [Route("register/staff")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<ActionResult> registerNewStaff(v2_StaffDTO DTO)
        {
            var errors = await _IAM.registerStaff(DTO);

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

        [HttpPost]
        [Route("register/developer")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<ActionResult> registerNewDeveloper(v2_StaffDTO DTO)
        {
            var errors = await _IAM.registerDeveloper(DTO);

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


        [HttpPost]
        [Route("customer")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<ActionResult> grantEntryToCustomer(v2_CustomerDTO DTO)
        {

            Full__AuthResponseDTO authenticatedUser = await _IAM.loginCustomer(DTO);

            if (authenticatedUser == null)
            {
                _LOGS.LogInformation($"Failed Login Attempt for {DTO.Email}");
                return Unauthorized();
            }

            return Ok(authenticatedUser);
        }

        [HttpPost]
        [Route("staff")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<ActionResult> grantEntryToStaff(v2_StaffDTO DTO)
        {

            Full__AuthResponseDTO authenticatedUser = await _IAM.loginStaff(DTO);

            if (authenticatedUser == null)
            {
                _LOGS.LogInformation($"Failed Login Attempt for {DTO.Email}");
                return Unauthorized();
            }

            return Ok(authenticatedUser);
        }

        [HttpPost]
        [Route("developer")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<ActionResult> grantEntryToDeveloper(v2_StaffDTO DTO)
        {

            Full__AuthResponseDTO authenticatedUser = await _IAM.loginDeveloper(DTO);

            if (authenticatedUser == null)
            {
                _LOGS.LogInformation($"Failed Login Attempt for {DTO.Email}");
                return Unauthorized();
            }

            return Ok(authenticatedUser);
        }

        [HttpPost]
        [Route("refresh")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<Full__AuthResponseDTO> RefreshToken([FromBody] Full__AuthResponseDTO DTO)
        {
            Full__AuthResponseDTO refreshOldToken = await _IAM.VerifyRefreshToken(DTO);
            return refreshOldToken;
        }
    }
}