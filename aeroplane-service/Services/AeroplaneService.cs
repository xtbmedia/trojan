using data_library.DataContexts.Canine;
using data_library.DataContexts.Transport;
using data_library.Enumerations;
using data_library.Interfaces;
using data_library.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AeroplaneService.Services
{
    public class AeroplaneService : IDataService<Aeroplane>
    {
        private IAeroplaneContext context;
        private IAuditService<Aeroplane> auditService;

        public AeroplaneService(IAeroplaneContext context, IAuditService<Aeroplane> auditService)
        {
            this.context = context;
            this.auditService = auditService;
        }

        public Aeroplane Create()
        {
            return new Aeroplane
            {
                Id = Guid.NewGuid().ToString()
            };
        }

        public async Task DeleteAsync(string id)
        {
            var entity = await context.Aeroplanes.FindAsync(id);
            if (entity == null)
                return;

            context.Aeroplanes.Remove(entity);
            await auditService.CreateEntryAsync(Operation.Delete, entity, null);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Aeroplane>> GetAsync()
        {
            return await context.Aeroplanes.ToListAsync();
        }

        public Task<Aeroplane> GetAsync(string id)
        {
            return context.Aeroplanes.FindAsync(id);
        }

        public async Task<Aeroplane> WriteAsync(Aeroplane entity)
        {
            var existing = await GetAsync(entity.Id);
            if (existing == null)
            {
                await auditService.CreateEntryAsync(Operation.Create, null, entity);
                context.Aeroplanes.Add(entity);
            }
            else
            {
                await auditService.CreateEntryAsync(Operation.Update, existing, entity);
                context.Entry(existing).CurrentValues.SetValues(entity);
            }
            await context.SaveChangesAsync();
            return existing;
        }
    }
}
