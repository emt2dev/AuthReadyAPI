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
using Microsoft.AspNetCore.Authorization;
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
    public class UserController : ControllerBase
    {
        private readonly ICompany _company;
        private readonly IUser _user;
        private readonly IProduct _product;
        private readonly ICart _cart;
        private readonly IOrder _order;

        private readonly ILogger<UserController> _LOGS;
        private readonly IAuthManager _IAM;
        private readonly IMapper _mapper;

        private readonly UserManager<APIUser> _UM;




        public UserController(ICompany company, IUser user, IProduct product, ICart cart, IOrder order, ILogger<UserController> LOGS, IAuthManager IAM, IMapper mapper, UserManager<APIUser> UM)
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

        [HttpPost]

        [Route("details/{userId}")]
        [Authorize(Roles = ("User"))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<Full__APIUser> USER__DETAILS__GET(string userId)
        {

            APIUser userFound = await _IAM.USER__DETAILS(userId);
            Full__APIUser preparedUser =_mapper.Map<Full__APIUser>(userFound);

            if (preparedUser == null || userFound == null) return null;

            return preparedUser;
        }

        [HttpPost]
        [Route("details/{userId}")]
        [Authorize(Roles = ("User"))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<Full__APIUser> USER__UPDATE__FULL([FromForm] Full__APIUser DTO)
        {
            APIUser userFound = _mapper.Map<APIUser>(DTO);

            await _UM.UpdateAsync(userFound);

            Full__APIUser _DTO = _mapper.Map<Full__APIUser>(userFound);

            return _DTO;
        }

                [HttpPost]
        [Route("details/{userId}")]
        [Authorize(Roles = ("User"))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<Full__APIUser> USER__UPDATE__PASSWORD([FromForm] Full__APIUser DTO)
        {
            APIUser userFound = _mapper.Map<APIUser>(DTO);
            
            await _UM.UpdateAsync(userFound);

            Full__APIUser _DTO = _mapper.Map<Full__APIUser>(userFound);

            return _DTO;
        }
    }
}
