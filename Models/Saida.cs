using System.ComponentModel.DataAnnotations;

namespace MorningStar.Models
{
    public class Saida
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Data e hora de saída")]
        [DataType(DataType.DateTime)]
        public DateTime DataHoraSaida { get; set; }
        [Required]
        public string? Local { get; set; }
        [Required, Range(0, int.MaxValue)]
        public int Quantidade { get; set; }
        public int MercadoriaID { get; set; }
        public Mercadoria? Mercadoria { get; set; }

    }
}
