using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PessoaController : ControllerBase
    {
        private readonly ILogger<PessoaController> _logger;

        private readonly IPessoaRepository _pessoaRepository;

        public PessoaController(ILogger<PessoaController> logger, IPessoaRepository pessoaRepository)
        {
            _logger = logger;
            _pessoaRepository = pessoaRepository;
        }

        [HttpPost]
        public IActionResult Post(string nome, int idade){

            var pessoa = new Core.Models.Pessoa() { Nome = nome, Idade= idade};
            return Ok();
        } 
    }
}
