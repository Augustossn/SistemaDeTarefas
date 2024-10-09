using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaDeTarefas.Models;
using SistemaDeTarefas.Repositorios.Interfaces;

namespace SistemaDeTarefas.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TarefaController : ControllerBase
    {
        private readonly ITarefaService _tarefaService;

        public TarefaController(ITarefaService tarefaService)
        {
            _tarefaService = tarefaService;
        }

        [HttpGet]
        public async Task<ActionResult<List<TarefaModel>>> GetAll()
        {
            List<TarefaModel> tarefa = await _tarefaService.GetAll();
            return Ok(tarefa);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TarefaModel>> GetById(int id)
        {
            TarefaModel tarefa = await _tarefaService.GetById(id);
            if (tarefa == null)
            {
                return NotFound();
            }

            return Ok(tarefa);
        }

        [HttpPost]
        public async Task<ActionResult<TarefaModel>> Post([FromBody] TarefaModel tarefaModel)
        {
            TarefaModel tarefa = await _tarefaService.Post(tarefaModel);
            return Ok(tarefa);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TarefaModel>> Put(TarefaModel tarefaModel, int id)
        {
            tarefaModel.Id = id;
            TarefaModel tarefa = await _tarefaService.Put(tarefaModel, id);
            return Ok(tarefa);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TarefaModel>> Delete(int id)
        {
            var user = await _tarefaService.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            bool deleted = await _tarefaService.Delete(id);

            if (deleted)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
