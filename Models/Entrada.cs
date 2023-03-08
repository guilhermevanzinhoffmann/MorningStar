using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace MorningStar.Models
{
    public class Entrada
    {
        public int Id { get; set; }
        [Display(Name = "Data e hora de entrada")]
        [DataType(DataType.DateTime)]
        [Required]
        public DateTime DataHoraEntrada { get; set; }
        [Required]
        public string? Local { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int Quantidade { get; set; }
        public int MercadoriaID { get; set; }
        public Mercadoria? Mercadoria { get; set; }
    }
}
