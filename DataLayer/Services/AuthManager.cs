using AuthReadyAPI.DataLayer.Interfaces;
using AuthReadyAPI.DataLayer.Models.PII;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace AuthReadyAPI.DataLayer.Services
{
    public class AuthManager : IAuthManager
    {
        private IMapper _mapper;
        private readonly IConfiguration _configs;

        private APIUserClass _user;
        private UserManager<APIUserClass> _UM;

        private string _tokenProvider = "AuthReadyAPI";
        private string _refreshToken = "MadeByDavidDuron";

        public AuthManager(IMapper mapper, UserManager<APIUserClass> userManager, IConfiguration configuration)
        {
            this._mapper = mapper;
            this._UM = userManager;
            this._configs = configuration;
        }

    }
}
