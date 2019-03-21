using data_library.DataContexts.Canine;
using data_library.Enumerations;
using data_library.Interfaces;
using data_library.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogService.Services
{
    public class DogService : IDataService<Dog>
    {
        private IDogContext context;
        private IAuditService<Dog> auditService;

        public DogService(IDogContext context, IAuditService<Dog> auditService)
        {
            this.context = context;
            this.auditService = auditService;
        }

        public Dog Create()
        {
            return new Dog
            {
                Id = Guid.NewGuid().ToString()
            };
        }

        public async Task DeleteAsync(string id)
        {
            var entity = await context.Dogs.FindAsync(id);
            if (entity == null)
                return;

            context.Dogs.Remove(entity);
            await auditService.CreateEntryAsync(Operation.Delete, entity, null);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Dog>> GetAsync()
        {
            return await context.Dogs.ToListAsync();
        }

        public Task<Dog> GetAsync(string id)
        {
            return context.Dogs.FindAsync(id);
        }

        public async Task<Dog> WriteAsync(Dog entity)
        {
            var existing = await GetAsync(entity.Id);
            if (existing == null)
            {
                await auditService.CreateEntryAsync(Operation.Create, null, entity);
                context.Dogs.Add(entity);
            }
            else
            {
                await auditService.CreateEntryAsync(Operation.Update, existing, entity);
                context.Dogs.Attach(entity);
            }
            await context.SaveChangesAsync();
            return entity;
        }
    }
}
