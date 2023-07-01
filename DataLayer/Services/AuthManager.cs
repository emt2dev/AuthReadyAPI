using AuthReadyAPI.DataLayer.DTOs.APIUser;
using AuthReadyAPI.DataLayer.DTOs.AuthResponse;
using AuthReadyAPI.DataLayer.Interfaces;
using AuthReadyAPI.DataLayer.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthReadyAPI.DataLayer.Services
{
    public class AuthManager : IAuthManager
    {
        private IMapper _mapper;
        private readonly IConfiguration _configs;

        private APIUser _user;
        private UserManager<APIUser> _UM;

        private string _tokenProvider = "AuthReadyAPI";
        private string _refreshToken = "MadeByDavidDuron";

        public AuthManager(IMapper mapper, UserManager<APIUser> userManager, IConfiguration configuration)
        {
            this._mapper = mapper;
            this._UM = userManager;
            this._configs = configuration;
        }

        public async Task<IEnumerable<IdentityError>> API__ADMIN__REGISTER(Base__APIUser DTO)
        {
            _user = _mapper.Map<APIUser>(DTO);
            _user.UserName = DTO.Email;

            var resultOfScript = await _UM.CreateAsync(_user, DTO.Password);
            if (resultOfScript.Succeeded) await _UM.AddToRoleAsync(_user, "User");
            await _UM.AddToRoleAsync(_user, "API_Admin");

            return resultOfScript.Errors;
        }

        public async Task<IEnumerable<IdentityError>> USER__REGISTER(Base__APIUser DTO)
        {
            _user = _mapper.Map<APIUser>(DTO);
            _user.UserName = DTO.Email;

            var resultOfScript = await _UM.CreateAsync(_user, DTO.Password);
            if (resultOfScript.Succeeded) await _UM.AddToRoleAsync(_user, "User");

            return resultOfScript.Errors;
        }

        public async Task<APIUser> USER__DETAILS(string userId)
        {
            APIUser searchForUser = await _UM.FindByIdAsync(userId);

            return searchForUser;
        }

        public async Task<Full__AuthResponseDTO> USER__LOGIN(Base__APIUser DTO)
        {
            _user = await _UM.FindByEmailAsync(DTO.Email);

            bool IsPasswordValid = await _UM.CheckPasswordAsync(_user, DTO.Password);

            if (_user is null || !IsPasswordValid) return null;

            var giveToken = await CreateJwt();
            return new Full__AuthResponseDTO
                    { Token = giveToken, UserId = _user.Id, RefreshToken = await CreateRefreshToken() };
        }

        public async Task<string> CreateRefreshToken()
        {
            await _UM.RemoveAuthenticationTokenAsync(_user, _tokenProvider, _refreshToken);
            var newToken = await _UM.GenerateUserTokenAsync(_user, _tokenProvider, _refreshToken);
            var setToken = await _UM.SetAuthenticationTokenAsync(_user, _tokenProvider, _refreshToken, newToken);

            return newToken;
        }

        public async Task<Full__AuthResponseDTO> VerifyRefreshToken(Full__AuthResponseDTO DTO)
        {
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var tokenContent = jwtSecurityTokenHandler.ReadJwtToken(DTO.Token);

            var username = tokenContent.Claims.ToList().FirstOrDefault(found => found.Type == JwtRegisteredClaimNames.Email)?.Value;

            _user = await _UM.FindByNameAsync(username);

            if (_user == null || _user.Id != DTO.UserId) return null;

            var isValidRefreshToken = await _UM.VerifyUserTokenAsync(_user, _tokenProvider, _refreshToken, DTO.RefreshToken);

            if (isValidRefreshToken)
            {
                var newToken = await CreateJwt();

                return new Full__AuthResponseDTO
                {
                    Token = newToken,
                    UserId = _user.Id,
                    RefreshToken = await CreateRefreshToken()
                };
            }

            await _UM.UpdateSecurityStampAsync(_user);

            return null;
        }

        private async Task<string> CreateJwt()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configs["JwtSettings:Key"]));

            var userCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var userRoles = await _UM.GetRolesAsync(_user);

            var userRoleClaims = userRoles.Select(role => new Claim(ClaimTypes.Role, role)).ToList();

            /* Below is the method to retrieve claims that are stored in database */
            var userClaims = await _UM.GetClaimsAsync(_user);

            var userClaimsList = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, _user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, _user.Email),
                new Claim("uid", _user.Id), // custom claim user id
            }
            .Union(userClaims)
            .Union(userRoleClaims);

            var newToken = new JwtSecurityToken(
                issuer: _configs["JwtSettings:Issuer"],
                audience: _configs["JwtSettings:Audience"],
                claims: userClaimsList,
                expires: DateTime.Now
                    .AddMinutes(Convert.ToDouble(
                    _configs["JwtSettings:DurationInMinutes"])),

                signingCredentials: userCredentials
                );

            return new JwtSecurityTokenHandler()
                .WriteToken(newToken);
        }
    }
}
