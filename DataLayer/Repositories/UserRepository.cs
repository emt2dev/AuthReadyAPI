using AuthReadyAPI.DataLayer.DTOs.APIUser;
using AuthReadyAPI.DataLayer.Interfaces;
using AuthReadyAPI.DataLayer.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace AuthReadyAPI.DataLayer.Repositories
{
    public class UserRepository : GenericRepository<APIUser>, IUser
    {
        private readonly AuthDbContext _context;
        private readonly IMapper _mapper;
        UserManager<APIUser> _UM;

        public UserRepository(AuthDbContext context, IMapper mapper, UserManager<APIUser> UM) : base(context, mapper)
        {
            this._context = context;
            this._mapper = mapper;
            this._UM = UM;
        }

        public async Task<string> USER__CREATE(Full__APIUser initObj)
        {
            APIUser newUser = new()
            {
                Email = initObj.Email,
                Name = initObj.Name,
                UserName = initObj.Email,
                IsStaff = initObj.IsStaff,
                PhoneNumber = initObj.PhoneNumber,
            };

            _ = await _UM.CreateAsync(newUser, initObj.Password);
            _ = await _UM.AddToRoleAsync(newUser, "User");

            return newUser.UserName;
        }

        public async Task<APIUser> USER__FIND__BY__EMAIL__ASYNC(string? email)
        {
            if (email == null) return null;

            APIUser user = await _UM.FindByNameAsync(email);

            return user;
        }
    }
}
