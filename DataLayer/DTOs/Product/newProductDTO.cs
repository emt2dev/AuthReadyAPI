namespace AuthReadyAPI.DataLayer.DTOs.Product
{
    public class NewProductDTO
    {
        public int Id { get; set; }

        // Information
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

        // FKeys
        public string UpdatedBy { get; set; }
        public int CompanyId { get; set; }
        public int CategoryId { get; set; }

        // Stripe Info
        public string TaxCode { get; set; }
    }
}