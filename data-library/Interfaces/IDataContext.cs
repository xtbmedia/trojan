using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace data_library.Interfaces
{
    public interface IDataContext
    {
        EntityEntry Entry(object entity);
        Task<int> SaveChangesAsync();
    }
}
