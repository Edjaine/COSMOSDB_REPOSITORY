using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Core.Interface;
using Core.Models;
using Microsoft.Azure.Documents;
using TodoService.Infrastructure.Data;

namespace Infra.Data
{
    public partial class PessoaRepository<T> : AgenteRepository<T>, IPessoaRepository<T> where T: Dominio
    {
        private readonly IMapper _mapper;
        private string tipo = "Pessoa";

        public PessoaRepository(ICosmosDbClientFactory factory, IMapper mapper) : base(factory)
        {
            _mapper = mapper;
        }

        public async Task<T> AddAsync(T entity)
        {
            var agente = _mapper.Map<Agente<T>>(entity);
            var retorno  =  await base.AddAsync(agente);
            return retorno.documento;            
        }

        public async Task<T> UpdateAsync(T entity)
        {
            var agente = _mapper.Map<Agente<T>>(entity);
            var retorno  =  await base.UpdateAsync(agente);
            return retorno.documento;
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