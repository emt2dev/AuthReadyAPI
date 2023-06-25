using AuthReadyAPI.DataLayer.DTOs;
using AuthReadyAPI.DataLayer.DTOs.APIUser;
using AuthReadyAPI.DataLayer.DTOs.Cart;
using AuthReadyAPI.DataLayer.DTOs.Company;
using AuthReadyAPI.DataLayer.DTOs.Order;
using AuthReadyAPI.DataLayer.DTOs.Product;
using AuthReadyAPI.DataLayer.Interfaces;
using AuthReadyAPI.DataLayer.Models;
using AuthReadyAPI.DataLayer.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Windows.Input;

namespace AuthReadyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
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




        public AuthController(ICompany company, IUser user, IProduct product, ICart cart, IOrder order, ILogger<AuthController> LOGS, IAuthManager IAM, IMapper mapper, UserManager<APIUser> UM)
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

        }

        /* api/auth/register */
        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<ActionResult> Register([FromBody] Full__APIUser DTO)
        {
            var errors = await _IAM.USER__REGISTER(DTO);

            if (errors.Any())
            {
                foreach (var error in errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }

                _LOGS.LogInformation($"Failed Register Attempt for {DTO.Email}");

                return BadRequest(ModelState);
            }

            return Ok();
        }

        /* api/auth/login */
        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<ActionResult> Login([FromBody] Base__APIUser DTO)
        {
            var authenticatedUser = await _IAM.USER__LOGIN(DTO);

            if (authenticatedUser == null)
            {
                _LOGS.LogInformation($"Failed Login Attempt for {DTO.Email}");
                return Unauthorized();
            }

            return Ok(authenticatedUser);
        }

            /*
            // Create Product
            Full__Product _company_001_product001 = new Full__Product
            {
                Name = "Burrito",
                Description = "burrito",
                Price_Current = 3.45,
                Price_Normal = 3.45,
                Price_Sale = 3.19,
                ImageURL = "https://asassyspoon.com/wp-content/uploads/cafe-con-leche-a-sassy-spoon-7.jpg",
                CompanyId = id_newCompany.ToString(),
                Modifiers = "none"
            };

            Full__Product _company_001_product002 = new Full__Product
            {
                Name = "lo mein",
                Description = "lo mein",
                Price_Current = 11.99,
                Price_Normal = 11.99,
                Price_Sale = 8.59,
                ImageURL = "https://images-gmi-pmc.edge-generalmills.com/ec9349d6-204b-4687-b94d-d0a0663d70de.jpg",
                CompanyId = id_newCompany.ToString(),
                Modifiers = "none"
            };

            Product productMapped001 = _mapper.Map<Product>(_company_001_product001);
            Product product001 = await _product.AddAsync(productMapped001);

            Product productMapped002 = _mapper.Map<Product>(_company_001_product002);
            Product product002 = await _product.AddAsync(productMapped002);

            Full__Product addTest001 = _mapper.Map<Full__Product>(product001);
            Full__Product addTest002 = _mapper.Map<Full__Product>(product002);

            // create cart


            Full__Cart u002_cart = new Full__Cart
            {
                CompanyId = id_newCompany.ToString(),
                Customer_Id = u002.Id,
                Total_Amount = 0,
                Total_Discounted = 0,
                Discount_Rate = 0,
                Abandoned = false,
                Submitted = false,
            };

            // add products to cart
            u002_cart.Products.Add(addTest001);
            _ = u002_cart.Total_Amount = u002_cart.Total_Amount + addTest001.Price_Current;

            u002_cart.Products.Add(addTest002);
            _ = u002_cart.Total_Amount = u002_cart.Total_Amount + addTest001.Price_Current;

            Cart mappedCart_u002 = _mapper.Map<Cart>(u002_cart);


            Cart addCart_u001 = await _cart.AddAsync(mappedCart_u002);


            // Created second cart
            Full__Cart u003_cart = new Full__Cart
            {
                CompanyId = id_newCompany.ToString(),
                Customer_Id = u003.Id,
                Total_Amount = 0,
                Total_Discounted = 1,
                Discount_Rate = 0,
                Abandoned = false,
                Submitted = false,
            };
            
            // add products to cart
            u003_cart.Products.Add(addTest001);
            _ = u003_cart.Total_Amount = u003_cart.Total_Amount + addTest001.Price_Current;

            u003_cart.Products.Add(addTest002);
            _ = u003_cart.Total_Amount = u003_cart.Total_Amount + addTest001.Price_Current;

            Cart mappedCart_u003 = _mapper.Map<Cart>(u003_cart);

            /*
            IList <Cart> cartList = new List<Cart>();

            cartList.Add(mappedCart_u003);
            cartList.Add(mappedCart_u003);

            return cartList;
            
            
            Cart addCart_u003 = await _cart.AddAsync(mappedCart_u003);

            // Here we "submit the cart" and it becomes an order
            addCart_u003.Submitted = true;

            Cart updateCart = addCart_u003;
            await _cart.UpdateAsync(updateCart);

            IList<Full__Product> Mapped__ProductDTO__List = new List<Full__Product>();
            double cartRunningTotal = 0;

            // Here we have to convert all products to DTO products
            foreach (var Product in updateCart.Products)
            {
                var mappedProduct = _mapper.Map<Full__Product>(Product);
                Mapped__ProductDTO__List.Add(mappedProduct);
                cartRunningTotal = cartRunningTotal + mappedProduct.Price_Current;
            }

            // here we update the cart to submitted and create an order
            Full__Order newOrderDTOTest = new Full__Order
            {
                Customer_Id = updateCart.Customer,
                CompanyId = updateCart.Company,
                CartId = updateCart.Id.ToString(),
                Products = Mapped__ProductDTO__List,
                Payment_Amount = cartRunningTotal,
                Time__Submitted = DateTime.Now,
                Payment_Complete = true,
                DestinationAddress = "4001 W Tampa Blvd, Tampa, FL 33614",
                CurrentStatus = "Paid, Not Touched",
            };

            Order newOrder = _mapper.Map<Order>(newOrderDTOTest);
            var saveOrder = await _order.AddAsync(newOrder);

            

            // Created second cart
            Full__Cart u004_cart = new Full__Cart
            {
                CompanyId = id_newCompany.ToString(),
                Customer_Id = u004.Id,
                Total_Amount = 0,
                Total_Discounted = 1,
                Discount_Rate = 0,
                Abandoned = false,
                Submitted = false,
            };

            // add products to cart
            u004_cart.Products.Add(addTest001);
            _ = u004_cart.Total_Amount = u004_cart.Total_Amount + addTest001.Price_Current;

            u004_cart.Products.Add(addTest002);
            _ = u004_cart.Total_Amount = u004_cart.Total_Amount + addTest001.Price_Current;

            Cart mappedCart_u004 = _mapper.Map<Cart>(u004_cart);
            Cart addCart_u004 = await _cart.AddAsync(mappedCart_u004);

            // Here we "submit the cart" and it becomes an order
            addCart_u004.Submitted = true;

            updateCart = addCart_u004;

            await _cart.UpdateAsync(updateCart);

            IList<Full__Product> Mapped__ProductDTO__List2 = new List<Full__Product>();
            double cartRunningTotalu004 = 0;

            // Here we have to convert all products to DTO products
            foreach (var Product in updateCart.Products)
            {
                var mappedProducts = _mapper.Map<Full__Product>(Product);
                Mapped__ProductDTO__List2.Add(mappedProducts);
                cartRunningTotalu004 = cartRunningTotalu004 + mappedProducts.Price_Current;
            }

            // here we update the cart to submitted and create an order
            Full__Order completedOrderDTOTest = new Full__Order
            {
                Customer_Id = updateCart.Customer,
                CompanyId = updateCart.Company,
                CartId = updateCart.Id.ToString(),
                Products = Mapped__ProductDTO__List,
                Payment_Amount = cartRunningTotal,
                Time__Submitted = DateTime.Now,
                Payment_Complete = true,
                DestinationAddress = "4001 W Tampa Blvd, Tampa, FL 33614",
                CurrentStatus = "Paid, Not Touched",
            };

            Order completedOrder = _mapper.Map<Order>(completedOrderDTOTest);
            var completedOrderDTOFinal = await _order.AddAsync(completedOrder);


            return "initialized";
            */
    }
}
