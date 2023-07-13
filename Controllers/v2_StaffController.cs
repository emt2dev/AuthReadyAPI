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
    [Route("api/v{version:apiVersion}/staff")]
    [ApiVersion("2.0")]
    public class v2_StaffController : ControllerBase
    {
        private readonly IV2_User _user;
        private readonly ILogger<v2_StaffController> _LOGS;
        private readonly IMapper _mapper;
        private readonly UserManager<v2_UserStripe> _UM;
        public v2_StaffController(UserManager<v2_UserStripe> UM, ILogger<v2_StaffController> LOGS, IV2_User user, IMapper mapper)
        {
            this._LOGS = LOGS;
            this._mapper = mapper;
            this._user = user;
            this._UM = UM;
        }

        [HttpGet]
        [Route("all/{companyId}")]
        // ?StartIndex={StartIndex}&pagesize={pagesize}&pagenumber={pagenumber}
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        // public async Task<IList<v2_StaffDTO>> getAllStaffsPaged(int companyId, [FromQuery] QueryParameters QP)
        public async Task<IList<v2_StaffDTO>> getAllStaffs([FromRoute] int companyId)
        {
            IList<v2_UserStripe> staffList = new List<v2_UserStripe>();
            staffList = await _user.getAllStaff(companyId);

            IList<v2_StaffDTO> listOfAllDTOs = new List<v2_StaffDTO>();

            foreach (v2_UserStripe staff in staffList)
                {
                   v2_StaffDTO DTO = _mapper.Map<v2_StaffDTO>(staff);

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
            v2_UserStripe staffFound = await _UM.FindByIdAsync(staffId);

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
        public async Task<IActionResult> updateStaff(v2_StaffDTO incomingDTO)
        {
            v2_UserStripe customerFound = _mapper.Map<v2_UserStripe>(incomingDTO);
            _ = await _UM.UpdateAsync(customerFound);

            return Ok();
        }
    }
}