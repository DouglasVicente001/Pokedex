using System.ComponentModel.DataAnnotations;

namespace Pokedex.Dtos
{
    public class AtualizarPokemonDto
    {
        public string NomePokemom { get; set; }
        public float Peso { get; set; }
        public float Altura { get; set; }
        public string Tipo { get; set; }
    }
}
