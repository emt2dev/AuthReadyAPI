using AuthReadyAPI.DataLayer.Interfaces;
using AuthReadyAPI.DataLayer.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AuthReadyAPI.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/company")]
    [ApiVersion("2.0")]
    
    public class v2_CompanyController : ControllerBase
    {
        private readonly ILogger<v2_ProductController> _LOGS;
        private readonly IMapper _mapper;
        private readonly IV2_Company _company;
        public v2_CompanyController(ILogger<v2_ProductController> LOGS, IV2_Company company, IMapper mapper)
        {
            this._LOGS = LOGS;
            this._mapper = mapper;
            this._company = company;
        }

        [HttpGet]
        [Route("details/{companyId}")]
        // ?StartIndex={StartIndex}&pagesize={pagesize}&pagenumber={pagenumber}
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<v2_CompanyDTO> getCompanyDetails([FromRoute] int companyId)
        {
            v2_Company companyFound = await _company.GetAsyncById(companyId);

            v2_CompanyDTO outgoingDTO = _mapper.Map<v2_CompanyDTO>(companyFound);

            return outgoingDTO;
        }

        [HttpPost]
        [Route("giveAdmin/{companyId}")]
        // [Authorize(Roles = ("API_Admin, Company_Admin"))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<IActionResult> addCompanyAdmin([FromBody] string adminEmail, [FromRoute] int companyId)
        {
            _ = await _company.giveAdminPrivledges(adminEmail, companyId);
            return Ok("added admin");
        }

        [HttpPost]
        [Route("removeAdmin/{companyId}")]
        // [Authorize(Roles = ("API_Admin, Company_Admin"))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<IActionResult> removeCompanyAdmin([FromBody] string adminEmail, [FromRoute] int companyId)
        {
            _ = await _company.removeAdminPrivledges(adminEmail, companyId);
            return Ok("removed admin");
        }

        [HttpPut]
        [Route("update")]
        // [Authorize(Roles = ("API_Admin, Company_Admin"))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<IActionResult> updateCompany([FromForm] v2_CompanyDTO incomingDTO)
        {
            v2_Company companyUpdating = _mapper.Map<v2_Company>(incomingDTO);
            await _company.UpdateAsync(companyUpdating);

            return Ok(companyUpdating);
        }
    }
}