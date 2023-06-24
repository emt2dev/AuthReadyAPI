using AuthReadyAPI.DataLayer.DTOs;
using AuthReadyAPI.DataLayer.DTOs.APIUser;
using AuthReadyAPI.DataLayer.Interfaces;
using AuthReadyAPI.DataLayer.Models;
using AuthReadyAPI.DataLayer.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AuthReadyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private IMapper _mapper;
        private IAuthManager _IAM;
        private Iinit _start;
        private readonly ILogger<AuthController> _LOGS;

        private APIUser _user;

        public AuthController(Iinit start, ILogger<AuthController> LOGS, IAuthManager IAM, IMapper mapper)
        {
            this._start = start;
            this._LOGS = LOGS;
            this._IAM = IAM;
            this._mapper = mapper;
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

        /* call this route first, this allows to seed the db with users. */
        [HttpGet]
        public async Task<string> Init()
        {
            Full__APIUser _user001 = new Full__APIUser
            {
                Name = "user 001",
                PhoneNumber = "123-456-7890",
                Email = "user001@example.com",
                Password = "P@ssword1",
            };
            Full__APIUser _user002 = new Full__APIUser
            {
                Name = "user 002",
                PhoneNumber = "123-456-7890",
                Email = "user002@example.com",
                Password = "P@ssword1",
            };
            Full__APIUser _user003 = new Full__APIUser
            {
                Name = "user 003",
                PhoneNumber = "123-456-7890",
                Email = "user003@example.com",
                Password = "P@ssword1",
            };
            Full__APIUser _user004 = new Full__APIUser
            {
                Name = "user 004",
                PhoneNumber = "123-456-7890",
                Email = "user004@example.com",
                Password = "P@ssword1",
            };
            Full__APIUser _user005 = new Full__APIUser
            {
                Name = "user 005",
                PhoneNumber = "123-456-7890",
                Email = "user005@example.com",
                Password = "P@ssword1",
            };
            Full__APIUser _admin001 = new Full__APIUser
            {
                Name = "admin 001",
                PhoneNumber = "123-456-7890",
                Email = "admin001@example.com",
                Password = "P@ssword1",
            };
            Full__APIUser _admin002 = new Full__APIUser
            {
                Name = "admin 002",
                PhoneNumber = "123-456-7890",
                Email = "admin002@example.com",
                Password = "P@ssword1",
            };

            var userAdded = await _start.INIT__START(_user001);
            userAdded = await _start.INIT__START(_user002);
            userAdded = await _start.INIT__START(_user003);
            userAdded = await _start.INIT__START(_user004);
            userAdded = await _start.INIT__START(_user005);

            var adminAdded = await _start.INIT__START(_admin001);
            adminAdded = await _start.INIT__START(_admin002);

            return "initialized";
        }
    }
}
