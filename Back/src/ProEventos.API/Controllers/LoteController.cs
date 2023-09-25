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
    public class LoteController : ControllerBase
    {
        private readonly ILoteService _loteService;

        public LoteController(ILoteService loteService)
        {
                _loteService = loteService;
        }

        [HttpGet("{eventoId}")]
        public async Task<IActionResult> Get(int eventoId)
        {
            try
            {
                var lotes = await _loteService.GetLotesByEventoIdAsync(eventoId);
                if(lotes == null) return NoContent();


                return Ok(lotes);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar lotes. Erro: {ex.Message}");
            }
        }

        [HttpPut("{eventoId}")]
        public async Task<IActionResult> SaveLotes(int eventoId, LoteDto[] lotes)
        {
            try
            {
                var salvouLotes = await _loteService.SaveLotes(eventoId, lotes);
                if (salvouLotes == null) return BadRequest("Erro ao tentar atualizar os lotes");

                return Ok(salvouLotes);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar atualizar lotes. Erro: {ex.Message}");
            }
        }

        [HttpDelete("{eventoId}/{loteId}")]
        public async Task<IActionResult> Delete(int eventoId, int loteId)
        {
            try
            {
                var lote = await _loteService.GetLoteByEventoIdLoteIdAsync(eventoId, loteId);
                if (lote == null) return NoContent();

                var excluiu = await _loteService.DeleteLote(lote.EventoId, lote.Id);
                if (!excluiu) return BadRequest("Erro ao tentar deletar um lote");

                return Ok(new { message = "Deletado", sucesso = true});
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar deletar lote. Erro: {ex.Message}");
            }
        }
    }
}


