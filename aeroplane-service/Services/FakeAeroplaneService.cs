using data_library.Interfaces;
using data_library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AeroplaneService.Services
{
    public class FakeAeroplaneService : IDataService<Aeroplane>
    {
        private List<Aeroplane> localData;

        public FakeAeroplaneService()
        {
            localData = new List<Aeroplane>() {
                new Aeroplane{ Id = "A", Manufacturer = "Generic", Model = "A320", InService = true },
                new Aeroplane{ Id = "B", Manufacturer = "Generic", Model = "ATR-72", InService = true },
                new Aeroplane{ Id = "C", Manufacturer = "Unknown", Model = "777-800", InService = true }
            };
        }

        public Aeroplane Create()
        {
            var result = new Aeroplane
            {
                Id = DateTime.UtcNow.Ticks.ToString()
            };
            localData.Add(result);
            return result;
        }

        public Task DeleteAsync(string id)
        {
            var entity = localData.FirstOrDefault(o => o.Id == id);
            if (entity == null)
                return Task.FromResult(false);

            localData.Remove(entity);
            return Task.FromResult(true);
        }

        public Task<IEnumerable<Aeroplane>> GetAsync()
            => Task.FromResult<IEnumerable<Aeroplane>>(localData);

        public Task<Aeroplane> GetAsync(string id)
            => Task.FromResult(localData.FirstOrDefault(o => o.Id == id));

        public async Task<Aeroplane> WriteAsync(Aeroplane entity)
        {
            var existing = await GetAsync(entity.Id);
            if (existing != null)
                localData.Remove(existing);

            localData.Add(entity);

            return entity;
        }
    }
}
