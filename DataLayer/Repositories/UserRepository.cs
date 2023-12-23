using AuthReadyAPI.DataLayer.DTOs.PII.APIUser;
using AuthReadyAPI.DataLayer.Interfaces;
using AuthReadyAPI.DataLayer.Models.PII;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace AuthReadyAPI.DataLayer.Repositories
{
    public class UserRepository : IUser
    {
        private readonly AuthDbContext _context;
        private readonly IMapper _mapper;
        UserManager<APIUserClass> _UM;

        public UserRepository(AuthDbContext context, IMapper mapper, UserManager<APIUserClass> UM)
        {
            _context = context;
            _mapper = mapper;
            _UM = UM;
        }

        public Task<string> USER__CREATE(APIUserDTO user)
        {
            throw new NotImplementedException();
        }

        public Task<APIUserClass> USER__FIND__BY__EMAIL__ASYNC(string? email)
        {
            throw new NotImplementedException();
        }
    }
}
