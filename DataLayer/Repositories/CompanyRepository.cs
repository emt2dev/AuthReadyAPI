using AuthReadyAPI.DataLayer.DTOs.APIUser;
using AuthReadyAPI.DataLayer.DTOs.Company;
using AuthReadyAPI.DataLayer.Interfaces;
using AuthReadyAPI.DataLayer.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace AuthReadyAPI.DataLayer.Repositories
{
    public class CompanyRepository : GenericRepository<Company>, ICompany
    {
        private readonly AuthDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<APIUser> _UM;

        public CompanyRepository(AuthDbContext context, IMapper mapper, UserManager<APIUser> UM) : base(context, mapper)
        {
            this._context = context;
            this._mapper = mapper;
            this._UM = UM;
        }

        public async Task<string> COMPANY__GIVE__ADMIN(Full__APIUser DTO)
        {
            APIUser newUser = new()
            {
                Email = DTO.Email,
                Name = DTO.Name,
                UserName = DTO.Email,
                IsStaff = DTO.IsStaff,
                PhoneNumber = DTO.PhoneNumber,
            };

            _ = await _UM.CreateAsync(newUser, DTO.Password);
            _ = await _UM.AddToRoleAsync(newUser, "Company_Admin");

            return newUser.UserName;
        }
    }
}
