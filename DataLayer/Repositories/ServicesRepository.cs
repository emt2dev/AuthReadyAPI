using AuthReadyAPI.DataLayer.DTOs.Services;
using AuthReadyAPI.DataLayer.Interfaces;
using AuthReadyAPI.DataLayer.Models.ServicesInfo;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace AuthReadyAPI.DataLayer.Repositories
{
    public class ServicesRepository : IServices
    {
        private readonly AuthDbContext _context;
        private readonly IMediaService _mediaService;
        private readonly IMapper _mapper;
        public ServicesRepository(AuthDbContext context, IMapper mapper, IMediaService mediaService)
        {
            _context = context;
            _mediaService = mediaService;
            _mapper = mapper;
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
        public async Task<bool> ScheduleAppointment(NewAppointmentDTO DTO)
        {
            AppointmentClass Obj = new AppointmentClass(DTO);

            foreach (var item in DTO.ServicesClassIds)
            {
                ServicesClass Svc = await _context.Services.Where(x => x.Id == item).FirstOrDefaultAsync();
                Obj.AddService(Svc);
            }

            _context.ChangeTracker.Clear();

            await _context.Appointments.AddAsync(Obj);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> CustomerArrived(AppointmentShowDTO DTO)
        {
            AppointmentClass Obj = await _context.Appointments.Where(x => x.Id == DTO.AppointmentId).FirstOrDefaultAsync();
            Obj.CustomerShowed = DTO.CustomerArrived;

            _context.Appointments.Update(Obj);
            await _context.SaveChangesAsync();

            return false;

        }
        public Task<string> SubmitServicesOrder(AppointmentClass Obj)
        {


            return "worked";
        }
    }
}
