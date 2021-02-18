using AutoMapper;
using Core.Models;
using Core.ViewModel;

namespace Core.Mapper
{
    public class ModelToEntity: Profile
    {
        public ModelToEntity()
        {
            CreateMap<PessoaViewModel, Agente<Pessoa>>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.Id))
                .ReverseMap();
        }
    }
}