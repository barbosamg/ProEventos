using Microsoft.AspNetCore.Mvc;
using ProEventos.Domain;
using ProEventos.Application.Interfaces;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using ProEventos.Application.Dtos;

namespace ProEventos.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class EventoController : ControllerBase
    {
        private readonly IEventoService _eventoService;

        public EventoController(IEventoService eventoService)
        {
                _eventoService = eventoService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var eventos = await _eventoService.GetAllEventosAsync(true);
                if(eventos == null) return NoContent();


                return Ok(eventos);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar eventos. Erro: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        // public async Task<ActionResult<Evento>> GetById(int id)
        {
            try
            {
                var evento = await _eventoService.GetEventoByIdAsync(id, true);
                if(evento == null) return NoContent();

                return Ok(evento);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar eventos. Erro: {ex.Message}");
            }
        }

        [HttpGet("{tema}/tema")]
        public async Task<IActionResult> GetByTema(string tema)
        // public async Task<ActionResult<Evento>> GetById(int id)
        {
            try
            {
                var evento = await _eventoService.GetAllEventosByTemaAsync(tema, true);
                if(evento == null) return NoContent();

                return Ok(evento);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar eventos. Erro: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(EventoDto evento)
        {
            try
            {
                var eventoSalvo = await _eventoService.AddEvento(evento);
                if(eventoSalvo == null) return BadRequest("Erro ao tentar criar um evento");

                return Ok(eventoSalvo);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar criar evento. Erro: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, EventoDto evento)
        {
            try
            {
                var salvou = await _eventoService.UpdateEvento(id, evento);
                if (salvou == null) return BadRequest("Erro ao tentar atualizar um evento");

                return Ok(evento);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar atualizar evento. Erro: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var excluiu = await _eventoService.DeleteEvento(id);
                if (!excluiu) return BadRequest("Erro ao tentar deletar um evento");

                return Ok(new { message = "Deletado", sucesso = true});
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar deletar evento. Erro: {ex.Message}");
            }
        }
    }
}


