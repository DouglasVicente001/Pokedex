using Pokedex.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pokedex.Repositorios.Interfaces
{
    public interface IPokemonRepository
    {
        Task<IEnumerable<Pokemon>> BuscarTodosPokemons();
        Task<Pokemon> BuscarPokemonPorId(int id);
        Task AdicionaPokemon(Pokemon pokemon);
        Task AtualizaPokemon(Pokemon pokemon);
        Task DeletaPokemon(int id);
        Task  DeletarTodosPokemons();
    }
}
