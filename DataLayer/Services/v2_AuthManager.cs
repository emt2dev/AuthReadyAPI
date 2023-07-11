using AuthReadyAPI.DataLayer.DTOs.APIUser;
using AuthReadyAPI.DataLayer.DTOs.AuthResponse;
using AuthReadyAPI.DataLayer.Interfaces;
using AuthReadyAPI.DataLayer.Models;
using AutoMapper;
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
        private v2_CustomerStripe _customer;
        private v2_Staff _staff;
        private UserManager<v2_Staff> _staffUM;
        private readonly UserManager<v2_CustomerStripe> _customerUM;

        private string _tokenProvider = "AuthReadyAPI";
        private string _refreshToken = "MadeByDavidDuron";
        

        public v2_AuthManager(UserManager<v2_Staff> staffUM, UserManager<v2_CustomerStripe> customerUM, IConfiguration configuration)
        {
            this._staffUM = staffUM;
            this._customerUM = customerUM;
            this._configs = configuration;
        }

        //Below are the methods to register all types of users
        public async Task<IEnumerable<IdentityError>> registerCustomer(v2_CustomerDTO incomingDTO)
        {
            _customer.Email = incomingDTO.Email;

            var resultOfScript = await _customerUM.CreateAsync(_customer, incomingDTO.Password);
            if (resultOfScript.Succeeded) await _customerUM.AddToRoleAsync(_customer, "Customer");

            return resultOfScript.Errors;
        }

        public async Task<IEnumerable<IdentityError>> registerStaff(v2_StaffDTO incomingDTO)
        {
            _staff.Email = incomingDTO.Email;

            var resultOfScript = await _staffUM.CreateAsync(_staff, incomingDTO.Password);
            if (resultOfScript.Succeeded) await _staffUM.AddToRoleAsync(_staff, "Staff");

            return resultOfScript.Errors;
        }

        public async Task<IEnumerable<IdentityError>> registerDeveloper(v2_StaffDTO incomingDTO)
        {
            _staff.Email = incomingDTO.Email;
            _staff.giveDeveloperPrivledges = true;

            var resultOfScript = await _staffUM.CreateAsync(_staff, incomingDTO.Password);
            if (resultOfScript.Succeeded) await _staffUM.AddToRoleAsync(_staff, "Developer");

            return resultOfScript.Errors;
        }

        public async Task<Full__AuthResponseDTO> loginCustomer(v2_CustomerDTO incomingDTO)
        {
            _customer = await _customerUM.FindByEmailAsync(incomingDTO.Email);

            bool IsPasswordValid = await _customerUM.CheckPasswordAsync(_customer, incomingDTO.Password);

            if (_customer is null || !IsPasswordValid) return null;

            var giveToken = await getNewJWTForCustomers();
            return new Full__AuthResponseDTO { Token = giveToken, UserId = _customer.Id, RefreshToken = await newRefreshTokenForCustomer() };
        }

        public async Task<Full__AuthResponseDTO> loginStaff(v2_StaffDTO incomingDTO)
        {
            _staff = await _staffUM.FindByEmailAsync(incomingDTO.Email);

            bool IsPasswordValid = await _staffUM.CheckPasswordAsync(_staff, incomingDTO.Password);

            if (_customer is null || !IsPasswordValid) return null;

            string giveToken = await getNewJWTForStaff();
            string refreshToken = await newRefreshTokenForStaff();

            Full__AuthResponseDTO outgoingDTO = new Full__AuthResponseDTO
            {
                Token = giveToken,
                UserId = _staff.Id,
                RefreshToken = refreshToken,
            };

            return outgoingDTO;
        }

        public async Task<Full__AuthResponseDTO> loginDeveloper(v2_StaffDTO incomingDTO)
        {
            _staff = await _staffUM.FindByEmailAsync(incomingDTO.Email);

            bool IsPasswordValid = await _staffUM.CheckPasswordAsync(_staff, incomingDTO.Password);

            if (_customer is null || !IsPasswordValid) return null;

            string giveToken = await getNewJWTForDevelopers();
            string refreshToken = await newRefreshTokenForDeveloper();

            Full__AuthResponseDTO outgoingDTO = new Full__AuthResponseDTO
            {
                Token = giveToken,
                UserId = _staff.Id,
                RefreshToken = refreshToken,
            };

            return outgoingDTO;
        }

        public async Task<string> newRefreshTokenForCustomer()
        {
            await _customerUM.RemoveAuthenticationTokenAsync(_customer, _tokenProvider, _refreshToken);
            var newToken = await _customerUM.GenerateUserTokenAsync(_customer, _tokenProvider, _refreshToken);
            var setToken = await _customerUM.SetAuthenticationTokenAsync(_customer, _tokenProvider, _refreshToken, newToken);

            return newToken;
        }

        public async Task<string> newRefreshTokenForDeveloper()
        {
            await _staffUM.RemoveAuthenticationTokenAsync(_staff, _tokenProvider, _refreshToken);
            var newToken = await _staffUM.GenerateUserTokenAsync(_staff, _tokenProvider, _refreshToken);
            var setToken = await _staffUM.SetAuthenticationTokenAsync(_staff, _tokenProvider, _refreshToken, newToken);

            return newToken;
        }

        public async Task<string> newRefreshTokenForStaff()
        {
            await _staffUM.RemoveAuthenticationTokenAsync(_staff, _tokenProvider, _refreshToken);
            var newToken = await _staffUM.GenerateUserTokenAsync(_staff, _tokenProvider, _refreshToken);
            var setToken = await _staffUM.SetAuthenticationTokenAsync(_staff, _tokenProvider, _refreshToken, newToken);

            return newToken;
        }

        public async Task<string> getNewJWTForCustomers()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configs["JwtSettings:Key"]));

            var userCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var userRoles = await _customerUM.GetRolesAsync(_customer);

            var userRoleClaims = userRoles.Select(role => new Claim(ClaimTypes.Role, role)).ToList();

            var userClaims = await _customerUM.GetClaimsAsync(_customer);

            var userClaimsList = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, _customer.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, _customer.Email),
                new Claim("uid", _customer.Id), // custom claim user id
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

        public async Task<string> getNewJWTForStaff()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configs["JwtSettings:Key"]));

            var userCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var userRoles = await _staffUM.GetRolesAsync(_staff);

            var userRoleClaims = userRoles.Select(role => new Claim(ClaimTypes.Role, role)).ToList();

            var userClaims = await _staffUM.GetClaimsAsync(_staff);

            var userClaimsList = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, _staff.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, _staff.Email),
                new Claim("uid", _staff.Id), // custom claim user id
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

        public async Task<string> getNewJWTForDevelopers()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configs["JwtSettings:Key"]));

            var userCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var userRoles = await _staffUM.GetRolesAsync(_staff);

            var userRoleClaims = userRoles.Select(role => new Claim(ClaimTypes.Role, role)).ToList();

            var userClaims = await _staffUM.GetClaimsAsync(_staff);

            var userClaimsList = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, _staff.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, _staff.Email),
                new Claim("uid", _staff.Id), // custom claim user id
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

        public async Task<v2_CustomerStripe> getCustomerDetails(string customerId)
        {
            _customer = await _customerUM.FindByIdAsync(customerId.ToString());
            if(_customer == null) return null;
            else return _customer;
        }

        public async Task<v2_Staff> getStaffDetails(string staffId)
        {
            _staff = await _staffUM.FindByIdAsync(staffId.ToString());
            if(_staff == null) return null;
            else return _staff;
        }

        public async Task<v2_Staff> getDeveloperDetails(string developerId)
        {
            _staff = await _staffUM.FindByIdAsync(developerId.ToString());
            if(_staff == null) return null;
            else return _staff;
        }

        public async Task<Stripe.Customer> addStripeCustomer(string email)
        {
            _customer = await _customerUM.FindByEmailAsync(email);
            if(_staff == null) return null;

            var options = new CustomerCreateOptions
            {
                Name = _customer.name,
                Email = _customer.Email,
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

            _customer = await _customerUM.FindByEmailAsync(email);
            _staff = await _staffUM.FindByEmailAsync(email);

            var isValidCustomer = await _customerUM.VerifyUserTokenAsync(_customer, _tokenProvider, _refreshToken, DTO.RefreshToken);
            var isValidStaff = await _staffUM.VerifyUserTokenAsync(_staff, _tokenProvider, _refreshToken, DTO.RefreshToken);

            if (isValidCustomer)
            {
                await _customerUM.UpdateSecurityStampAsync(_customer);

                var newToken = await getNewJWTForCustomers();

                return new Full__AuthResponseDTO
                {
                    Token = newToken,
                    UserId = _customer.Id,
                    RefreshToken = await newRefreshTokenForCustomer()
                };
            } else if(isValidStaff) {
                await _staffUM.UpdateSecurityStampAsync(_staff);

                var newToken = await getNewJWTForStaff();

                return new Full__AuthResponseDTO
                {
                    Token = newToken,
                    UserId = _customer.Id,
                    RefreshToken = await newRefreshTokenForCustomer()
                };
            }
            
            return null;
        }
    }
}
