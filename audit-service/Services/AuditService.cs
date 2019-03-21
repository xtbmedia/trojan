using data_library.DataContexts.AuditTrail;
using data_library.Enumerations;
using data_library.Interfaces;
using data_library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace AuditService.Services
{
    public class AuditService<T> : IAuditService<T>
    {
        private AuditContext context;

        public AuditService(AuditContext context)
        {
            this.context = context;
        }

        public Task<bool> CreateEntryAsync(Operation operation, T before, T after)
        {
            var entry = new AuditEvent() {
                Id = Guid.NewGuid().ToString(),
                EventType = operation,
                Occured = DateTime.UtcNow
            };

            var propertyList = typeof(T).GetTypeInfo();

            return true;
        }

        public IEnumerable<AuditEvent> GetAsync()
        {
            throw new NotImplementedException();
        }
    }
}
