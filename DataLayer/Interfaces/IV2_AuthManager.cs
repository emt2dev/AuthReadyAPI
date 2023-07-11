using AuthReadyAPI.DataLayer.DTOs.APIUser;
using AuthReadyAPI.DataLayer.DTOs.AuthResponse;
using AuthReadyAPI.DataLayer.Models;
using Microsoft.AspNetCore.Identity;

namespace AuthReadyAPI.DataLayer.Interfaces
{
    public interface IV2_AuthManager
    {

        Task<IEnumerable<IdentityError>> registerCustomer(v2_CustomerDTO incomingDTO);
        Task<IEnumerable<IdentityError>> registerDeveloper(v2_StaffDTO incomingDTO);
        Task<IEnumerable<IdentityError>> registerStaff(v2_StaffDTO incomingDTO);

        Task<Full__AuthResponseDTO> loginCustomer(v2_CustomerDTO incomingDTO);
        Task<Full__AuthResponseDTO> loginDeveloper(v2_StaffDTO incomingDTO);
        Task<Full__AuthResponseDTO> loginStaff(v2_StaffDTO incomingDTO);

        Task<v2_Staff> getStaffDetails(string staffId);
        Task<v2_Staff> getDeveloperDetails(string developerId);
        Task<v2_CustomerStripe> getCustomerDetails(string customerId);
        Task<string> getNewJWTForDevelopers();
        Task<string> getNewJWTForStaff();
        Task<string> getNewJWTForCustomers();
        Task<string> newRefreshTokenForStaff();
        Task<string> newRefreshTokenForDeveloper();
        Task<string> newRefreshTokenForCustomer();
        Task<Stripe.Customer> addStripeCustomer(string email);
        Task<Full__AuthResponseDTO> VerifyRefreshToken(Full__AuthResponseDTO DTO);
    }
}
