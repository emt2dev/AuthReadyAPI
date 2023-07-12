using AuthReadyAPI.DataLayer.DTOs.Pagination;
using AuthReadyAPI.DataLayer.DTOs.Product;
using AuthReadyAPI.DataLayer.Interfaces;
using AuthReadyAPI.DataLayer.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AuthReadyAPI.Controllers
{
    [Route("api/v{version:apiVersion}/products")]
    [ApiVersion("2.0")]
    [ApiController]
    public class v2_ProductController : ControllerBase
    {
        private readonly IV2_Product _product;
        private readonly ILogger<v2_ProductController> _LOGS;
        private readonly IMapper _mapper;
        private readonly IMediaService _IMS;
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
            listOfAllProducts = await _product.GetAllAsync<v2_ProductStripe>();
            
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
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<IActionResult> newProduct([FromForm] v2_ProductDTO incomingDTO)
        {

            var uploadPhoto = await _IMS.AddPhotoAsync(incomingDTO.imageToBeUploaded);

            v2_ProductStripe newProduct = _mapper.Map<v2_ProductStripe>(incomingDTO);

            newProduct.image = uploadPhoto.Url.ToString();
            var i = (double)newProduct.default_price;
            newProduct.priceInString = i.ToString("0.####");

            _ = await _product.AddAsync(newProduct);

            return Ok();
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

        [HttpDelete]
        [Route("delete/{productId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<IActionResult> deleteProduct([FromRoute] int productId)
        {
            await _product.DeleteAsync(productId);

            return Ok();
        }
    }
}