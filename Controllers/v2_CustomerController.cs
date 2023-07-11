using AuthReadyAPI.DataLayer.DTOs.APIUser;
using AuthReadyAPI.DataLayer.DTOs.Pagination;
using AuthReadyAPI.DataLayer.DTOs.Product;
using AuthReadyAPI.DataLayer.Interfaces;
using AuthReadyAPI.DataLayer.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AuthReadyAPI.Controllers
{
    [Route("api/v{version:apiVersion}/customers")]
    [ApiVersion("2.0")]
    [ApiController]
    public class v2_CustomerController : ControllerBase
    {
        private readonly IV2_Customer _customer;
        private readonly ILogger<v2_CustomerController> _LOGS;
        private readonly IMapper _mapper;
        public v2_CustomerController(ILogger<v2_CustomerController> LOGS, IV2_Customer customer, IMapper mapper)
        {
            this._LOGS = LOGS;
            this._mapper = mapper;
            this._customer = customer;
        }

        [HttpGet]
        [Route("all")]
        // ?StartIndex={StartIndex}&pagesize={pagesize}&pagenumber={pagenumber}
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        // public async Task<IList<v2_CustomerDTO>> getAllCustomersPaged(int companyId, [FromQuery] QueryParameters QP)
        public async Task<IList<v2_CustomerDTO>> getAllCustomers()
        {
            IList<v2_CustomerStripe> customerList = new List<v2_CustomerStripe>();
            customerList = await _customer.GetAllAsync<v2_CustomerStripe>();

            IList<v2_CustomerDTO> listOfAllDTOs = new List<v2_CustomerDTO>();

            foreach (v2_CustomerStripe customer in customerList)
                {
                   v2_CustomerDTO DTO = _mapper.Map<v2_CustomerDTO>(customer);

                    listOfAllDTOs.Add(DTO);
                }

            return listOfAllDTOs;
        }

        [HttpGet]
        [Route("details")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<v2_CustomerDTO> productDetails([FromBody] int customerId)
        {
            v2_CustomerStripe customerFound = await _customer.GetAsyncById(customerId);

            v2_CustomerDTO DTO = _mapper.Map<v2_CustomerDTO>(customerFound);

            return DTO;
        }
    }
}