using ProEventos.Application.Dtos;
using System.Threading.Tasks;

namespace ProEventos.Application.Interfaces
{
    public interface IEventoService
    {
        Task<EventoDto> AddEvento(EventoDto evento);
        Task<EventoDto> UpdateEvento(int eventoId, EventoDto evento);
        Task<bool> DeleteEvento(int eventoId);
        Task<EventoDto[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false);
        Task<EventoDto[]> GetAllEventosAsync(bool includePalestrantes = false);
        Task<EventoDto> GetEventoByIdAsync(int eventoId, bool includePalestrantes = false);
    }
}