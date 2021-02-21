using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Models;

namespace Core.Interface
{
    public interface IPessoaRepository<T>: IRepository<Agente<T>> where T : Dominio
    {
        IList<T> Get(Expression<Func<Agente<T>, bool>> predicate);
        IList<T> GetAll();
        Task<T> AddAsync(T dominio);
        Task<T> UpdateAsync(T dominio);
        Task<T> GetById(string id);
       
    }
}