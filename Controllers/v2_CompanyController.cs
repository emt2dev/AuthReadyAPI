using AuthReadyAPI.DataLayer.Interfaces;
using AuthReadyAPI.DataLayer.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
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
        private readonly IMediaService _IMS;
        public v2_CompanyController(ILogger<v2_ProductController> LOGS, IMediaService IMS, IV2_Company company, IMapper mapper)
        {
            this._LOGS = LOGS;
            this._mapper = mapper;
            this._company = company;
            this._IMS = IMS;
        }

        [HttpGet]
        [Route("details/{companyId}")]
        // ?StartIndex={StartIndex}&pagesize={pagesize}&pagenumber={pagenumber}
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<v2_CompanyDTO> getCompanyDetails([FromRoute] int companyId)
        {
            v2_Company companyFound = await _company.getFullCompany(companyId);

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
            v2_Company foundCompany = await _company.GetAsyncById(incomingDTO.id);

            if (incomingDTO.name != foundCompany.name && incomingDTO.name != "") foundCompany.name = incomingDTO.name;
            if (incomingDTO.description != foundCompany.description && incomingDTO.description != "") foundCompany.description = incomingDTO.description;
            if (incomingDTO.phoneNumber != foundCompany.phoneNumber && incomingDTO.phoneNumber != "") foundCompany.phoneNumber = incomingDTO.phoneNumber;
            if (incomingDTO.addressStreet != foundCompany.addressStreet && incomingDTO.addressStreet != "") foundCompany.addressStreet = incomingDTO.addressStreet;
            if (incomingDTO.addressSuite != foundCompany.addressSuite && incomingDTO.addressSuite != "") foundCompany.addressSuite = incomingDTO.addressSuite;
            if (incomingDTO.addressCity != foundCompany.addressCity && incomingDTO.addressCity != "") foundCompany.addressCity = incomingDTO.addressCity;
            if (incomingDTO.addressState != foundCompany.addressState && incomingDTO.addressState != "") foundCompany.addressState = incomingDTO.addressState;
            if (incomingDTO.addressPostal_code != foundCompany.addressPostal_code && incomingDTO.addressPostal_code != "") foundCompany.addressPostal_code = incomingDTO.addressPostal_code;
            if (incomingDTO.addressCountry != foundCompany.addressCountry && incomingDTO.addressCountry != "") foundCompany.addressCountry = incomingDTO.addressCountry;
            if (incomingDTO.smallTagline != foundCompany.smallTagline && incomingDTO.smallTagline != "") foundCompany.smallTagline = incomingDTO.smallTagline;
            if (incomingDTO.menuDescription != foundCompany.menuDescription && incomingDTO.menuDescription != "") foundCompany.menuDescription = incomingDTO.menuDescription;

            await _company.UpdateAsync(foundCompany);

            return Ok("company has been updated");
        }

        [HttpPut]
        [Route("images/{companyId}/header")]
        // [Authorize(Roles = ("API_Admin, Company_Admin"))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<IActionResult> updateHeaderImage([FromRoute] int companyId, [FromForm] IFormFile newImage)
        {
            var uploadPhoto = await _IMS.AddPhotoAsync(newImage);

            v2_Company foundCompany = await _company.GetAsyncById(companyId);

            foundCompany.headerImage = uploadPhoto.Url.ToString();

            await _company.UpdateAsync(foundCompany);

            HttpContext.Response.Headers.Add("Location", "http://localhost:4200/staff");

            return Ok("header has been updated");
        }

        [HttpPut]
        [Route("images/{companyId}/aboutus")]
        // [Authorize(Roles = ("API_Admin, Company_Admin"))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<IActionResult> updateAboutUsImage([FromRoute] int companyId, [FromForm] IFormFile newImage)
        {
            var uploadPhoto = await _IMS.AddPhotoAsync(newImage);

            v2_Company foundCompany = await _company.GetAsyncById(companyId);

            foundCompany.aboutUsImageUrl = uploadPhoto.Url.ToString();

            await _company.UpdateAsync(foundCompany);

            HttpContext.Response.Headers.Add("Location", "http://localhost:4200/staff");

            return Ok("aboutUsImageUrl has been updated");
        }

        [HttpPut]
        [Route("images/{companyId}/location")]
        // [Authorize(Roles = ("API_Admin, Company_Admin"))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<IActionResult> updateLocationImage([FromRoute] int companyId, [FromForm] IFormFile newImage)
        {
            var uploadPhoto = await _IMS.AddPhotoAsync(newImage);

            v2_Company foundCompany = await _company.GetAsyncById(companyId);

            foundCompany.locationImageUrl = uploadPhoto.Url.ToString();

            await _company.UpdateAsync(foundCompany);

            HttpContext.Response.Headers.Add("Location", "http://localhost:4200/staff");

            return Ok("locationImageUrl has been updated");
        }

        [HttpPut]
        [Route("images/{companyId}/logo")]
        // [Authorize(Roles = ("API_Admin, Company_Admin"))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<IActionResult> updateLogoImage([FromRoute] int companyId, [FromForm] IFormFile newImage)
        {
            var uploadPhoto = await _IMS.AddPhotoAsync(newImage);

            v2_Company foundCompany = await _company.GetAsyncById(companyId);

            foundCompany.logoImageUrl = uploadPhoto.Url.ToString();

            await _company.UpdateAsync(foundCompany);

            HttpContext.Response.Headers.Add("Location", "http://localhost:4200/staff");

            return Ok("logoImageUrl has been updated");
        }

        [HttpPut]
        [Route("images/{companyId}/misc")]
        // [Authorize(Roles = ("API_Admin, Company_Admin"))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<IActionResult> updateMiscImage([FromRoute] int companyId, [FromForm] IFormFile newImage)
        {
            var uploadPhoto = await _IMS.AddPhotoAsync(newImage);

            v2_Company foundCompany = await _company.GetAsyncById(companyId);

            foundCompany.miscImageUrl = uploadPhoto.Url.ToString();

            await _company.UpdateAsync(foundCompany);

            HttpContext.Response.Headers.Add("Location", "http://localhost:4200/staff");
            return Ok("miscImageUrl has been updated");
        }
    }
}