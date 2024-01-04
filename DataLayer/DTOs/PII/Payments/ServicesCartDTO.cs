using AuthReadyAPI.DataLayer.DTOs.Services;
using AuthReadyAPI.DataLayer.Models.ServicesInfo;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthReadyAPI.DataLayer.DTOs.PII.Payments
{
    public class ServicesCartDTO
    {
        public int Id { get; set; }
        public List<AppointmentDTO> Appointments { get; set; }
        public bool Submitted { get; set; }
        public bool Abandoned { get; set; }
        public bool CouponApplied { get; set; }
        public double PriceBeforeCoupon { get; set; }
        public double PriceAfterCoupon { get; set; }

        public string UserEmail { get; set; }

        // Fkeys
        public int CouponCodeId { get; set; }

        [ForeignKey(nameof(CompanyId))]
        public int CompanyId { get; set; }
    }
}
