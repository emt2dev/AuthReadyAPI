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
    [Route("api/v{version:apiVersion}/staff")]
    [ApiVersion("2.0")]
    [ApiController]
    public class v2_StaffController : ControllerBase
    {
        private readonly IV2_Staff _staff;
        private readonly ILogger<v2_StaffController> _LOGS;
        private readonly IMapper _mapper;
        private readonly UserManager<v2_Staff> _UM;
        public v2_StaffController(UserManager<v2_Staff> UM, ILogger<v2_StaffController> LOGS, IV2_Staff staff, IMapper mapper)
        {
            this._LOGS = LOGS;
            this._mapper = mapper;
            this._staff = staff;
            this._UM = UM;
        }

        [HttpGet]
        [Route("all")]
        // ?StartIndex={StartIndex}&pagesize={pagesize}&pagenumber={pagenumber}
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        // public async Task<IList<v2_StaffDTO>> getAllStaffsPaged(int companyId, [FromQuery] QueryParameters QP)
        public async Task<IList<v2_StaffDTO>> getAllStaffs()
        {
            IList<v2_Staff> customerList = new List<v2_Staff>();
            customerList = await _staff.GetAllAsync<v2_Staff>();

            IList<v2_StaffDTO> listOfAllDTOs = new List<v2_StaffDTO>();

            foreach (v2_Staff customer in customerList)
                {
                   v2_StaffDTO DTO = _mapper.Map<v2_StaffDTO>(customer);

                    listOfAllDTOs.Add(DTO);
                }

            return listOfAllDTOs;
        }

        [HttpGet]
        [Route("details/{staffId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<v2_StaffDTO> customerDetails([FromRoute] string staffId)
        {
            v2_Staff staffFound = await _UM.FindByIdAsync(staffId);

            if(staffFound is not null) {
                v2_StaffDTO DTO = _mapper.Map<v2_StaffDTO>(staffFound);

                return DTO;
            } else {
                return null;
            }
        }

        [HttpPut]
        [Route("update")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<IActionResult> updateStaff(v2_Staff incomingDTO)
        {
            v2_Staff customerFound = _mapper.Map<v2_Staff>(incomingDTO);
            _ = await _UM.UpdateAsync(customerFound);

            return Ok();
        }
    }
}