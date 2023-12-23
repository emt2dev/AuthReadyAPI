namespace AuthReadyAPI.DataLayer.DTOs.Product
{
    public class newProductDTO
    {
        public int Id { get; set; }
        // Information
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; } // updated by cloudinary

        // FKeys
        public string UpdatedBy { get; set; } // userId pulled from jwt
        public int CompanyId { get; set; } // which company does the product belong to?
        public string TaxCode { get; set; }
    }
}