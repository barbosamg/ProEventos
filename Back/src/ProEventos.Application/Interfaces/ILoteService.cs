using ProEventos.Application.Dtos;
using System.Threading.Tasks;

namespace ProEventos.Application.Interfaces
{
    public interface ILoteService
    {
        Task<LoteDto[]> SaveLotes(int eventoId, LoteDto[] lotes);
        Task<bool> DeleteLote(int eventoId, int loteId);
        Task<LoteDto[]> GetLotesByEventoIdAsync(int eventoId);
        Task<LoteDto> GetLoteByEventoIdLoteIdAsync(int eventoId, int loteId);
    }
}