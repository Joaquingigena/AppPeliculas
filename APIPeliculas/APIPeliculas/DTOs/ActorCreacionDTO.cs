using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace APIPeliculas.DTOs
{
    public class ActorCreacionDTO
    {
        [Required]
        [StringLength(150)]
        public required string Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }
        [Unicode(false)]
        public IFormFile? Foto { get; set; }
    }
}
