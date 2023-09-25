using System;
using System.Threading.Tasks;
using AutoMapper;
using ProEventos.Application.Dtos;
using ProEventos.Application.Interfaces;
using ProEventos.Domain;
using ProEventos.Persistence.Interfaces;

namespace ProEventos.Application
{
    public class EventoService : IEventoService
    {
        private readonly IGeralPersistence _geralPersistence;
        private readonly IEventoPersistence _eventoPersistence;
        private readonly IMapper _mapper;

        public EventoService(IGeralPersistence geralPersistence, IEventoPersistence eventoPersistence, IMapper mapper)
        {
            _geralPersistence = geralPersistence;
            _eventoPersistence = eventoPersistence;
            _mapper = mapper;
        }

        public async Task<LoteDto> AddEvento(LoteDto eventoDto)
        {
            try
            {
                var eventoModel = _mapper.Map<Evento>(eventoDto);

                _geralPersistence.Add<Evento>(eventoModel);

                if (await _geralPersistence.SaveChangesAsync())
                {
                    var eventoRetorno = await _eventoPersistence.GetEventoByIdAsync(eventoModel.Id, false);
                    return _mapper.Map<LoteDto>(eventoRetorno);
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<LoteDto> UpdateEvento(int eventoId, LoteDto eventoDto)
        {
            try
            {
                var eventoModel = await _eventoPersistence.GetEventoByIdAsync(eventoId, false);
                if (eventoModel == null) return null;

                eventoDto.Id = eventoModel.Id;

                _mapper.Map(eventoDto, eventoModel);
                _geralPersistence.Update<Evento>(eventoModel);

                if (await _geralPersistence.SaveChangesAsync())
                {
                    var eventoRetorno = await _eventoPersistence.GetEventoByIdAsync(eventoModel.Id, false);
                    return _mapper.Map<LoteDto>(eventoRetorno);
                }

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
                if (eventoRetorno == null) throw new Exception("Evento não encontrado.");

                _geralPersistence.Delete<Evento>(eventoRetorno);
                return await _geralPersistence.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<LoteDto[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _eventoPersistence.GetAllEventosByTemaAsync(tema, includePalestrantes);
                if (eventos == null) return null;

                return _mapper.Map<LoteDto[]>(eventos);
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<LoteDto[]> GetAllEventosAsync(bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _eventoPersistence.GetAllEventosAsync(includePalestrantes);
                if (eventos == null) return null;

                return _mapper.Map<LoteDto[]>(eventos);
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<LoteDto> GetEventoByIdAsync(int eventoId, bool includePalestrantes = false)
        {
            try
            {
                var evento = await _eventoPersistence.GetEventoByIdAsync(eventoId, includePalestrantes);
                if (evento == null) return null;

                return _mapper.Map<LoteDto>(evento);
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}