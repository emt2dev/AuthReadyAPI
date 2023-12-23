using AuthReadyAPI.DataLayer.DTOs.Company;
using AuthReadyAPI.DataLayer.DTOs.PII.APIUser;
using AuthReadyAPI.DataLayer.Interfaces;
using AuthReadyAPI.DataLayer.Models.Companies;
using AuthReadyAPI.DataLayer.Models.PII;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace AuthReadyAPI.DataLayer.Repositories
{
    public class CompanyRepository : ICompany
    {
        private readonly AuthDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<APIUserClass> _UM;

        public CompanyRepository(AuthDbContext context, IMapper mapper, UserManager<APIUserClass> UM)
        {
            this._context = context;
            this._mapper = mapper;
            this._UM = UM;
        }

        public Task<string> COMPANY__GIVE__ADMIN(APIUserDTO DTO)
        {
            throw new NotImplementedException();
        }
    }
}
