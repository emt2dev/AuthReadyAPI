﻿using AuthReadyAPI.DataLayer.DTOs.Product;
using AuthReadyAPI.DataLayer.DTOs.Services;
using AuthReadyAPI.DataLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace AuthReadyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly IServices _services;

        public ServicesController(IServices services)
        {
            _services = services;
        }

        // This controller is for services rendered (non-phyiscal products)
        [HttpGet]
        [Route("list/all")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<ServicesDTO>> GetCompanyServices(int CompanyId)
        {
            return await _services.GetCompanyServicesOffered(CompanyId);
        }

        [HttpPost]
        [Route("list/category")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<ServicesDTO>> FindCompanyServices([FromForm] string Description, int CompanyId)
        {
            return await _services.FindCompanyServicesOffered(CompanyId, Description);
        }

        [HttpPost]
        [Route("new")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<bool> AddService([FromForm] NewServicesDTO DTO, int CompanyId)
        {
            return await _services.AddService(DTO);
        }

        [HttpPut]
        [Route("update")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<bool> UpdateOffering([FromForm] ServicesDTO DTO)
        {
            return await _services.UpdateService(DTO);
        }

        [HttpDelete]
        [Route("delete")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<bool> DeleteOffering([FromForm] int ServiceId)
        {
            return await _services.DeleteService(ServiceId);
        }
    }
}
