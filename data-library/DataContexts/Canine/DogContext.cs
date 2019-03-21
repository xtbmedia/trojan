using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using data_library.Models;
using System.Threading.Tasks;

namespace data_library.DataContexts.Canine
{
    public class DogContext : DbContext, IDogContext
    {
        public DogContext(DbContextOptions<DogContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Dog> Dogs { get; set; }

        public Task<int> SaveChangesAsync()
            => base.SaveChangesAsync();
    }
}
