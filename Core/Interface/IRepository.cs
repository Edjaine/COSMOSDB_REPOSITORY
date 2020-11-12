using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Models;

namespace Core.Interface
{
    public interface IRepository<T> where T : Entity
    {
        Task<IList<T>> GetAll(string query);
        Task<T> GetByIdAsync(string id);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(string id);
    }
}