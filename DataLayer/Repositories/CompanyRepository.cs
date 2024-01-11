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
    public class CompanyRepository : ICompanyRepository
    {
        private readonly AuthDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<APIUserClass> _UM;
        private readonly IMediaService _mediaService;

        public CompanyRepository(AuthDbContext context, IMapper mapper, UserManager<APIUserClass> UM, IMediaService mediaService)
        {
            _context = context;
            _mapper = mapper;
            _UM = UM;
            _mediaService = mediaService;
        }

        public async Task<List<string>> GetCompanyImages(int CompanyId)
        {
            List<string> UrlList = new List<string>();
            List<CompanyImageClass> ImageList = await _context.CompanyImages.Where(x => x.CompanyId == CompanyId).ToListAsync();

            foreach (var item in ImageList)
            {
                UrlList.Add(item.ImageUrl);
            }

            return UrlList;
        }
        public async Task<bool> NewCompanyImage(NewCompanyImageDTO IncomingDTO, int CompanyId)
        {
            int idCount = 0;
            foreach (var item in IncomingDTO.Images)
            {
                idCount++;
                var Result = _mediaService.AddPhotoAsync(item);
                CompanyImageClass Obj = new CompanyImageClass
                {
                    Id = idCount,
                    CompanyId = CompanyId,
                    ImageUrl = Result.Result.Url.ToString(),
                    Name = IncomingDTO.Name
                };

                await _context.CompanyImages.AddAsync(Obj);
                await _context.SaveChangesAsync();
            }

            return true;
        }

        public async Task<bool> NewCompany(NewCompanyDTO IncomingDTO)
        {
            CompanyClass Obj = new CompanyClass(IncomingDTO);
            Obj.Active = false;

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
            Company.Active = true;

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
