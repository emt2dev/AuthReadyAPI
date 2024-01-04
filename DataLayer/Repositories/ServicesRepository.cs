using AuthReadyAPI.DataLayer.DTOs.PII.Payments;
using AuthReadyAPI.DataLayer.DTOs.Services;
using AuthReadyAPI.DataLayer.Interfaces;
using AuthReadyAPI.DataLayer.Models.PII;
using AuthReadyAPI.DataLayer.Models.ServicesInfo;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AuthReadyAPI.DataLayer.Repositories
{
    public class ServicesRepository : IServicesRepository
    {
        private readonly AuthDbContext _context;
        private readonly IMediaService _mediaService;
        private readonly IMapper _mapper;
        private readonly UserManager<APIUserClass> _userManager;
        public ServicesRepository(AuthDbContext context, IMapper mapper, IMediaService mediaService, UserManager<APIUserClass> userManager)
        {
            _context = context;
            _mediaService = mediaService;
            _mapper = mapper;
            _userManager = userManager;
        }
        public async Task<bool> AddService(NewServicesDTO DTO)
        {
            ServicesClass Class = new ServicesClass(DTO);
            _context.Services.AddAsync(Class);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteService(int ServiceId)
        {
            ServicesClass Class = await _context.Services.Where(x => x.Id == ServiceId).FirstOrDefaultAsync();
            if (Class is null) return true;

            _context.Services.Remove(Class);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<ServicesDTO>> FindCompanyServicesOffered(int CompanyId, string Description)
        {
            return await _context.Services.Where(x => x.Name.Contains(Description) || x.Description.Contains(Description)).ProjectTo<ServicesDTO>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<List<ServicesDTO>> GetCompanyServicesOffered(int CompanyId)
        {
            return await _context.Services.Where(x => x.CompanyId == CompanyId).ProjectTo<ServicesDTO>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<bool> UpdateService(ServicesDTO DTO)
        {
            _context.Services.Update(_mapper.Map<ServicesClass>(DTO));
            await _context.SaveChangesAsync();

            return true;
        }

        // Workflow
        public async Task<AppointmentClass> ScheduleAppointment(NewAppointmentDTO DTO)
        {
            AppointmentClass Obj = new AppointmentClass(DTO);

            foreach (var item in DTO.ServicesClassIds)
            {
                ServicesClass Svc = await _context.Services.Where(x => x.Id == item).FirstOrDefaultAsync();
                Obj.AddService(Svc);
            }

            _context.ChangeTracker.Clear();

            foreach (var Prod in DTO.ServicesProductIds)
            {
                ServiceProductClass Product = await _context.ServiceProducts.Where(x => x.Id == Prod).FirstOrDefaultAsync();
                Obj.AddProduct(Product);
            }

            _context.ChangeTracker.Clear();

            await _context.Appointments.AddAsync(Obj);
            await _context.SaveChangesAsync();

            return Obj;
        }

        public async Task<ServicesCartDTO> IssueNewServiceCart(AppointmentClass Class)
        {
            List<ServicesCartClass> Carts = await _context.ServiceCarts.Where(x => x.UserEmail == Class.CustomerEmail && !x.Submitted || x.UserEmail == Class.CustomerEmail && x.Abandoned).ToListAsync();
            foreach (var item in Carts)
            {
                _context.ServiceCarts.Remove(item);
                await _context.SaveChangesAsync();
            }

            List<ServiceProductClass> ProductList = Class.Products;
            Class.Products.Clear();

            ServicesCartClass New = new ServicesCartClass
            {
                Id = 0,
                Appointments = new List<AppointmentClass> { Class },
                Products = ProductList,
                Submitted = false,
                Abandoned = false,
                CouponApplied = false,
                PriceAfterCoupon = 0.00,
                PriceBeforeCoupon = 0.00,
                CouponCodeId = 0,
                CompanyId = Class.CompanyId,
                UserEmail = Class.CustomerEmail,
            };

            foreach (var item in ProductList)
            {
                New.PriceBeforeCoupon += item.Cost * item.Quantity;
            }

            return null;
        }

        public async Task<bool> CustomerArrived(AppointmentShowDTO DTO)
        {
            AppointmentClass Obj = await _context.Appointments.Where(x => x.Id == DTO.AppointmentId).FirstOrDefaultAsync();
            Obj.CustomerShowed = DTO.CustomerArrived;

            _context.Appointments.Update(Obj);
            await _context.SaveChangesAsync();

            return false;

        }
        public string SubmitServicesOrder(AppointmentClass Obj)
        {


            return "worked";
        }
    }
}
