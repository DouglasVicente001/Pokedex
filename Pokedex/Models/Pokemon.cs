using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Pokedex.Models
{
    public class Pokemon
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        public string NomePokemom { get; set; }
        public float Peso { get; set; }
        public float Altura { get; set; }
        public string Tipo { get; set; }
    }
}
