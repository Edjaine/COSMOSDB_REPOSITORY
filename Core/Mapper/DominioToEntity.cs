using System;
using AutoMapper;
using Core.Models;
using Core.ViewModel;

namespace Core.Mapper
{
    public class DominioToEntity: Profile
    {
        public DominioToEntity()
        {
            CreateMap<Pessoa, Pessoa>();

            CreateMap<Pessoa, Agente<Pessoa>>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.tipo, opt => opt.MapFrom(src => typeof(Pessoa).Name.ToString()))
                .ForMember(dest => dest.documento, opt => opt.MapFrom(src => src))                
                .ReverseMap();
        }
    }
}