using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using data_library.Interfaces;
using data_library.Models;
using DogService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace dog_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DogsController : ControllerBase
    {
        private IDataService<Dog> dogService;

        public DogsController(IDataService<Dog> dogService)
        {
            this.dogService = dogService;
        }

        // GET: api/Dogs
        [HttpGet]
        public Task<IEnumerable<Dog>> Get()
        {
            return dogService.GetAsync();
        }

        // GET: api/Dogs/5
        [HttpGet("{id}", Name = "Get")]
        public Task<Dog> Get(string id)
        {
            return dogService.GetAsync(id);
        }

        // POST: api/Dogs
        [HttpPost]
        public async Task<Dog> Post([FromBody] Dog dog)
        {
            var posted = await dogService.WriteAsync(dog);
            return posted;
        }

        // PUT: api/Dogs/5
        [HttpPut("{id}")]
        public async Task<Dog> Put(Dog dog)
        {
            var posted = await dogService.WriteAsync(dog);
            return posted;
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public Task Delete(string id)
        {
            return dogService.DeleteAsync(id);
        }
    }
}
