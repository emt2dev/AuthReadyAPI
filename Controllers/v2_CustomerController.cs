using System.Text.Json;
using AuthReadyAPI.DataLayer.DTOs.APIUser;
using AuthReadyAPI.DataLayer.DTOs.Pagination;
using AuthReadyAPI.DataLayer.DTOs.Product;
using AuthReadyAPI.DataLayer.Interfaces;
using AuthReadyAPI.DataLayer.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuthReadyAPI.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/customers")]
    [ApiVersion("2.0")]
    public class v2_CustomerController : ControllerBase
    {
        private string _tokenProvider = "AuthReadyAPI";
        private readonly IV2_User _customer;
        private readonly ILogger<v2_CustomerController> _LOGS;
        private readonly IMapper _mapper;
        private readonly UserManager<v2_UserStripe> _UM;
        private v2_UserStripe? _user;
        private readonly string customerDashboard = "http://localhost:4200/dashboard";
        public v2_CustomerController(UserManager<v2_UserStripe> UM, ILogger<v2_CustomerController> LOGS, IV2_User customer, IMapper mapper)
        {
            this._LOGS = LOGS;
            this._mapper = mapper;
            this._customer = customer;
            this._UM = UM;
        }

        [HttpPost]
        [Route("update/password/{customerId}")]
        // ?StartIndex={StartIndex}&pagesize={pagesize}&pagenumber={pagenumber}
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        // public async Task<IList<v2_CustomerDTO>> getAllCustomersPaged(int companyId, [FromQuery] QueryParameters QP)
        public async Task<IActionResult> updatePassword([FromRoute] string customerId, [FromForm] updatePasswordDTO incomingDTO)
        {
            v2_UserStripe found = await _UM.FindByIdAsync(customerId);
            var i = await _UM.ChangePasswordAsync(found, incomingDTO.currentPassword, incomingDTO.newPassword);

            await _UM.UpdateAsync(found);

            return Ok();
        }
        
        [HttpPost]
        [Route("update/address/{customerId}")]
        // ?StartIndex={StartIndex}&pagesize={pagesize}&pagenumber={pagenumber}
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        // public async Task<IList<v2_CustomerDTO>> getAllCustomersPaged(int companyId, [FromQuery] QueryParameters QP)
        public async Task<IActionResult> UpdateAddress([FromRoute] string customerId, [FromForm] updateAddressDTO DTO)
        {
            v2_UserStripe found = await _UM.FindByIdAsync(customerId);

            if(found is not null) {
                found.addressStreet = DTO.addressStreet;
                found.addressSuite = DTO.addressSuite;
                found.addressState = DTO.addressState;
                found.addressCity = DTO.addressCity;
                found.addressCountry = DTO.addressCountry;
                found.addressPostal_code = DTO.addressPostal_code;
                found.PhoneNumber = DTO.PhoneNumber;

                await _UM.UpdateAsync(found);
                
                return Ok();
            } else return Ok();
        }

        [HttpPost]
        [Route("get/address/{customerId}")]
        // ?StartIndex={StartIndex}&pagesize={pagesize}&pagenumber={pagenumber}
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        // public async Task<IList<v2_CustomerDTO>> getAllCustomersPaged(int companyId, [FromQuery] QueryParameters QP)
        public async Task<updateAddressDTO> GetAddress([FromRoute] string customerId)
        {
            v2_UserStripe found = await _UM.FindByIdAsync(customerId);

            updateAddressDTO outgoingDTO = new updateAddressDTO();
            
            outgoingDTO.addressStreet = found.addressStreet;
            outgoingDTO.addressSuite = found.addressSuite;
            outgoingDTO.addressState = found.addressState;
            outgoingDTO.addressCity = found.addressCity;
            outgoingDTO.addressCountry = found.addressCountry;
            outgoingDTO.addressPostal_code = found.addressPostal_code;
            outgoingDTO.PhoneNumber = found.PhoneNumber;
            
            return outgoingDTO;
        }

        [HttpPost]
        [Route("details/{customerId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<IActionResult> customerDetails([FromRoute] string customerId)
        {
            // return Ok("reached");
            v2_UserStripe customerFound = await _UM.FindByIdAsync(customerId);

            return Ok(customerFound);
        }

        [HttpPost]
        [Route("update/{customerId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<IActionResult> updateCustomer([FromRoute] string customerId, v2_CustomerDTO DTO)
        {
            // v2_UserStripe customerToBeUpdated = _mapper.Map<v2_UserStripe>(DTO);
            v2_UserStripe customerToBeUpdated = await _UM.FindByIdAsync(customerId);

            if (DTO.PhoneNumber != customerToBeUpdated.PhoneNumber && DTO.PhoneNumber != "")
                    customerToBeUpdated.PhoneNumber = DTO.PhoneNumber;

            if (DTO.name != customerToBeUpdated.name && DTO.name != "")
                    customerToBeUpdated.name = DTO.name;
    
            if (DTO.addressStreet != customerToBeUpdated.addressStreet && DTO.addressStreet != "") 
                 customerToBeUpdated.addressStreet = DTO.addressStreet;

            if (DTO.addressSuite != customerToBeUpdated.addressSuite && DTO.addressSuite != "")
                 customerToBeUpdated.addressSuite = DTO.addressSuite;

            if (DTO.addressCity != customerToBeUpdated.addressCity && DTO.addressCity != "")
                 customerToBeUpdated.addressCity = DTO.addressCity;

            if (DTO.addressState != customerToBeUpdated.addressState && DTO.addressState != "")
                    customerToBeUpdated.addressState = DTO.addressState;

            if (DTO.addressPostal_code != customerToBeUpdated.addressPostal_code && DTO.addressPostal_code != "")
                    customerToBeUpdated.addressPostal_code = DTO.addressPostal_code;

            if (DTO.addressCountry != customerToBeUpdated.addressCountry && DTO.addressCountry != "")
                    customerToBeUpdated.addressCountry = DTO.addressCountry;

            _ = await _UM.UpdateAsync(customerToBeUpdated);

            System.Uri uri = new System.Uri(customerDashboard);

            return Redirect(customerDashboard);
        }
    }
}