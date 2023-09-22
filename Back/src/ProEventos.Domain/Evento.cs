using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProEventos.Domain
{
    //[Table("Evento")]
    public class Evento
    {
        //[Key]
        public int Id { get; set; }
        //[Column("Local")]
        public string Local { get; set; } = "";
        public DateTime? DataEvento { get; set; }
        //[Required]
        //[MaxLength(50)]
        //[MinLength(3)]
        public string Tema { get; set; } = "";

        //[NotMapped]
        //public int ContagemDias { get; set; }

        public int QuantidadePessoas { get; set; }
        public string ImagemURL { get; set; } = "";
        public string Telefone { get; set; }
        public string Email { get; set; }
        public IEnumerable<Lote> Lotes { get; set; }
        public IEnumerable<RedeSocial> RedesSociais { get; set; }
        public IEnumerable<PalestranteEvento> PalestrantesEventos { get; set; }
    }
}