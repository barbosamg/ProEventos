using Microsoft.AspNetCore.Mvc;
using ProEventos.API.Models;

namespace ProEventos.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventoController : ControllerBase
{
    private IEnumerable<Evento> _evento = new Evento[] {
            new Evento(){
                EventoId = 1,
                Tema = "Angular e .NET",
                Local = "São Paulo",
                Lote = "1 lote",
                QuantidadePessoas = 300,
                DataEvento = "29/03/1996"
            },
            new Evento(){
                EventoId = 2,
                Tema = "Angular e .NET 2",
                Local = "São Paulo",
                Lote = "2 lote",
                QuantidadePessoas = 300,
                DataEvento = "30/03/2023"
            }
        };
    public EventoController()
    {
    }

    [HttpGet]
    public IEnumerable<Evento> Get()
    {
        return _evento;
    }

    [HttpGet("{id}")]
    public IEnumerable<Evento> Get(int id)
    {
        return _evento.Where(e => e.EventoId == id);
    }

    [HttpPost]
    public string Post()
    {
        return "exemplo post";
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
