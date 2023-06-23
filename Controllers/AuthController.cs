using AuthReadyAPI.DataLayer.DTOs;
using AuthReadyAPI.DataLayer.DTOs.APIUser;
using AuthReadyAPI.DataLayer.Interfaces;
using AuthReadyAPI.DataLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AuthReadyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IStart _start;
        private readonly ILogger<AuthController> _LOGS;

        public AuthController(IStart start, ILogger<AuthController> LOGS)
        {
            this._start = start;
            this._LOGS = LOGS;
        }

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
