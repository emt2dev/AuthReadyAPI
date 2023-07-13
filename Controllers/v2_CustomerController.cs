using AuthReadyAPI.DataLayer.DTOs.APIUser;
using AuthReadyAPI.DataLayer.DTOs.Pagination;
using AuthReadyAPI.DataLayer.DTOs.Product;
using AuthReadyAPI.DataLayer.Interfaces;
using AuthReadyAPI.DataLayer.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuthReadyAPI.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/customers")]
    [ApiVersion("2.0")]
    public class v2_CustomerController : ControllerBase
    {
        private readonly IV2_User _customer;
        private readonly ILogger<v2_CustomerController> _LOGS;
        private readonly IMapper _mapper;
        private readonly UserManager<v2_UserStripe> _UM;
        private v2_UserStripe _user;
        public v2_CustomerController(UserManager<v2_UserStripe> UM, ILogger<v2_CustomerController> LOGS, IV2_User customer, IMapper mapper)
        {
            this._LOGS = LOGS;
            this._mapper = mapper;
            this._customer = customer;
            this._UM = UM;
        }

        // [HttpGet]
        // [Route("all")]
        // // ?StartIndex={StartIndex}&pagesize={pagesize}&pagenumber={pagenumber}
        // [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        // [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        // [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        // // public async Task<IList<v2_CustomerDTO>> getAllCustomersPaged(int companyId, [FromQuery] QueryParameters QP)
        // public async Task<IList<v2_CustomerDTO>> getAllCustomers()
        // {
        //     IList<v2_UserStripe> customerList = new List<v2_UserStripe>();
        //     customerList = await _customer.GetAllAsync<v2_UserStripe>();

        //     IList<v2_CustomerDTO> listOfAllDTOs = new List<v2_CustomerDTO>();

        //     foreach (v2_UserStripe customer in customerList)
        //         {
        //            v2_CustomerDTO DTO = _mapper.Map<v2_CustomerDTO>(customer);

        //             listOfAllDTOs.Add(DTO);
        //         }

        //     return listOfAllDTOs;
        // }

        [HttpPost]
        [Route("details/{customerId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<v2_CustomerDTO> customerDetails([FromRoute] string customerId)
        {
            v2_UserStripe customerFound = await _UM.FindByIdAsync(customerId);


            if(customerFound is not null) {
                v2_CustomerDTO outgoingDTO = _mapper.Map<v2_CustomerDTO>(customerFound);

                return outgoingDTO;
            } else {
                return null;
            }
        }

        [HttpPut]
        [Route("update")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<IActionResult> updateCustomer(v2_CustomerDTO incomingDTO)
        {
            v2_UserStripe customerFound = _mapper.Map<v2_UserStripe>(incomingDTO);
            _ = await _UM.UpdateAsync(customerFound);

            return Ok();
        }
    }
}