using data_library.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace data_library.DataContexts.AuditTrail
{
    public class AuditContext : DbContext, IAuditContext
    {
        public AuditContext(DbContextOptions<AuditContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<AuditEvent> AuditEvents { get; set; }

        public Task<int> SaveChangesAsync()
            => base.SaveChangesAsync();
    }
}
