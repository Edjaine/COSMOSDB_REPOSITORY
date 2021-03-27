using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Interface;
using Core.Models;
using Core.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PessoaController : ControllerBase
    {
        private readonly ILogger<PessoaController> _logger;
        private readonly IPessoaRepository<Pessoa> _pessoaRepository;
        private readonly IMapper _mapper;

        public PessoaController(ILogger<PessoaController> logger, 
                                IPessoaRepository<Pessoa> pessoaRepository,
                                IMapper mapper)
        {
            _logger = logger;
            _pessoaRepository = pessoaRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public ActionResult Get() {
            try 
            {         
                var pessoas = _pessoaRepository.GetAll();
                return Ok(_mapper.Map<List<PessoaViewModel>>(pessoas));
            }
            catch( Exception ex) {
                return BadRequest(ex);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id) {
            try 
            {
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
                var pessoaPersistida = await _pessoaRepository.AddAsync(pessoa);
                return Ok(_mapper.Map<PessoaViewModel>(pessoaPersistida));
            } 
            catch( Exception ex) {
                return BadRequest(ex);
            }
        } 

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id){
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
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] Core.Models.Pessoa pessoa, Guid id) 
        {
            try
            {  
                pessoa.Id = id;          
                var pessoaPesistida  = await _pessoaRepository.UpdateAsync(pessoa);                
                return Ok( _mapper.Map<PessoaViewModel>(pessoaPesistida));
            }
            catch (Exception ex)
            {
              return BadRequest(ex);
            }
        }
    }
}
