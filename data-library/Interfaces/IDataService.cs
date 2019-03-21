using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace data_library.Interfaces
{
    public interface IDataService<T>
    {
        T Create();
        Task<IEnumerable<T>> GetAsync();
        Task<T> GetAsync(string id);
        Task<T> WriteAsync(T entity);
        Task DeleteAsync(string id);
    }
}
