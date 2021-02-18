using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Interface;
using Core.Models;
using Microsoft.Azure.Documents;
using TodoService.Infrastructure.Data;

namespace Infra.Data
{
    public partial class PessoaRepository<T> : AgenteRepository<T>, IPessoaRepository<T> where T: Dominio
    {
        private string tipo = "Pessoa";
        public PessoaRepository(ICosmosDbClientFactory factory) : base(factory)
        {
        
        }

        public async Task<T> AddAsync(T entity)
        {
            var agente = new Agente<T>() {
                id = Guid.NewGuid().ToString(),
                tipo = tipo,
                documento = entity
            };
            var agentes  =  await base.AddAsync(agente);
            return agentes.documento;
            
        }

        public async Task<T> UpdateAsync(T entity)
        {
                var agente = new Agente<T>() {
                id = Guid.NewGuid().ToString(),
                tipo = tipo,
                documento = entity
            };
            var agentes  =  await base.UpdateAsync(agente);
            return agentes.documento;
        }
        

        IList<T> IPessoaRepository<T>.GetAll(Expression<Func<Agente<T>, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        IList<T> IPessoaRepository<T>.GetAll(string query)
        {
            var agente = base.GetAll(c => c.tipo == tipo);  
            return agente.Select(a => a.documento).ToList<T>();            
        }
    }
}