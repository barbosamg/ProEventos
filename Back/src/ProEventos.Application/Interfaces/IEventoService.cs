using ProEventos.Application.Dtos;
using System.Threading.Tasks;

namespace ProEventos.Application.Interfaces
{
    public interface IEventoService
    {
        Task<LoteDto> AddEvento(LoteDto evento);
        Task<LoteDto> UpdateEvento(int eventoId, LoteDto evento);
        Task<bool> DeleteEvento(int eventoId);
        Task<LoteDto[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false);
        Task<LoteDto[]> GetAllEventosAsync(bool includePalestrantes = false);
        Task<LoteDto> GetEventoByIdAsync(int eventoId, bool includePalestrantes = false);
    }
}