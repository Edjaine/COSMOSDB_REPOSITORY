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
            return _mapper.Map<T>(agente);            
        }

        public async Task<T> UpdateAsync(T entity)
        {
            var agente = _mapper.Map<Agente<T>>(entity);
            var retorno  =  await base.UpdateAsync(agente);
            return retorno.documento;
        }
        IList<T> IPessoaRepository<T>.GetAll()
        {
            var agente = base.GetAll(c => c.tipo == tipo);  
            
            var pessoas = new List<T>();
            agente.ToList().ForEach( a => {
                pessoas.Add( _mapper.Map<T>(a));
            });
            return pessoas;            
        }

        IList<T> IPessoaRepository<T>.Get(Expression<Func<Agente<T>, bool>> predicate)
        {
            throw new NotImplementedException();
        }        

        public async Task<T> GetById(Guid id)
        {
            var chave = $"{tipo}:{id.ToString()}";
            var agente = await base.GetByIdAsync(chave);
            return _mapper.Map<T>(agente.documento);
        }

        public async Task DeleteAsync(Guid id)
        {
            var chave = $"{tipo}:{id.ToString()}";
            await base.DeleteAsync(chave);
        }

        public T GetByIdAsync(Guid d)
        {
            throw new NotImplementedException();
        }
    }
}