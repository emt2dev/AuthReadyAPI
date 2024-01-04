namespace AuthReadyAPI.DataLayer.DTOs.PII.APIUser
{
    public class NewUserDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string BillingAddress { get; set; }
        public string MailingAddress { get; set; }
        public string DateOfBirthString { get; set; }
        public string FullName { get; set; }

        // Metrics
        public double LifetimeSpent { get; set; } // how much they've purchased throughout their account history within the authready ecosystem
        public int OrderCount { get; set; }

        public bool IsCompany { get; set; }
        public int CompanyId { get; set; }
        
        public bool IsAdmin { get; set; }
    }
}
