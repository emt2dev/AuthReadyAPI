using System.ComponentModel.DataAnnotations.Schema;

namespace AuthReadyAPI.DataLayer.DTOs.Food
{
    public class FoodOrderDTO
    {
        public int Id { get; set; }
        public bool ReceivedByKitchen { get; set; }
        public bool GivenToCustomer { get; set; }
        public bool Refunded { get; set; }

        public double TotalSpent { get; set; }


        [ForeignKey(nameof(CompanyId))]
        public int CompanyId { get; set; }

        [ForeignKey(nameof(FoodCartClassId))]
        public int FoodCartClassId { get; set; }
    }
}
