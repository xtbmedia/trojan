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
    public class FakeAuditService<T> : IAuditService<T>
    {
        private List<AuditEvent> localData;

        public FakeAuditService()
        {
            localData = new List<AuditEvent>();
        }

        public Task<bool> CreateEntryAsync(Operation operation, T before, T after)
        {
            var entry = new AuditEvent()
            {
                Id = Guid.NewGuid().ToString(),
                EventType = operation,
                Occured = DateTime.UtcNow
            };

            var propertyList = typeof(T).GetTypeInfo().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var property in propertyList)
            {
                var auditEventItem = new AuditEventItem()
                {
                    TableName = typeof(T).Name,
                    FieldName = property.Name
                };

                switch (operation)
                {
                    case Operation.Create:
                        auditEventItem.AfterChangeValue = property.GetValue(after)?.ToString();
                        break;
                    case Operation.Delete:
                        auditEventItem.BeforeChangeValue = property.GetValue(before)?.ToString();
                        break;
                    case Operation.Update:
                        auditEventItem.AfterChangeValue = property.GetValue(after)?.ToString();
                        auditEventItem.BeforeChangeValue = property.GetValue(before)?.ToString();
                        break;
                }

                if (auditEventItem.BeforeChangeValue != auditEventItem.AfterChangeValue)
                    entry.Changes.Add(auditEventItem);
            }

            if (entry.Changes.Any())
            {
                localData.Add(entry);
            }

            return Task.FromResult(true);
        }

        public Task<IEnumerable<AuditEvent>> GetAsync()
            => Task.FromResult(localData.AsEnumerable());
    }
}
