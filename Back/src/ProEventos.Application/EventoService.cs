using System;
using System.Threading.Tasks;
using ProEventos.Application.Interfaces;
using ProEventos.Domain;
using ProEventos.Persistence.Interfaces;

namespace ProEventos.Application
{
    public class EventoService : IEventoService
    {
        private readonly IGeralPersistence _geralPersistence;
        private readonly IEventoPersistence _eventoPersistence;
        public EventoService(IGeralPersistence geralPersistence, IEventoPersistence eventoPersistence)
        {
            _geralPersistence = geralPersistence;
            _eventoPersistence = eventoPersistence;
        }

        public async Task<Evento> AddEventos(Evento evento)
        {
            try
            {
                _geralPersistence.Add<Evento>(evento);

                if(await _geralPersistence.SaveChangesAsync())
                    return await _eventoPersistence.GetEventoByIdAsync(evento.Id, false);

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<Evento> UpdateEvento(int eventoId, Evento evento)
        {
            try
            {
                var eventoRetorno = await _eventoPersistence.GetEventoByIdAsync(eventoId, false);
                if (eventoRetorno == null ) return null;

                evento.Id = eventoRetorno.Id;

                _geralPersistence.Update<Evento>(evento);

                if(await _geralPersistence.SaveChangesAsync())
                    return await _eventoPersistence.GetEventoByIdAsync(evento.Id, false);

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<bool> DeleteEvento(int eventoId)
        {
            try
            {
                var eventoRetorno = await _eventoPersistence.GetEventoByIdAsync(eventoId, false);
                if (eventoRetorno == null ) throw new Exception("Evento não encontrado.");

                _geralPersistence.Delete<Evento>(eventoRetorno);
                return await _geralPersistence.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _eventoPersistence.GetAllEventosByTemaAsync(tema, includePalestrantes);
                if(eventos == null) return null;

                return eventos;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _eventoPersistence.GetAllEventosAsync(includePalestrantes);
                if(eventos == null) return null;

                return eventos;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _eventoPersistence.GetEventoByIdAsync(eventoId, includePalestrantes);
                if(eventos == null) return null;

                return eventos;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}