using System.ComponentModel.DataAnnotations;

namespace AuthReadyAPI.DataLayer.Models
{
    public class v2_ShoppingCart
    {
        [Key]
        public int Id {get;set;}
        public string customerId {get;set;}
        public string companyId {get;set;}
        public IList<v2_ProductStripe> Items {get;set;} = new List<v2_ProductStripe>();
        public double cost {get;set;}
        public Boolean submitted {get;set;}
        public Boolean abandoned {get;set;}
        public string costInString { get; set; }
    }
}