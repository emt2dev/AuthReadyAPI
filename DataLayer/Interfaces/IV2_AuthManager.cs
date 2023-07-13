using AuthReadyAPI.DataLayer.DTOs.APIUser;
using AuthReadyAPI.DataLayer.DTOs.AuthResponse;
using AuthReadyAPI.DataLayer.Models;
using Microsoft.AspNetCore.Identity;

namespace AuthReadyAPI.DataLayer.Interfaces
{
    public interface IV2_AuthManager
    {
        Task<IEnumerable<IdentityError>> registerCustomer(Base__APIUser incomingDTO);
        Task<IEnumerable<IdentityError>> registerDeveloper(Base__APIUser incomingDTO);
        Task<IEnumerable<IdentityError>> registerStaff(Base__APIUser incomingDTO);
        Task<Full__AuthResponseDTO> loginCustomer(Base__APIUser incomingDTO);
        Task<Full__AuthResponseDTO> loginDeveloper(Base__APIUser incomingDTO);
        Task<Full__AuthResponseDTO> loginStaff(Base__APIUser incomingDTO);
        Task<v2_UserStripe> getStaffDetails(string staffId);
        Task<v2_UserStripe> getDeveloperDetails(string developerId);
        Task<v2_UserStripe> getCustomerDetails(string customerId);
        Task<string> getNewJWTForDevelopers(v2_UserStripe _user);
        Task<string> getNewJWTForStaff(v2_UserStripe _user);
        Task<string> getNewJWTForCustomers(v2_UserStripe _user);
        Task<string> newRefreshTokenForStaff(v2_UserStripe _user);
        Task<string> newRefreshTokenForDeveloper(v2_UserStripe _user);
        Task<string> newRefreshTokenForCustomer(v2_UserStripe _user);
        Task<Stripe.Customer> addStripeCustomer(string email);
        Task<Full__AuthResponseDTO> VerifyRefreshToken(Full__AuthResponseDTO DTO);
    }
}
