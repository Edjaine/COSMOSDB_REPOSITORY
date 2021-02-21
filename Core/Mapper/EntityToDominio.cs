using System;
using AutoMapper;
using Core.Models;

namespace Core.Mapper
{
    public class EntityToDominio: Profile
    {
        public EntityToDominio()
        {
            CreateMap<Agente<Pessoa>, Pessoa>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => TrataChave(src.id)))
                .ForMember(dest => dest.Idade, opt => opt.MapFrom(src => src.documento.Idade))
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.documento.Nome));
        }
        private Guid TrataChave(string id){
            var chaveDividida = id.Split(':');
            var guid = new Guid();
            Guid.TryParse(chaveDividida[1], out guid);
            return guid;    
        }
    }
}