using AutoMapper;

namespace Core.Mapper
{
    public class MappingsConfig
    {
        public static MapperConfiguration Register() {
            return new MapperConfiguration(m => 
                { 
                    m.AddProfile( new DominioToEntity());
                    m.AddProfile( new EntityToDominio());
                    m.AddProfile( new DominioToViewModel());
                });
        }
    }
}