using Pokedex.Models;

namespace Pokedex.Dtos
{
    public class PokemonResponse
    {
        public string Mensagem { get; set; }
        public Pokemon Pokemon { get; set; }
    }
}
