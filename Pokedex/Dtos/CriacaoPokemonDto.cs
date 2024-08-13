using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Pokedex.Dtos
{
    public class CriacaoPokemonDto
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string NomePokemom { get; set; }
        public float Peso { get; set; }
        public float Altura { get; set; }
        public string Tipo { get; set; }
    }
}
