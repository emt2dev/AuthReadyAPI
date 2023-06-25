using AuthReadyAPI.DataLayer.DTOs.APIUser;
using AuthReadyAPI.DataLayer.DTOs.Company;
using AuthReadyAPI.DataLayer.Interfaces;
using AuthReadyAPI.DataLayer.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace AuthReadyAPI.DataLayer.Repositories
{
    public class ApiAdminRepository : GenericRepository<APIUser>, IApiAdmin
    {
        private readonly AuthDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICompany _company;

        UserManager<APIUser> _UM;

        public ApiAdminRepository(AuthDbContext context, IMapper mapper, UserManager<APIUser> UM, ICompany company) : base(context, mapper)
        {
            this._context = context;
            this._mapper = mapper;
            this._UM = UM;
            this._company = company;
        }

        public async Task<Full__Company> COMPANY__CREATE(Full__Company DTO)
        {
            Company newCompany = new()
            {
                Name = DTO.Name,
                Description = DTO.Description,
                PhoneNumber = DTO.PhoneNumber,
                Address = DTO.Address,
            };

            Company CompanyRow = await _company.AddAsync(newCompany);

            Full__Company _DTO = _mapper.Map<Full__Company>(CompanyRow);

            return _DTO;
        }

        public async Task<string> API__ADMIN__CREATE(Full__APIUser DTO)
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
            _ = await _UM.AddToRoleAsync(newUser, "API_Admin");

            return newUser.UserName + " was created";
        }
    }
}
