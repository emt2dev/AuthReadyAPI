using System.ComponentModel.DataAnnotations.Schema;

namespace AuthReadyAPI.DataLayer.Models.Companies
{
    public class CompanyTransactionClass
    {
        // Below are example amounts, make sure you charge a health and competitve rate
        // remember that whatever your "net" is, you have to subtract stripe's fees.

        public int Id { get; set; }
        public DateTime TimeOfTransaction { get; set; }
        public double SaleGross { get; set; }
        public double CompanyNet { get; set; }
        public double AuthReadyNet { get; set; }
        public bool DepositedToCompany { get; set; } // manually change this
        public DateTime DepositTime { get; set; }
        public string TransactionType { get; set; } // ProductClass,ServicesClass,AuctionProductClass,SingleProductClass
        public int TransactionTypeId { get; set; }

        public string UserEmail { get; set; }

        [ForeignKey(nameof(CompanyId))]
        public int CompanyId { get; set; }

        [ForeignKey(nameof(OrderId))]
        public int OrderId { get; set; }

        public CompanyTransactionClass()
        {
            
        }
    }
}
