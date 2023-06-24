using AuthReadyAPI.DataLayer.DTOs.APIUser;
using AuthReadyAPI.DataLayer.Interfaces;
using AuthReadyAPI.DataLayer.Models;
using AutoMapper;
using Humanizer;
using Microsoft.AspNetCore.Identity;

namespace AuthReadyAPI.DataLayer.Services
{
    public class InitService : Iinit
    {
        private APIUser _user;
        private UserManager<APIUser> _UM;
        private IMapper _mapper;

        public InitService(UserManager<APIUser> userManager, IMapper mapper)
        {
            this._UM = userManager;
            this._mapper = mapper;

        }
        public async Task<string> INIT__START(Full__APIUser initObj)
        {
            _user = _mapper.Map<APIUser>(initObj);
            _user.UserName = initObj.Email;

            await _UM.CreateAsync(_user, initObj.Password);
            await _UM.AddToRoleAsync(_user, "User");

            return "Ready to begin";
        }
    }
}
