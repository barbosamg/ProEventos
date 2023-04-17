using Microsoft.AspNetCore.Mvc;
using ProEventos.API.Data;
using ProEventos.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class EventoController : ControllerBase
    {
        private readonly DataContext _context;

        public EventoController(DataContext context)
        {
                _context = context;
        }

        [HttpGet]
        public IEnumerable<Evento> Get()
        {
            return _context.Eventos;
        }

        [HttpGet("{id}")]
        public Evento Get(int id)
        {
            return _context.Eventos.Find(id);
        }

        [HttpPost]
        public string Post(Evento evento)
        {
            _context.Eventos.Add(evento);
            var salvou = _context.SaveChanges();
            return $"Salvou: {salvou}";
        }

        [HttpPut("{id}")]
        public string Put(int id)
        {
            return $"exemplo Put id {id}";
        }

        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            return $"exemplo Delete {id}";
        }
    }
}


