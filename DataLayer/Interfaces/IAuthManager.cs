using AuthReadyAPI.DataLayer.DTOs.APIUser;
using AuthReadyAPI.DataLayer.DTOs.AuthResponse;
using Microsoft.AspNetCore.Identity;

namespace AuthReadyAPI.DataLayer.Interfaces
{
    public interface IAuthManager
    {
        Task<IEnumerable<IdentityError>> USER__REGISTER(Base__APIUser DTO);
        Task<Full__AuthResponseDTO> USER__LOGIN(Base__APIUser DTO);
        Task<Full__AuthResponseDTO> VerifyTokens(Full__AuthResponseDTO DTO);
        Task<string> CreateRefreshToken();
    }
}
