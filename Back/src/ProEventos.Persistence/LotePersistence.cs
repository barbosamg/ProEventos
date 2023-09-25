using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace ProEventos.Persistence
{
    public class LotePersistence : ILotePersistence
    {
        private readonly ProEventosContext _context;

        public LotePersistence(ProEventosContext context)
        {
            _context = context;
            //_context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

        }

        public async Task<Lote> GetLoteByEventoIdLoteIdAsync(int eventoId, int loteId)
        {
            IQueryable<Lote> query = _context.Lotes;
            query = query.AsNoTracking()
                            .Where(l => l.EventoId == eventoId && l.Id == loteId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Lote[]> GetLotesByEventoIdAsync(int eventoId)
        {
            IQueryable<Lote> query = _context.Lotes;
            query = query.AsNoTracking()
                            .Where(l => l.EventoId == eventoId);

            return await query.ToArrayAsync();
        }
    }
}