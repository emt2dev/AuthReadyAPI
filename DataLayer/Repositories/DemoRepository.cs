using AuthReadyAPI.DataLayer.Interfaces;
using AuthReadyAPI.DataLayer.Models;
using AuthReadyAPI.DataLayer.Models.PII;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AuthReadyAPI.DataLayer.Repositories
{
    public class DemoRepository : IDemoRepository
    {
        private readonly AuthDbContext _context;
        private readonly ICacheService _cache;

        public DemoRepository(AuthDbContext context, ICacheService cache)
        {
            _context = context;
            _cache = cache;

        }

        public async Task<bool> CreateNewDemoClasses()
        {
            for (int i = 0; i < 1001; i++)
            {
                CacheDemoClass New = new CacheDemoClass();
                New.RandomGuid = Guid.NewGuid().ToString();

                await _context.demoClasses.AddAsync(New);
                await _context.SaveChangesAsync();
            }

            return true;
        }

        public async Task<List<CacheDemoClass>> ReturnFromDb()
        {
            return await _context.demoClasses.Take(5000).ToListAsync();
        }
    }
}
