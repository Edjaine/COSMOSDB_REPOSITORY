using System;
using Bogus;
using TesteIntegrado.ViewModel;

namespace TesteIntegrado.Factory
{
    public class FactoryPessoaFake : IFactoryFake<PessoaViewModel>
    {
        public PessoaViewModel Constroi(Guid id)
        {
            return new PessoaViewModel () {
                Id = id.ToString(),
                Nome = new Faker().Person.FullName,
                Idade = new Faker().Person.Random.Int(5, 90).ToString()                                
            };
        }

        public PessoaViewModel Constroi()
        {
            return new PessoaViewModel () {
                Nome = new Faker().Person.FullName,
                Idade = new Faker().Person.Random.Int(5, 90).ToString()                                
            };
        }
    }
}