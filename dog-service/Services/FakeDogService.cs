using data_library.Interfaces;
using data_library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogService.Services
{
    public class FakeDogService : IDataService<Dog>
    {
        private List<Dog> localData;

        public FakeDogService()
        {
            localData = new List<Dog>() {
                new Dog{ Id = "1", Breed = "Generic", Name = "Tiddles", Tagged = false },
                new Dog{ Id = "2", Breed = "Generic", Name = "Buster", Tagged = false },
                new Dog{ Id = "3", Breed = "Unknown", Name = "Fido", Tagged = true }
            };
        }

        public Dog Create()
        {
            var result = new Dog
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

        public Task<IEnumerable<Dog>> GetAsync()
            => Task.FromResult<IEnumerable<Dog>>(localData);

        public Task<Dog> GetAsync(string id)
            => Task.FromResult(localData.FirstOrDefault(o => o.Id == id));

        public async Task<Dog> WriteAsync(Dog entity)
        {
            var existing = await GetAsync(entity.Id);
            if (existing != null)
                localData.Remove(existing);

            localData.Add(entity);

            return entity;
        }
    }
}
