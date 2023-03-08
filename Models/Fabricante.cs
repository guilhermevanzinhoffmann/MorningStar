using System.ComponentModel.DataAnnotations;

namespace MorningStar.Models
{
    public class Fabricante
    {
        public int Id { get; set; }
        [Required]
        public string? Nome { get; set; }

        public IEnumerable<Mercadoria>? Mercadorias { get; set; }
    }
}
