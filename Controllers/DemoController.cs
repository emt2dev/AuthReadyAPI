using AuthReadyAPI.DataLayer.Interfaces;
using AuthReadyAPI.DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;

namespace AuthReadyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        private readonly IDemoRepository _demoRepository;
        private readonly ICacheService _cache;
        public DemoController(IDemoRepository demoRepository, ICacheService cache)
        {
            _demoRepository = demoRepository;
            _cache = cache;
        }

        [HttpGet]
        [Route("seed")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<bool> Create1000()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            for (int i = 0; i < 60; i++)
            {
                await _demoRepository.CreateNewDemoClasses();
            }

            stopwatch.Stop();

            await Console.Out.WriteAsync($"\nTime to add to DB: {stopwatch.Elapsed.ToString()}\n");

            return true;
        }

        [HttpGet]
        [Route("db")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<CacheDemoClass>> GetFromDB()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            List<CacheDemoClass> List = await _demoRepository.ReturnFromDb();

            stopwatch.Stop();

            await Console.Out.WriteAsync($"\nDB Count: {List.Count()}\n");
            await Console.Out.WriteAsync($"\nTime to get from DB: {stopwatch.Elapsed.ToString()}\n");

            // 1.54 seconds

            Stopwatch stopwatch2 = Stopwatch.StartNew();
            await _cache.RemoveData("DemoClassCache");

            // new expiration
            var ExpirationTime = DateTime.Now.AddSeconds(35);

            _ = await _cache.SetData<List<CacheDemoClass>>("DemoClassCache", List, ExpirationTime);
            stopwatch2.Stop();

            await Console.Out.WriteAsync($"\nTime to add to Cache: {stopwatch2.Elapsed.ToString()}\n");

            // 6.34 seconds to add to cache 65,585 records
            return List;
        }

        [HttpGet]
        [Route("both")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<CacheDemoClass>> Full()
        {
            //await _cache.RemoveData("DemoClassCache");
            Stopwatch stopwatch = Stopwatch.StartNew();

            var CachedList = await _cache.GetData<IEnumerable<CacheDemoClass>>("DemoClassCache");

            if(CachedList != null && CachedList.Count() > 0)
            {
                stopwatch.Stop();
                await Console.Out.WriteAsync($"\nCache Count: {CachedList.Count()}\n");
                await Console.Out.WriteAsync($"\nTime to get from Cache: {stopwatch.Elapsed.ToString()}\n");

                // 0.10 seconds

                return CachedList;
            }

            CachedList = await _demoRepository.ReturnFromDb();

            // new expiration
            var ExpirationTime = DateTime.Now.AddMinutes(5);
            await _cache.SetData("DemoClassCache", CachedList, ExpirationTime);

            stopwatch.Stop();
            await Console.Out.WriteAsync($"\nDB Count: {CachedList.Count()}\n");
            await Console.Out.WriteAsync($"\nTime to get from DB: {stopwatch.Elapsed.ToString()}\n");

            // 0.35 seconds

            return CachedList;
        }


        [HttpGet]
        [Route("redis")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<CacheDemoClass>> GetFromDistributedCachee()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            var List = await _cache.GetDemoList<CacheDemoClass>();

            stopwatch.Stop();

            await Console.Out.WriteAsync($"\nCache Count: {List.Count()}\n");
            await Console.Out.WriteAsync(stopwatch.Elapsed.ToString());

            return List;
        }
    }
}
