using AuthReadyAPI.DataLayer.DTOs.APIUser;
using AuthReadyAPI.DataLayer.DTOs.AuthResponse;
using AuthReadyAPI.DataLayer.Models;
using Microsoft.AspNetCore.Identity;

namespace AuthReadyAPI.DataLayer.Interfaces
{
    public interface IAuthManager
    {
        Task<IEnumerable<IdentityError>> USER__REGISTER(Base__APIUser DTO);

        Task<IEnumerable<IdentityError>> API__ADMIN__REGISTER(Base__APIUser DTO);
        Task<APIUser> USER__DETAILS(string userId);
        Task<Full__AuthResponseDTO> USER__LOGIN(Base__APIUser DTO);
        Task<Full__AuthResponseDTO> VerifyRefreshToken(Full__AuthResponseDTO DTO);
        Task<string> CreateRefreshToken();
    }
}
