using AuthReadyAPI.DataLayer.DTOs.APIUser;
using AuthReadyAPI.DataLayer.DTOs.AuthResponse;
using AuthReadyAPI.DataLayer.Interfaces;
using AuthReadyAPI.DataLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Stripe;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthReadyAPI.DataLayer.Services
{
    public class v2_AuthManager : IV2_AuthManager
    {
        private readonly IConfiguration _configs;
        private UserManager<v2_UserStripe> _UM;
        private readonly int companyId = 1;
        private readonly string _tokenProvider = "AuthReadyAPI";
        private readonly string _refreshToken = "MadeByDavidDuron";
        

        public v2_AuthManager(UserManager<v2_UserStripe> UM, IConfiguration configuration)
        {
            this._UM = UM;
            this._configs = configuration;
        }

        //Below are the methods to register all types of users
        public async Task<IEnumerable<IdentityError>> registerCustomer(Base__APIUser incomingDTO)
        {
            v2_UserStripe _user = new v2_UserStripe {
                Email = incomingDTO.Email,
                UserName = incomingDTO.Email,
            };

            var resultOfScript = await _UM.CreateAsync(_user, incomingDTO.Password);
            if (resultOfScript.Succeeded) await _UM.AddToRoleAsync(_user, "Customer");

            return resultOfScript.Errors;
        }

        public async Task<IEnumerable<IdentityError>> registerStaff(Base__APIUser incomingDTO)
        {
            v2_UserStripe _user = new v2_UserStripe {
                Email = incomingDTO.Email,
                UserName = incomingDTO.Email,
                companyId = companyId
            };

            var resultOfScript = await _UM.CreateAsync(_user, incomingDTO.Password);
            if (resultOfScript.Succeeded) await _UM.AddToRoleAsync(_user, "Staff");

            return resultOfScript.Errors;
        }

        public async Task<IEnumerable<IdentityError>> registerDeveloper(Base__APIUser incomingDTO)
        {
            v2_UserStripe _user = new v2_UserStripe {
                Email = incomingDTO.Email,
                UserName = incomingDTO.Email,
                giveDeveloperPrivledges = true,
            };

            var resultOfScript = await _UM.CreateAsync(_user, incomingDTO.Password);
            if (resultOfScript.Succeeded) await _UM.AddToRoleAsync(_user, "Developer");

            return resultOfScript.Errors;
        }

        public async Task<Full__AuthResponseDTO> loginCustomer(Base__APIUser incomingDTO)
        {
            v2_UserStripe _user = await _UM.FindByEmailAsync(incomingDTO.Email);

            bool IsPasswordValid = await _UM.CheckPasswordAsync(_user, incomingDTO.Password);

            if (_user is null || !IsPasswordValid) return null;

            var giveToken = await getNewJWTForCustomers(_user);
            return new Full__AuthResponseDTO { Token = giveToken, UserId = _user.Id, RefreshToken = await newRefreshTokenForCustomer(_user) };
        }

        public async Task<Full__AuthResponseDTO> loginStaff(Base__APIUser incomingDTO)
        {
            v2_UserStripe _user = await _UM.FindByEmailAsync(incomingDTO.Email);

            bool IsPasswordValid = await _UM.CheckPasswordAsync(_user, incomingDTO.Password);

            if (_user is null || !IsPasswordValid) return null;

            string giveToken = await getNewJWTForStaff(_user);
            string refreshToken = await newRefreshTokenForStaff(_user);

            Full__AuthResponseDTO outgoingDTO = new Full__AuthResponseDTO
            {
                Token = giveToken,
                UserId = _user.Id,
                RefreshToken = refreshToken,
            };

            return outgoingDTO;
        }

        public async Task<Full__AuthResponseDTO> loginDeveloper(Base__APIUser incomingDTO)
        {
            v2_UserStripe _user = await _UM.FindByEmailAsync(incomingDTO.Email);

            bool IsPasswordValid = await _UM.CheckPasswordAsync(_user, incomingDTO.Password);

            if (_user is null || !IsPasswordValid) return null;

            string giveToken = await getNewJWTForDevelopers(_user);
            string refreshToken = await newRefreshTokenForDeveloper(_user);

            Full__AuthResponseDTO outgoingDTO = new Full__AuthResponseDTO
            {
                Token = giveToken,
                UserId = _user.Id,
                RefreshToken = refreshToken,
            };

            return outgoingDTO;
        }

        public async Task<string> newRefreshTokenForCustomer(v2_UserStripe _user)
        {
            await _UM.RemoveAuthenticationTokenAsync(_user, _tokenProvider, _refreshToken);
            var newToken = await _UM.GenerateUserTokenAsync(_user, _tokenProvider, _refreshToken);
            var setToken = await _UM.SetAuthenticationTokenAsync(_user, _tokenProvider, _refreshToken, newToken);

            return newToken;
        }

        public async Task<string> newRefreshTokenForDeveloper(v2_UserStripe _user)
        {
            await _UM.RemoveAuthenticationTokenAsync(_user, _tokenProvider, _refreshToken);
            var newToken = await _UM.GenerateUserTokenAsync(_user, _tokenProvider, _refreshToken);
            var setToken = await _UM.SetAuthenticationTokenAsync(_user, _tokenProvider, _refreshToken, newToken);

            return newToken;
        }

        public async Task<string> newRefreshTokenForStaff(v2_UserStripe _user)
        {
            await _UM.RemoveAuthenticationTokenAsync(_user, _tokenProvider, _refreshToken);
            var newToken = await _UM.GenerateUserTokenAsync(_user, _tokenProvider, _refreshToken);
            var setToken = await _UM.SetAuthenticationTokenAsync(_user, _tokenProvider, _refreshToken, newToken);

            return newToken;
        }

        public async Task<string> getNewJWTForCustomers(v2_UserStripe _user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configs["JwtSettings:Key"]));

            var userCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var userRoles = await _UM.GetRolesAsync(_user);

            List<Claim> userRoleClaims = userRoles.Select(role => new Claim(ClaimTypes.Role, role)).ToList();

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

        public async Task<string> getNewJWTForStaff(v2_UserStripe _user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configs["JwtSettings:Key"]));

            var userCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var userRoles = await _UM.GetRolesAsync(_user);

            List<Claim> userRoleClaims = userRoles.Select(role => new Claim(ClaimTypes.Role, role)).ToList();

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

        public async Task<string> getNewJWTForDevelopers(v2_UserStripe _user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configs["JwtSettings:Key"]));

            var userCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var userRoles = await _UM.GetRolesAsync(_user);

            List<Claim> userRoleClaims = userRoles.Select(role => new Claim(ClaimTypes.Role, role)).ToList();

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

        public async Task<v2_UserStripe> getCustomerDetails(string customerId)
        {
            v2_UserStripe _user = await _UM.FindByIdAsync(customerId.ToString());
            if(_user == null) return null;
            else return _user;
        }

        public async Task<v2_UserStripe> getStaffDetails(string staffId)
        {
            v2_UserStripe _user = await _UM.FindByIdAsync(staffId.ToString());
            if(_user == null) return null;
            else return _user;
        }

        public async Task<v2_UserStripe> getDeveloperDetails(string developerId)
        {
            v2_UserStripe _user = await _UM.FindByIdAsync(developerId.ToString());
            if(_user == null) return null;
            else return _user;
        }

        public async Task<Stripe.Customer> addStripeCustomer(string email)
        {
            v2_UserStripe _user = await _UM.FindByEmailAsync(email);
            if(_user == null) return null;

            var options = new CustomerCreateOptions
            {
                Name = _user.name,
                Email = _user.Email,
            };

            var service = new CustomerService();
            var result = await service.CreateAsync(options);

            return result;
        }

        public async Task<Full__AuthResponseDTO> VerifyRefreshToken(Full__AuthResponseDTO DTO)
        {
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var tokenContent = jwtSecurityTokenHandler.ReadJwtToken(DTO.Token);

            var email = tokenContent.Claims.ToList().FirstOrDefault(found => found.Type == JwtRegisteredClaimNames.Email)?.Value;

            v2_UserStripe _user = await _UM.FindByEmailAsync(email);

            var isValidCustomer = await _UM.VerifyUserTokenAsync(_user, _tokenProvider, _refreshToken, DTO.RefreshToken);
            var isValidStaff = await _UM.VerifyUserTokenAsync(_user, _tokenProvider, _refreshToken, DTO.RefreshToken);

            if (isValidCustomer)
            {
                await _UM.UpdateSecurityStampAsync(_user);

                var newToken = await getNewJWTForCustomers(_user);

                return new Full__AuthResponseDTO
                {
                    Token = newToken,
                    UserId = _user.Id,
                    RefreshToken = await newRefreshTokenForCustomer(_user)
                };
            }

            return null;
        }
    }
}
