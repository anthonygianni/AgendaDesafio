using AgendaDesafio2.Services;
using AgendaDesafioAPI.Data;
using AgendaDesafioAPI.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AgendaDesafioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgendaController : ControllerBase
    {
       // private readonly DataContext _context;

        private IAgendaService _agendaService;
        private IMapper _mapper;

        public AgendaController(IAgendaService agendaService,IMapper mapper)
        {
            //_context = context;
            _agendaService= agendaService;
            _mapper= mapper;
        }

        // GET: api/<AgendaController>
        [HttpGet]

        public IActionResult GetAll()
        {
            var agendas=_agendaService.GetAll();

            return Ok(agendas);
            //return Ok(_context.Agendas);
        }

        // GET api/<AgendaController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var agendas=_agendaService.GetById(id);
            return Ok(agendas);
           // var agenda=_context.Agendas.FirstOrDefault(a => a.Id == id);    
           // if (agenda == null) return BadRequest("Não encontrado");

           // return Ok(agenda);
        }

        [HttpPost]
        public IActionResult Create(Agenda model)
        {
            _agendaService.Create(model);
            return Ok(new { message = "User created" });
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Agenda model)
        {
            _agendaService.Update(id, model);
            return Ok(new { message = "User updated" });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _agendaService.Delete(id);
            return Ok(new { message = "User deleted" });
        }
    }
}
