using AuthReadyAPI.DataLayer.DTOs.PII.APIUser;
using Microsoft.AspNetCore.Identity;

namespace AuthReadyAPI.DataLayer.Interfaces
{
    public interface IAuthRepository
    {
        public Task<IEnumerable<IdentityError>> RegisterUser(NewUserDTO DTO);
        public Task<IEnumerable<IdentityError>> RegisterCompanyUser(NewUserDTO DTO);
        public Task<IEnumerable<IdentityError>> RegisterAdminUser(NewUserDTO DTO);
        public Task<TokensDTO> Login(LoginDTO DTO);
        public Task<string> CreateRefreshToken();
        public Task<string> CreateJwt(int CompanyId);
        public Task<TokensDTO> RefreshTokens(TokensDTO DTO);
        public Task<int> ReadCompanyId(string Jwt);
        public Task<string> ReadUserId(string Jwt);
    }
}
