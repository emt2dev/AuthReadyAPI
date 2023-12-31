﻿using System.ComponentModel.DataAnnotations.Schema;

namespace AuthReadyAPI.DataLayer.DTOs.Product
{
    public class ProductUpsellItemDTO
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }

        public int Quantity { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CostWithShipping { get; set; }
        public bool DigitalOnly { get; set; }
        public string ImageUrl { get; set; }
    }
}
