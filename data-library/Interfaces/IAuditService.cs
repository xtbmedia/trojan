using data_library.Enumerations;
using data_library.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace data_library.Interfaces
{
    public interface IAuditService<T>
    {
        Task<bool> CreateEntryAsync(Operation operation, T before, T after);
        Task<IEnumerable<AuditEvent>> GetAsync();
    }
}
