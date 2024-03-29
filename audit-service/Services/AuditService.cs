﻿using data_library.DataContexts.AuditTrail;
using data_library.Enumerations;
using data_library.Interfaces;
using data_library.Models;
using Microsoft.EntityFrameworkCore;
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

        public async Task<bool> CreateEntryAsync(Operation operation, T before, T after)
        {
            int updatedCount = 0;

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

            if(entry.Changes.Any())
            {
                context.AuditEvents.Add(entry);
                updatedCount = await context.SaveChangesAsync();
            }

            return updatedCount > 0;
        }

        public async Task<IEnumerable<AuditEvent>> GetAsync()
        {
            return await context.AuditEvents.Include(t => t.Changes).ToListAsync();
        }
    }
}
