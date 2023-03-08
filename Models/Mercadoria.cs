using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MorningStar.Models
{
    public class Mercadoria
    {
        public int Id { get; set; }
        [Required]
        public string? Nome { get; set; }
        [Required]
        [Display(Name = "Registro")]
        public int NumeroRegistro { get; set; }
        public int FabricanteID { get; set; }
        public Fabricante? Fabricante { get; set;}
        [Required]
        public string? Tipo { get; set; }
        [Required, Range(0, int.MaxValue)]
        public int Quantidade { get; set; }
    }
}
