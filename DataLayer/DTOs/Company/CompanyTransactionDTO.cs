using System.ComponentModel.DataAnnotations.Schema;

namespace AuthReadyAPI.DataLayer.DTOs.Company
{
    public class CompanyTransactionDTO
    {
        public int Id { get; set; }
        public DateTime TimeOfTransaction { get; set; }
        public double SaleGross { get; set; }
        public double CompanyNet { get; set; }
        public double AuthReadyNet { get; set; }
        public bool DepositedToCompany { get; set; }
        public DateTime DepositTime { get; set; }
        public string TransactionType { get; set; } // ProductClass,ServicesClass,AuctionProductClass,SingleProductClass
        public int TransactionTypeId { get; set; }

        public string UserEmail { get; set; }

        [ForeignKey(nameof(CompanyId))]
        public int CompanyId { get; set; }

        [ForeignKey(nameof(OrderId))]
        public int OrderId { get; set; }
    }
}
