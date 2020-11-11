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
        [HttpGet]
        public async Task<IActionResult> Get(string id) {
            try {
                return Ok( await _pessoaRepository.GetByIdAsync(id));
            }
            catch( Exception ex) {
                return BadRequest(ex);
            }
        } 

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Core.Models.Pessoa pessoa)
        {
            try {
                var resposta = await _pessoaRepository.AddAsync(pessoa);
                return Ok(resposta);
            } 
            catch( Exception ex) {
                return BadRequest(ex);
            }
        } 

        [HttpDelete]
        public async Task<IActionResult> Delete(string id){
            try
            {
                await _pessoaRepository.DeleteAsync(id);
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex);
            }
        }
        
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Core.Models.Pessoa pessoa) 
        {
            try
            {
                return Ok( await _pessoaRepository.UpdateAsync(pessoa));
            }
            catch (Exception ex)
            {
              return BadRequest(ex);
            }
        }
    }
}
