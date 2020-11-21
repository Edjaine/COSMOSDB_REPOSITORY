using System;
using Bogus;
using TesteIntegrado.ViewModel;

namespace TesteIntegrado.Factory
{
    public class FactoryPessoaFake : IFactoryFake<PessoaViewModel>
    {
        public PessoaViewModel Constroi(string id)
        {
            return new PessoaViewModel () {
                id = id.ToString(),
                nome = new Faker().Person.FullName,
                idade = new Faker().Person.Random.Int(5, 90).ToString()                                
            };
        }

        public PessoaViewModel Constroi()
        {
            return new PessoaViewModel () {
                nome = new Faker().Person.FullName,
                idade = new Faker().Person.Random.Int(5, 90).ToString()                                
            };
        }
    }
}