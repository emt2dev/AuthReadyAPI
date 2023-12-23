using AuthReadyAPI.DataLayer.DTOs.APIUser;
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
using Microsoft.AspNetCore.Mvc.Routing;
using System.ComponentModel.Design;

namespace AuthReadyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompany _company;
        private readonly IUser _user;
        private readonly IProduct _product;
        private readonly ICart _cart;
        private readonly IOrder _order;

        private readonly ILogger<AuthController> _LOGS;
        private readonly IAuthManager _IAM;
        private readonly IMapper _mapper;

        private readonly UserManager<APIUser> _UM;
        private readonly IMediaService _IMS;

        public CompanyController(ICompany company, IUser user, IProduct product, ICart cart, IOrder order, IMediaService IMS, ILogger<AuthController> LOGS, IAuthManager IAM, IMapper mapper, UserManager<APIUser> UM)
        {
            this._company = company;
            this._LOGS = LOGS;
            this._IAM = IAM;
            this._mapper = mapper;
            this._UM = UM;
            this._product = product;
            this._cart = cart;
            this._order = order;
            this._user = user;
            this._IMS = IMS;
        }

                /* api/Company/details/ 
         * semantics, by not needed since this is going to be used by one company.
         * Can be add other companies to this.
         */

        [HttpGet]
        [Route("all")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<IList<Base__Company>> GET__ALL__COMPANY()
        {
            IList<Base__Company> companies = new List<Base__Company>();
            IList<Full__Company> companiesDTList = new List<Full__Company>();

            companies = await _company.GetAllAsync<Base__Company>();
            // companiesDTList = _mapper.Map<IList<Full__Company>>(companies);

            return companies;
        }

        /* api/Company/details/ 
         * semantics, by not needed since this is going to be used by one company.
         * Can be add other companies to this.
         */

        [HttpGet]
        [Route("details/{companyId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<Base__Company> GET__COMPANY__DETAILS(int? companyId)
        {
            Company searchedFor = await _company.GetAsyncById(companyId);
            
            Base__Company companyDTO = _mapper.Map<Base__Company>(searchedFor);
            
            IList<Full__Product> ProductDTOs = new List<Full__Product>();
            
            if (searchedFor.Products is not null)
            {
                foreach (ProductDTO product in searchedFor.Products)
                {
                    Full__Product mappedProduct = new Full__Product {
                        Id = product.Id.ToString(),
                        Name = product.Name,
                        Description = product.Description,
                        Price_Current = product.Price_Current,
                        Price_Sale = product.Price_Sale,
                        Price_Normal = product.Price_Normal,
                        ImageURL = product.ImageURL,
                        CompanyId = product.CompanyId,
                        Modifiers = product.Modifiers,
                        Keyword = product.Keyword,
                    };
                    
                    ProductDTOs.Add(mappedProduct);
                }

                Full__Company returnThisCompany = new Full__Company {
                    Name = searchedFor.Name,
                    Description = searchedFor.Description,
                    Address = searchedFor.Address,
                    PhoneNumber = searchedFor.PhoneNumber,
                    Products = ProductDTOs,
                };

                return returnThisCompany;
            } else {

                Full__Company returnThisCompany = new Full__Company {
                    Id = searchedFor.Id.ToString(),
                    Name = searchedFor.Name,
                    Description = searchedFor.Description,
                    Address = searchedFor.Address,
                    PhoneNumber = searchedFor.PhoneNumber,
                    Products = ProductDTOs,
                };

                return returnThisCompany;
            }
        }

        // api/Company/new__admin/ 
        [HttpPost]
        [Route("new__admin")]
        // [Authorize(Roles = ("API_Admin, Company_Admin"))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<string> GIVE__ADMIN__PRIV(companyAdminPriv DTO)
        {
            if (DTO.userEmail == null) return null;
            if (DTO.replaceAdminOneOrTwo < 1 || DTO.replaceAdminOneOrTwo > 2) return null;

            Company companyGiven = await _company.GetAsyncById(DTO.companyId);

            APIUser userGiven = await _user.USER__FIND__BY__EMAIL__ASYNC(DTO.userEmail);

            // here we assign new values to the entity
            userGiven.IsStaff = true;
            userGiven.CompanyId = companyGiven.Id;

            _ = await _UM.AddToRoleAsync(userGiven, "Company_Admin");
            _ = _user.UpdateAsync(userGiven);

            if (DTO.replaceAdminOneOrTwo == 1) companyGiven.Id_admin_one = userGiven.Id;
            if (DTO.replaceAdminOneOrTwo == 2) companyGiven.Id_admin_two = userGiven.Id;

            _ = _company.UpdateAsync(companyGiven);

            return "admin added";
        }

                // api/Company/new__admin/ 
        [HttpPost]
        [Route("remove__admin")]
        // [Authorize(Roles = ("API_Admin, Company_Admin"))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<string> REMOVE__ADMIN__PRIV(companyAdminPriv DTO)
        {
            if (DTO.userEmail == null) return null;

            Company companyGiven = await _company.GetAsyncById(DTO.companyId);

            APIUser userGiven = await _user.USER__FIND__BY__EMAIL__ASYNC(DTO.userEmail);

            // here we assign new values to the entity
            userGiven.IsStaff = false;
            userGiven.CompanyId = null;

            _ = await _UM.RemoveFromRoleAsync(userGiven, "Company_Admin");
            _ = _user.UpdateAsync(userGiven);

            if (userGiven.Id == companyGiven.Id_admin_one) companyGiven.Id_admin_one = null;
            if (userGiven.Id == companyGiven.Id_admin_two) companyGiven.Id_admin_two = null;

            _ = _company.UpdateAsync(companyGiven);

            return "admin removed";
        }

        // api/Company/new__product/ 
        [HttpPost]
        [Route("new__product")]
        // [Authorize(Roles = ("API_Admin, Company_Admin"))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<IActionResult> CREATE__COMPANY__PRODUCT([FromForm] newProductDTO DTO)
        {

            var uploadPhoto = await _IMS.AddPhotoAsync(DTO.ImageURL);

            ProductDTO newProduct = new ProductDTO {
                Id = 0,
                Name = DTO.Name,
                Description = "update description",
                Price_Normal = DTO.Price_Normal,
                Price_Sale = DTO.Price_Sale,
                Price_Current = DTO.Price_Normal,
                ImageURL = uploadPhoto.Url.ToString(),
                CompanyId = DTO.CompanyId.ToString(),
                Keyword = DTO.Keyword,
                Modifiers = "",
            };

            _ = await _product.AddAsync(newProduct);

            return Ok();
            // return Ok(DTO.CompanyId.ToString());

        }

        // api/Company/update__product 
        [HttpPut]
        [Route("update__product")]
        // [Authorize(Roles = ("API_Admin, Company_Admin"))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<Full__Product> UPDATE__COMPANY__PRODUCT(Full__Product productObj)
        {
            ProductDTO searchedFor = _mapper.Map<ProductDTO>(productObj);
            await _product.UpdateAsync(searchedFor);

            Full__Product _DTO = _mapper.Map<Full__Product>(productObj);

            return _DTO;
        }

        // api/Company/delete__product/{productId} 
        [HttpDelete]
        [Route("delete__product/{productId}")]
        // [Authorize(Roles = ("API_Admin, Company_Admin"))]
        //[Authorize(Roles = "Company_Admin")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<string> DELETE__COMPANY__PRODUCT(int productId)
        {
            await _product.DeleteAsync(productId);

            return "This product has been deleted";
        }

        // api/Company/update__company/{companyId} 
        [HttpPut]
        [Route("update__company")]
        [Authorize(Roles = ("API_Admin, Company_Admin"))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<Full__Product> UPDATE__COMPANY__DETAILS(Full__Product productObj)
        {
            ProductDTO searchedFor = _mapper.Map<ProductDTO>(productObj);
            await _product.UpdateAsync(searchedFor);

            Full__Product _DTO = _mapper.Map<Full__Product>(productObj);

            return _DTO;

        }
    }
}
