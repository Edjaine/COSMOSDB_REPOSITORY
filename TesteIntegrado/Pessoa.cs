using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Api;
using TesteIntegrado.Factory;
using TesteIntegrado.ViewModel;
using Xunit;

namespace TesteIntegrado
{
    public class Pessoa: IClassFixture<TestFixture<Api.Startup>>
    {        
        private readonly HttpClient _client;

        public IFactoryFake<PessoaViewModel> _factoryPessoa { get; }

        public Pessoa(TestFixture<Api.Startup> fixture)
        {
            _client = fixture.Client;            
            _factoryPessoa = new FactoryPessoaFake();
        }

        [Fact]
        public async Task ConsultaPessoa()
        {
            //Arrange
            var url = "/Pessoa";
            //Act
            var response = await _client.GetAsync(url);
            var value = await response.Content.ReadAsStringAsync();   
            var pessoas = Newtonsoft.Json.JsonConvert.DeserializeObject<PessoaViewModel[]>(value);
            
            //Assert
            response.EnsureSuccessStatusCode();
            Assert.True(pessoas.ToList().Count > 0);
            Assert.NotEmpty(pessoas.ToList().FirstOrDefault().nome);
        }

        [Fact]
        public async Task<PessoaViewModel> CriaPessoa () {
            //Arrage

            var request = new {
                Url = "/Pessoa",
                Body = _factoryPessoa.Constroi()
            };
            
            //Act
            var result = await _client.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));
            var value = await result.Content.ReadAsStringAsync();
            var pessoas = Newtonsoft.Json.JsonConvert.DeserializeObject<PessoaViewModel>(value);

            //Assert
            result.EnsureSuccessStatusCode();
            Assert.NotEmpty(pessoas.id);

            return pessoas;
        }
        [Fact]
        public async Task AtualizaPessoa() {

            //Arrage

            var pessoaPersistido = await this.CriaPessoa();    
            var request = new {
                Url = "/Pessoa",
                Body = _factoryPessoa.Constroi(pessoaPersistido.id)
            };


            request.Body.idade = pessoaPersistido.idade;
            var novoNomePessoa = request.Body.nome;
            
            //Act
            var result = await _client.PutAsync(request.Url, ContentHelper.GetStringContent(request.Body));
            var value = await result.Content.ReadAsStringAsync();
            Console.WriteLine(value);
            
            var pessoas = Newtonsoft.Json.JsonConvert.DeserializeObject<PessoaViewModel>(value);

            //Assert
            result.EnsureSuccessStatusCode();
            Assert.NotEmpty(pessoas.id);
            Assert.NotEqual(pessoas.nome, pessoaPersistido.nome);

        }
            [Fact]
            public async Task RemovePessoa()
            {
                //Arrage
                var pessoa =  await this.CriaPessoa();
                var request = $"/Pessoa/{pessoa.id}";

                Console.WriteLine($"Delete >>> {request}"); 

                //Act
                var result = await _client.DeleteAsync(request);
                //Assert
                result.EnsureSuccessStatusCode();
            }
    }    

}
