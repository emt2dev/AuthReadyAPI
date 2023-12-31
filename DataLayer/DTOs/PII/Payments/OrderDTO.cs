﻿using AuthReadyAPI.DataLayer.DTOs.PII.Payments;
using AuthReadyAPI.DataLayer.DTOs.Product;
using Stripe;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthReadyAPI.DataLayer.DTOs.PII
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public string Status { get; set; }

        // Payment
        public double PaymentAmount { get; set; }
        public bool PaymentCompleted { get; set; }
        public bool PaymentRefunded { get; set; }
        public string StripeOrderId { get; set; } // For reference

        // Fkeys
        [ForeignKey(nameof(UserId))]
        public int UserId { get; set; }
        [ForeignKey(nameof(ShoppingCartId))]
        public int ShoppingCartId { get; set; }
        [ForeignKey(nameof(CompanyId))]
        public int CompanyId { get; set; }

        // Shipping
        /*
        public bool ShippedProduct { get; set; }
        public ShippingInfoDTO ShippingInfo { get; set; }

        // Digital Product
        public bool DigitalProduct { get; set; }
        public DigitalOwnershipDTO DigitalOwnershi { get; set; }
        */


        // Order Types, uncomment per business requiremnets
        // Delivery Drivers
        /*
        public bool delivery { get; set; }
        public DateTime Time__Submitted { get; set; }
        public DateTime? Time__Touched { get; set; }
        public DateTime? Time__Delivered { get; set; }
        public string? delivery_driver_name { get; set; }
        public string? delivery_driver_longitdte { get; set; }
        public string? delivery_driver_latitude { get; set; }
        public string? Destination_latitude { get; set; }
        public string? Destination_longitude { get; set; }
        public string? DestinationAddress { get; set; }
        */
    }
}
