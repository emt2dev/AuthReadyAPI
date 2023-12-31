﻿using System.ComponentModel.DataAnnotations.Schema;

namespace AuthReadyAPI.DataLayer.Models.ServicesInfo
{
    public class ServiceProductClass
    {
        public int Id { get; set; }

        [ForeignKey(nameof(CompanyId))]
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public double Cost { get; set; }
        public string TaxCode { get; set; }
    }
}
