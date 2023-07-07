using System.ComponentModel.DataAnnotations.Schema;
using AuthReadyAPI.DataLayer.DTOs.Product;

namespace AuthReadyAPI.DataLayer.Models
{
    public class shoppingCart
    {
        public int Id {get;set;}
        public string customerId {get;set;}
        public string companyId {get;set;}
        public IList<CartItem> Items {get;set;} = new List<CartItem>();
        public double cost {get;set;}
        public Boolean submitted {get;set;}
        public Boolean abandoned {get;set;}
    }
}