using System.Collections.Generic;
using AutoMapper;
using Core.Models;
using Core.ViewModel;

namespace Core.Mapper
{
    public class DominioToViewModel: Profile
    {
        public DominioToViewModel()
        {
            CreateMap<Pessoa, PessoaViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => typeof(Pessoa).Name + ":" + src.Id));
        }
    }
}