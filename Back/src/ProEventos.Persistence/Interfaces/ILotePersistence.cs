using System.Threading.Tasks;
using ProEventos.Domain;

namespace ProEventos.Persistence.Interfaces
{
    public interface ILotePersistence
    {
        Task<Lote[]> GetLotesByEventoIdAsync(int eventoId);
        Task<Lote> GetLoteByEventoIdLoteIdAsync(int eventoId, int loteId);
    }
}