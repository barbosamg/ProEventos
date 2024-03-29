using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Interfaces;

namespace ProEventos.Persistence
{
    public class EventoPersistence : IEventoPersistence
    {
        private readonly ProEventosContext _context;
        
        public EventoPersistence(ProEventosContext context)
        {
            _context = context;
            //_context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

        }
        public async Task<Evento[]> GetAllEventosAsync(int userId, bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos.AsNoTracking()
                                                .Include(e => e.Lotes)
                                                .Include(e => e.RedesSociais);

            if(includePalestrantes)
            {
                query = query
                .Include(e => e.PalestrantesEventos)
                .ThenInclude(e => e.Palestrante);
            }

            query = query.Where(e => e.UserId == userId).OrderBy(e => e.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Evento[]> GetAllEventosByTemaAsync(int userId, string tema, bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos.AsNoTracking()
            .Include(e => e.Lotes)
            .Include(e => e.RedesSociais);

            if(includePalestrantes)
            {
                query = query
                .Include(e => e.PalestrantesEventos)
                .ThenInclude(e => e.Palestrante);
            }

            query = query
                        .OrderBy(e => e.Id)
                        .Where(e => e.Tema.ToLower().Contains(tema.ToLower()) && e.UserId == userId);

            return await query.ToArrayAsync();
        }


        public async Task<Evento> GetEventoByIdAsync(int userId, int eventoId, bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos.AsNoTracking()
            .Include(e => e.Lotes)
            .Include(e => e.RedesSociais);

            if(includePalestrantes)
            {
                query = query
                .Include(e => e.PalestrantesEventos)
                .ThenInclude(e => e.Palestrante);
            }

            query = query
                        .OrderBy(e => e.Id)
                        .Where(e => e.Id == eventoId && e.UserId == userId);

            return await query.FirstOrDefaultAsync();
        }
    }
}