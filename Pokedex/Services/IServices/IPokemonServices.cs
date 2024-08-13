using Pokedex.Models;

namespace Pokedex.Services.IServices
{
    public interface IPokemonServices
    {
        Task<IEnumerable<Pokemon>> BuscarTodosPokemons();
        Task<Pokemon> BuscarPokemonPorId(int id);
        Task AdicionaPokemon(Pokemon pokemon);
        Task AtualizaPokemon(Pokemon pokemon);
        Task DeletaPokemon(int id);
        Task DeletarTodosPokemons();
    }
}
