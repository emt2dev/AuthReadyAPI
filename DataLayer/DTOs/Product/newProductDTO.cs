namespace AuthReadyAPI.DataLayer.DTOs.Product
{
    public class NewProductDTO
    {
        // Information
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile ImageUrl { get; set; }

        // FKeys
        public int CompanyId { get; set; }
        public int CategoryId { get; set; }

        // Stripe Info
        public string TaxCode { get; set; }
    }
}