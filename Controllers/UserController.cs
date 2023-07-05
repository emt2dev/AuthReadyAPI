using AuthReadyAPI.DataLayer.DTOs.APIUser;
using AuthReadyAPI.DataLayer.Interfaces;
using AuthReadyAPI.DataLayer.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPut]
        [Route("update/{userId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<Full__APIUser> USER__UPDATE__DETAILS(Full__APIUser DTO)
        {
            APIUser userFound = _mapper.Map<APIUser>(DTO);

            _ = await _UM.UpdateAsync(userFound);
            
            Full__APIUser _DTO = _mapper.Map<Full__APIUser>(userFound);

            return _DTO;
        }

        [HttpPatch]
        [Route("update/{userId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // if validation fails, send this
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // If client issues
        [ProducesResponseType(StatusCodes.Status200OK)] // if okay
        public async Task<string> USER__UPDATE__PASSWORD(userUpdatesDTO DTO)
        {
            APIUser userFound = await _UM.FindByEmailAsync(DTO.email);
            _ = await _UM.ChangePasswordAsync(userFound, DTO.currentPassword, DTO.newPassword);

            return "password updated";
        }
    }
}
