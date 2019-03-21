using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using data_library.Interfaces;
using data_library.Models;
using AeroplaneService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AeroplaneService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AeroplanesController : ControllerBase
    {
        private IDataService<Aeroplane> aeroplaneService;

        public AeroplanesController(IDataService<Aeroplane> aeroplaneService)
        {
            this.aeroplaneService = aeroplaneService;
        }

        // GET: api/Aeroplanes
        [HttpGet]
        public Task<IEnumerable<Aeroplane>> Get()
        {
            return aeroplaneService.GetAsync();
        }

        // GET: api/Aeroplanes/5
        [HttpGet("{id}", Name = "Get")]
        public Task<Aeroplane> Get(string id)
        {
            return aeroplaneService.GetAsync(id);
        }

        // POST: api/Aeroplanes
        [HttpPost]
        public async Task<Aeroplane> Post([FromBody] Aeroplane aeroplane)
        {
            var posted = await aeroplaneService.WriteAsync(aeroplane);
            return posted;
        }

        // PUT: api/Aeroplanes/5
        [HttpPut("{id}")]
        public async Task<Aeroplane> Put(Aeroplane aeroplane)
        {
            var posted = await aeroplaneService.WriteAsync(aeroplane);
            return posted;
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public Task Delete(string id)
        {
            return aeroplaneService.DeleteAsync(id);
        }
    }
}
