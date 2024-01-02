using AuthReadyAPI.DataLayer.Models.ProductInfo;
using AuthReadyAPI.DataLayer.Models.ServicesInfo;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthReadyAPI.DataLayer.Models.PII
{
    public class ServicesCartClass
    {
        public int Id { get; set; }
        public List<AppointmentClass> Appointments { get; set; }
        public List<ServiceProductClass> Products { get; set; }
        public bool Submitted { get; set; }
        public bool Abandoned { get; set; }
        public bool CouponApplied { get; set; }
        public double PriceBeforeCoupon { get; set; }
        public double PriceAfterCoupon { get; set; }


        // Fkeys
        [ForeignKey(nameof(CouponCodeId))]
        public int CouponCodeId { get; set; }

        [ForeignKey(nameof(UserEmail))]
        public string UserEmail { get; set; }

        [ForeignKey(nameof(CompanyId))]
        public int CompanyId { get; set; }
    }
}
