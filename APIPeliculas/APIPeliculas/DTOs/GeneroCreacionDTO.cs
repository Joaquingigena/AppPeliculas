using System.ComponentModel.DataAnnotations;

namespace APIPeliculas.DTOs
{
    public class GeneroCreacionDTO
    {
        [MaxLength(50)]
        public string Nombre { get; set; } = null!;
    }
}
