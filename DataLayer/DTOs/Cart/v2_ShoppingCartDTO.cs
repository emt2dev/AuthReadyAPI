using AuthReadyAPI.DataLayer.DTOs.Product;

namespace AuthReadyAPI.DataLayer.Models
{
    public class v2_ShoppingCartDTO
    {
        public int id {get;set;}
        public string customerId {get;set;}
        public int companyId {get;set;}
        public IList<v2_ProductDTO> Items {get;set;} = new List<v2_ProductDTO>();
        public double cost {get;set;}
        public Boolean submitted {get;set;}
        public Boolean abandoned {get;set;}
        public string costInString { get; set; }
    }
}