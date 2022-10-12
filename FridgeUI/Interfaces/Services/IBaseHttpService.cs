using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace FridgeUI.Interfaces.Services
{
    public interface IBaseHttpService<T>
    {
        Task<IEnumerable<T>> GetAll(string url);
        Task Create(T model, string url);
        Task Update(Guid id, T model, string url);
        Task Delete(Guid id, string url);
    }
}
