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
        Company __sendingDTO;

        UserManager<APIUser> _UM;

        public ApiAdminRepository(AuthDbContext context, IMapper mapper, UserManager<APIUser> UM, ICompany company) : base(context, mapper)
        {
            this._context = context;
            this._mapper = mapper;
            this._UM = UM;
            this._company = company;
        }

        public async Task<Base__Company> COMPANY__CREATE(Base__Company DTO)
        {
            DTO.Id = null;
            __sendingDTO = _mapper.Map<Company>(DTO);

            Company CompanyRow = await _company.AddAsync(__sendingDTO);

            Base__Company _DTO = _mapper.Map<Base__Company>(CompanyRow);

            return _DTO;
        }
    }
}
