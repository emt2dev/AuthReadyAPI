namespace AuthReadyAPI.DataLayer.DTOs.Product
{
    public class CartItem
    {
        public int Id {get;set;}
        public string productId {get;set;}
        public string name {get;set;}
        public double price {get;set;}
        public string imageURL {get;set;}
        public int count {get;set;}
        public string description {get;set;}
    }
}