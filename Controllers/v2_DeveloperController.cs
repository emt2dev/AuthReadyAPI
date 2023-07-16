using AuthReadyAPI.DataLayer.DTOs.APIUser;
using AuthReadyAPI.DataLayer.DTOs.AuthResponse;
using AuthReadyAPI.DataLayer.DTOs.Cart;
using AuthReadyAPI.DataLayer.DTOs.Company;
using AuthReadyAPI.DataLayer.DTOs.Order;
using AuthReadyAPI.DataLayer.DTOs.Product;
using AuthReadyAPI.DataLayer.Interfaces;
using AuthReadyAPI.DataLayer.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;

namespace AuthReadyAPI.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/developers")]
    [ApiVersion("2.0")]
    // uncomment this out once deployed
    // [Authorize(Roles = "Developer")]
    public class v2_DeveloperController : ControllerBase
    {
        private readonly IV2_AuthManager _IAM;
        private readonly IV2_User _staff;
        private readonly ILogger<v2_DeveloperController> _LOGS;
        private readonly IMapper _mapper;
        private readonly UserManager<v2_UserStripe> _UM;
        private readonly IV2_Product _product;
        private readonly IV2_Company _company;
        public v2_DeveloperController(IV2_AuthManager IAM, IV2_Company company, IV2_Product product, UserManager<v2_UserStripe> UM, ILogger<v2_DeveloperController> LOGS, IV2_User staff, IMapper mapper)
        {
            this._LOGS = LOGS;
            this._mapper = mapper;
            this._staff = staff;
            this._UM = UM;
            this._company = company;
            this._IAM = IAM;
            this._product = product;
        }

        // api/admin/company__create 
        [HttpPost]
        [Route("new/company")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<string> createNewCompany(v2_CompanyDTO incomingDTO)
        {
           v2_Company createdCompany = _mapper.Map<v2_Company>(incomingDTO);
           
            _ = await _company.AddAsync(createdCompany);

            return createdCompany.name + " was created";
        }

        // api/admin/company__create 
        [HttpPost]
        [Route("new/dev")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<string> createNewDeveloper(Base__APIUser incomingDTO)
        {
           _ = await _IAM.registerDeveloper(incomingDTO);

            return incomingDTO.Email + " added as a developer";
        }

        [HttpPost]
        [Route("update/admin")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<string> updateCompanyAdmin(overrideDTO DTO)
        {
            v2_Company companyOverriddenAdmin = await _company.GetAsyncById(DTO.companyId);
            v2_UserStripe userGivenPrivledges = await _UM.FindByEmailAsync(DTO.userEmail);
            userGivenPrivledges.companyId = DTO.companyId;
            
            
            if(DTO.replaceAdminOneOrTwo != 0)
            {
                userGivenPrivledges.giveAdminPrivledges = true;
                await _UM.AddToRoleAsync(userGivenPrivledges, "Staff");

                if(DTO.replaceAdminOneOrTwo == 1) {
                    companyOverriddenAdmin.administratorOne = userGivenPrivledges;
                }
                else if(DTO.replaceAdminOneOrTwo == 2) {
                    companyOverriddenAdmin.administratorTwo = userGivenPrivledges;
                }

                await _company.UpdateAsync(companyOverriddenAdmin);

                return DTO.userEmail + " replaced existing admin " + DTO.replaceAdminOneOrTwo + "for company: " + DTO.companyId;
            }
            
            else if(DTO.replaceAdminOneOrTwo == 0)
            {
                userGivenPrivledges.giveAdminPrivledges = true;
                await _UM.AddToRoleAsync(userGivenPrivledges, "Owner");

                companyOverriddenAdmin.owner = userGivenPrivledges;
                await _company.UpdateAsync(companyOverriddenAdmin);

                return DTO.userEmail + " replaced owner " + DTO.replaceAdminOneOrTwo + "for company: " + DTO.companyId;
            }

            return "invalid replace, choose 0 to replace the owner, 1 for adminone, 2 for admintwo.";
        }

        [HttpGet]
        [Route("init")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<string> init()
        {
            Base__APIUser newCustomer = new Base__APIUser {
                Email = "customer1@customer.com",
                Password = "P@ssword1",
            };

            var errors = await _IAM.registerCustomer(newCustomer);

            Base__APIUser newDeveloper = new Base__APIUser {
                Email = "dev1@dev.com",
                Password = "P@ssword1",
            };

            errors = await _IAM.registerDeveloper(newDeveloper);

            Base__APIUser newStaff1 = new Base__APIUser {
                Email = "staff1@staff.com",
                Password = "P@ssword1",
            };

            errors = await _IAM.registerStaff(newStaff1);
            v2_UserStripe findStaff1 = await _UM.FindByEmailAsync(newStaff1.Email);

            Base__APIUser newStaff2 = new Base__APIUser {
                Email = "staff1@staff.com",
                Password = "P@ssword1",
            };

            errors = await _IAM.registerStaff(newStaff2);
            v2_UserStripe findStaff2 = await _UM.FindByEmailAsync(newStaff2.Email);

            Base__APIUser newOwner = new Base__APIUser {
                Email = "owner1@owner.com",
                Password = "P@ssword1",
            };

            errors = await _IAM.registerStaff(newOwner);
            
            v2_UserStripe findOwner = await _UM.FindByEmailAsync(newOwner.Email);

            v2_Company newCompany = new v2_Company {
                name = "Cafe of the World",
                description = "This is an example description of your company, and we're proud to do it in our small town. We've pulled ourselevs up by our bootstraps and offer the best bang for your buck.",
                addressStreet = "123 E Main St",
                addressCity = "Lakeland",
                addressState = "FL",
                addressPostal_code = "33805",
                addressCountry = "USA",
                addressSuite = "",
                phoneNumber = "863-500-4411",
                smallTagline = "American comfort food with an International Twist",
                menuDescription = "Our Products are the result of our love for family and good quality which we dedicate to our customers.",
                headerImage = "https://via.placeholder.com/300x100?text=Header%20Placeholder",
                aboutUsImageUrl = "https://via.placeholder.com/150x100?text=About%20Us",
                locationImageUrl = "https://via.placeholder.com/500x250?text=Location",
                logoImageUrl = "https://via.placeholder.com/300x150?text=Logo",
                miscImageUrl = "https://via.placeholder.com/150x80?text=Misc%20Image",
                owner = findOwner,
                administratorOne = findStaff1,
                administratorTwo = findStaff2                
            };

            newCompany = await _company.AddAsync(newCompany);

            return newCompany.id.ToString();
        }
    }
}
