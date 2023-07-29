using AuthReadyAPI.DataLayer.DTOs.Pagination;
using AuthReadyAPI.DataLayer.DTOs.Product;
using AuthReadyAPI.DataLayer.Interfaces;
using AuthReadyAPI.DataLayer.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthReadyAPI.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/products")]
    [ApiVersion("2.0")]
    public class v2_ProductController : ControllerBase
    {
        private readonly IV2_Product _product;
        private readonly ILogger<v2_ProductController> _LOGS;
        private readonly IMapper _mapper;
        private readonly IMediaService _IMS;
        private readonly int _defaultId;
        private readonly string staffDashboard = "http://localhost:4200/staff";
        public v2_ProductController(ILogger<v2_ProductController> LOGS, IV2_Product product, IMapper mapper, IMediaService IMS)
        {
            this._LOGS = LOGS;
            this._product = product;
            this._mapper = mapper;
            this._IMS = IMS;
        }

        [HttpGet]
        [Route("all/{companyId}")]
        // ?StartIndex={StartIndex}&pagesize={pagesize}&pagenumber={pagenumber}
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        // public async Task<IList<Full__Product>> PRODUCT__ALL(int companyId, [FromQuery] QueryParameters QP)
        public async Task<IList<v2_ProductDTO>> getAllProducts([FromRoute] int companyId)
        {
            IList<v2_ProductStripe> listOfAllProducts = new List<v2_ProductStripe>();
            listOfAllProducts = await _product.getAllCompanyProducts(companyId);
            
            IList<v2_ProductDTO> listOfAllDTOs = new List<v2_ProductDTO>();

            foreach (v2_ProductStripe product in listOfAllProducts)
                {
                   v2_ProductDTO DTO = _mapper.Map<v2_ProductDTO>(product);

                    listOfAllDTOs.Add(DTO);
                }

            return listOfAllDTOs;
        }

        [HttpGet]
        [Route("details/{productId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<v2_ProductDTO> getProductDetails([FromRoute] int productId)
        {
            v2_ProductStripe productFound = await _product.GetAsyncById(productId);
            v2_ProductDTO DTO = _mapper.Map<v2_ProductDTO>(productFound);

            return DTO;
        }

        [HttpPost]
        [Route("create")]
        [Consumes("multipart/form-data")]
        [Authorize(Roles ="Staff")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        // public async Task<IActionResult> newProduct([FromForm] IFormFile icFile)
        public async Task<IActionResult> newProduct([FromForm] v2_newProductDTO incomingDTO)
        {
            // return Ok("reached");
            
            v2_ProductDTO newProductBuilder = _mapper.Map<v2_ProductDTO>(incomingDTO);
            newProductBuilder.id = _defaultId;

            var uploadPhoto = await _IMS.AddPhotoAsync(incomingDTO.image);

            v2_ProductStripe newProduct = new v2_ProductStripe {
                companyId = incomingDTO.companyId,
                name = incomingDTO.name,
                description = incomingDTO.description,
                default_price = incomingDTO.default_price,
                quantity = 1,
                image = uploadPhoto.Url.ToString(),
                priceInString = incomingDTO.default_price.ToString(),
            };
            
            _ = await _product.AddAsync(newProduct);

            System.Uri uri = new System.Uri(staffDashboard);

            return Redirect(staffDashboard);
            
        }

        [HttpPut]
        [Route("update")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<IActionResult> updateProduct([FromForm] v2_ProductDTO incomingDTO)
        {
            v2_ProductStripe updatedProduct = _mapper.Map<v2_ProductStripe>(incomingDTO);

            await _product.UpdateAsync(updatedProduct);

            return Ok();
        }

        [HttpPost]
        [Route("update/image/{productId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<IActionResult> updateProductImage([FromRoute] int productId, IFormFile newImage)
        {
            v2_ProductStripe updatedProduct = await _product.GetAsyncById(productId);

            var uploadPhoto = await _IMS.AddPhotoAsync(newImage);

            updatedProduct.image = uploadPhoto.Url.ToString();

            await _product.UpdateAsync(updatedProduct);

            System.Uri uri = new System.Uri(staffDashboard);

            return Redirect(staffDashboard);
        }

        [HttpPost]
        [Route("delete")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<IActionResult> deleteProduct([FromForm] string productId)
        {
            var i = Convert.ToInt32(productId);
            await _product.DeleteAsync(i);

            System.Uri uri = new System.Uri(staffDashboard);

            return Redirect(staffDashboard);
        }
    }
}