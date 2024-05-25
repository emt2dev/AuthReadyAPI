using AuthReadyAPI.DataLayer.DTOs.PII.APIUser;
using AuthReadyAPI.DataLayer.Interfaces;
using AuthReadyAPI.DataLayer.Models.PII;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthReadyAPI.DataLayer.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _configs;
        private string _tokenProvider = "AuthReadyAPI";
        private string _refreshToken = "MadeByDavidDuron";
        private APIUserClass _user;
        private UserManager<APIUserClass> _userManager;
        private readonly AuthDbContext _context;

        public AuthRepository(AuthDbContext context, IMapper mapper, UserManager<APIUserClass> userManager, IConfiguration configs)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _configs = configs;
        }

        public async Task<IEnumerable<IdentityError>> RegisterUser(NewUserDTO DTO)
        {
            APIUserClass _user = _mapper.Map<APIUserClass>(DTO);
            _user.UserName = DTO.Email;

            var Attempt = await _userManager.CreateAsync(_user, DTO.Password);
            if (Attempt.Succeeded) await _userManager.AddToRoleAsync(_user, "User");
            return Attempt.Errors;
        }
        public async Task<IEnumerable<IdentityError>> RegisterCompanyUser(NewUserDTO DTO)
        {
            APIUserClass _user = _mapper.Map<APIUserClass>(DTO);
            _user.UserName = DTO.Email;

            var Attempt = await _userManager.CreateAsync(_user, DTO.Password);
            if (Attempt.Succeeded) await _userManager.AddToRoleAsync(_user, "Company");
            return Attempt.Errors;
        }
        public async Task<IEnumerable<IdentityError>> RegisterAdminUser(NewUserDTO DTO)
        {
            APIUserClass _user = _mapper.Map<APIUserClass>(DTO);
            _user.UserName = DTO.Email;

            var Attempt = await _userManager.CreateAsync(_user, DTO.Password);
            if (Attempt.Succeeded) await _userManager.AddToRoleAsync(_user, "Admin");
            return Attempt.Errors;
        }
        public async Task<string> CreateJwt(int CompanyId)
        {
            var SecKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configs["JwtSettings:Key"]));
            var userCreds = new SigningCredentials(SecKey, SecurityAlgorithms.HmacSha256);
            var userRoles = await _userManager.GetRolesAsync(_user);
            var userRoleClaims = userRoles.Select(role => new Claim(ClaimTypes.Role, role)).ToList();
            var userClaims = await _userManager.GetClaimsAsync(_user);
            var TokenDetails = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, _user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.NameId, CompanyId.ToString())
            }
            .Union(userClaims)
            .Union(userRoleClaims);

            var newToken = new JwtSecurityToken(
                issuer: _configs["JwtSettings:Issuer"],
                audience: _configs["JwtSettings:Audience"],
                claims: TokenDetails,
                expires: DateTime.Now
                    .AddMinutes(Convert.ToDouble(
                    _configs["JwtSettings:DurationInMinutes"])),

                signingCredentials: userCreds
                );

            return new JwtSecurityTokenHandler().WriteToken(newToken);
        }
        public async Task<string> CreateRefreshToken()
        {
            await _userManager.RemoveAuthenticationTokenAsync(_user, _tokenProvider, _refreshToken);
            var NewToken = await _userManager.GenerateUserTokenAsync(_user, _tokenProvider, _refreshToken);
            _ = await _userManager.SetAuthenticationTokenAsync(_user, _tokenProvider, _refreshToken, NewToken);

            return NewToken;
        }
        public async Task<TokensDTO> RefreshTokens(TokensDTO DTO)
        {
            var SecHandler = new JwtSecurityTokenHandler();
            var Content = SecHandler.ReadJwtToken(DTO.Token);
            string UserId = Content.Claims.ToList().FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub)?.Value;
            string CompanyId = Content.Claims.ToList().FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.NameId)?.Value;

            if (Int32.Parse(CompanyId).GetType() != typeof(int)) return null;

            _user = await _userManager.FindByIdAsync(UserId);

            _context.ChangeTracker.Clear();

            if (_user is null) return null;

            var validatedRefreshToken = await _userManager.VerifyUserTokenAsync(_user, _tokenProvider, _refreshToken, DTO.RefreshToken);
            if (!validatedRefreshToken) return null;

            var NewJwt = await CreateJwt(Int32.Parse(CompanyId));

            return new TokensDTO
            {
                Token = NewJwt,
                RefreshToken = await CreateRefreshToken()
            };
        }
        public async Task<int> ReadCompanyId(string Jwt)
        {
            var SecHandler = new JwtSecurityTokenHandler();
            var Content = SecHandler.ReadJwtToken(Jwt);
            string CompanyId = Content.Claims.ToList().FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.NameId)?.Value;

            if (Int32.Parse(CompanyId).GetType() != typeof(int)) return 0;

            _context.ChangeTracker.Clear();

            return 1;
        }
        public async Task<string> ReadUserId(string Jwt)
        {
            var SecHandler = new JwtSecurityTokenHandler();
            var Content = SecHandler.ReadJwtToken(Jwt);
            string UserId = Content.Claims.ToList().FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub)?.Value;

            _user = await _userManager.FindByIdAsync(UserId);
            if (_user is null) return string.Empty;
            return UserId;
        }
        public async Task<TokensDTO> Login(LoginDTO DTO)
        {
            _user = await _userManager.FindByEmailAsync(DTO.Email);
            if(_user is null) return null;

            bool ValidPassword = await _userManager.CheckPasswordAsync(_user, DTO.Password);
            if (!ValidPassword) return null;

            var This = await CreateJwt(DTO.CompanyId);
            return new TokensDTO
            {
                Token = This,
                RefreshToken = await CreateRefreshToken()
            };
        }
    }
}
