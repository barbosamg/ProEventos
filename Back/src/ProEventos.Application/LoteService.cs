using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ProEventos.Application.Dtos;
using ProEventos.Application.Interfaces;
using ProEventos.Domain;
using ProEventos.Persistence.Interfaces;

namespace ProEventos.Application
{
    public class LoteService : ILoteService
    {
        private readonly IGeralPersistence _geralPersistence;
        private readonly ILotePersistence _lotePersistence;
        private readonly IMapper _mapper;

        public LoteService(IGeralPersistence geralPersistence, ILotePersistence lotePersistence, IMapper mapper)
        {
            _geralPersistence = geralPersistence;
            _lotePersistence = lotePersistence;
            _mapper = mapper;
        }

        private async Task AddLote(int eventoId, LoteDto loteDto)
        {
            try
            {
                loteDto.EventoId = eventoId;
                var loteModel = _mapper.Map<Lote>(loteDto);

                _geralPersistence.Add<Lote>(loteModel);
                await _geralPersistence.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<LoteDto[]> SaveLotes(int eventoId, LoteDto[] lotes)
        {
            try
            {
                var lotesModel = await _lotePersistence.GetLotesByEventoIdAsync(eventoId);
                if (lotesModel == null) return null;

                foreach (var itemDto in lotes)
                {
                    if (itemDto.Id == 0)
                    {
                        await AddLote(eventoId, itemDto);
                    }
                    else
                    {
                        var loteModel = lotesModel.FirstOrDefault(l => l.Id == itemDto.Id);
                        itemDto.EventoId = eventoId;

                        _mapper.Map(itemDto, loteModel);
                        _geralPersistence.Update<Lote>(loteModel);

                        await _geralPersistence.SaveChangesAsync();
                    }
                }

                var loteRetorno = await _lotePersistence.GetLotesByEventoIdAsync(eventoId);
                return _mapper.Map<LoteDto[]>(loteRetorno);
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<bool> DeleteLote(int eventoId, int loteId)
        {
            try
            {
                var loteRetorno = await _lotePersistence.GetLoteByEventoIdLoteIdAsync(eventoId, loteId);
                if (loteRetorno == null) throw new Exception("Lote não encontrado.");

                _geralPersistence.Delete<Lote>(loteRetorno);
                return await _geralPersistence.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<LoteDto[]> GetLotesByEventoIdAsync(int eventoId)
        {
            try
            {
                var lotes = await _lotePersistence.GetLotesByEventoIdAsync(eventoId);
                if (lotes == null) return null;

                return _mapper.Map<LoteDto[]>(lotes);
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<LoteDto> GetLoteByEventoIdLoteIdAsync(int eventoId, int loteId)
        {
            try
            {
                var lote = await _lotePersistence.GetLoteByEventoIdLoteIdAsync(eventoId, loteId);
                if (lote == null) return null;

                return _mapper.Map<LoteDto>(lote);
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}