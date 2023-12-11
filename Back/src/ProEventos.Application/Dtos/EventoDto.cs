using ProEventos.Domain;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProEventos.Application.Dtos
{
    public class EventoDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public UserDto UserDto { get; set; }
        public string Local { get; set; } = "";
        public string DataEvento { get; set; } = "";

        [Display(Name = "tema")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "O campo {0} deve ter entre 3 e 50 caracteres")]
        //[MinLength(3, ErrorMessage = "O campo {0} deve ter no mínimo 3 caracteres")] [MaxLength(50, ErrorMessage = "O campo {0} deve ter no máximo 50 caracteres")]
        public string Tema { get; set; } = "";

        [Display(Name = "Quantidade de pessoas")]
        [Range(1, 120000, ErrorMessage = "{0} deve estar entre 1 e 120000")]
        public int QuantidadePessoas { get; set; }

        [RegularExpression(@".*\.(.gif|jpe?g|bmp|png)$", ErrorMessage = "Não é uma imagem válida (gif, jpg, jpeg, bmp ou png)")]
        public string ImagemURL { get; set; } = "";

        [Display(Name = "telefone")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Phone(ErrorMessage = "O campo {0} está no formado inválido")]
        public string Telefone { get; set; } = "";

        [Display(Name = "e-mail")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [EmailAddress(ErrorMessage = "Digite um {0} válido")]
        public string Email { get; set; } = "";
        public IEnumerable<Lote> Lotes { get; set; }
        public IEnumerable<RedeSocial> RedesSociais { get; set; }
        public IEnumerable<PalestranteEvento> PalestrantesEventos { get; set; }
    }
}
