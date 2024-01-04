﻿using AuthReadyAPI.DataLayer.DTOs.PII.APIUser;
using AuthReadyAPI.DataLayer.DTOs.PII.Payments;
using AuthReadyAPI.DataLayer.DTOs.Product;
using AuthReadyAPI.DataLayer.Interfaces;
using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthReadyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuctionController : ControllerBase
    {
        private readonly IAuctionRepository _auction;
        public AuctionController(IAuctionRepository auction)
        {
            _auction = auction;
        }

        // 
        /// <summary>
        /// Note this functionality needs real-time communication (ie websocket, SignalR, or something similiar) to be successful
        /// This shows the basic concept
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        [Route("active")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<AuctionProductDTO>> GetActiveAuctionProducts()
        {
            return await _auction.GetActiveAuctionProducts();
        }

        [HttpGet]
        [Route("inactive")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<AuctionProductDTO>> GetInactiveAuctionProducts()
        {
            return await _auction.GetInactiveAuctionProducts();
        }

        [HttpPost]
        [Route("create/new")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<bool> CreateAuctionProduct(NewAuctionProductDTO DTO)
        {
            return await _auction.AddAuctionProduct(DTO);
        }

        [HttpPost]
        [Route("submit/bid")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<bool> NewBid(BidDTO DTO)
        {
            return await _auction.AddBid(DTO);
        }

    }
}
