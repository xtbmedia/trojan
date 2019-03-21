using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using data_library.Models;
using Microsoft.EntityFrameworkCore;

namespace data_library.DataContexts.Transport
{
    public class AeroplaneContext : DbContext, IAeroplaneContext
    {
        public AeroplaneContext(DbContextOptions<AeroplaneContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Aeroplane> Aeroplanes { get; set; }

        public Task<int> SaveChangesAsync() =>
            base.SaveChangesAsync();

    }
}
