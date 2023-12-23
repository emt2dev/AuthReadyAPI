using AuthReadyAPI.DataLayer.DTOs.Company;
using AuthReadyAPI.DataLayer.DTOs.PII.APIUser;
using AuthReadyAPI.DataLayer.Interfaces;
using AuthReadyAPI.DataLayer.Models.Companies;
using AuthReadyAPI.DataLayer.Models.PII;
using AuthReadyAPI.DataLayer.Models.ProductInfo;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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

        public async Task<bool> NewCompany(NewCompanyDTO IncomingDTO)
        {
            CompanyClass Obj = _mapper.Map<CompanyClass>(IncomingDTO);

            await _context.Companies.AddAsync(Obj);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> NewContact(NewPointOfContactDTO IncomingDTO)
        {
            PointOfContactClass Obj = _mapper.Map<PointOfContactClass>(IncomingDTO);

            await _context.PointOfContacts.AddAsync(Obj);
            await _context.SaveChangesAsync();

            _context.ChangeTracker.Clear();

            CompanyClass Company = await _context.Companies.Where(x => x.Id == Obj.CompanyId).FirstOrDefaultAsync();
            Company.PointOfContactId = Obj.Id;

            _context.Companies.Update(Company);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<CompanyDTO>> GetAPICompanyList()
        {
            return await _context.Companies.ProjectTo<CompanyDTO>(_mapper.ConfigurationProvider).ToListAsync();
        }
    }
}
