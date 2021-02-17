using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Models;

namespace Core.Interface
{
    public interface IPessoaRepository<T>: IRepository<Agente<T>>
    {
        IList<T> GetAll(Expression<Func<Agente<T>, bool>> predicate);
        IList<T> GetAll(string query);
        Task<T> AddAsync(T dominio);
        Task<T> UpdateAsync(T dominio);
        Task DeleteAsync(string id);
       
    }
}