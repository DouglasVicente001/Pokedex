using Pokedex.Models;

namespace Pokedex.Repository.Interfaces
{
    public interface IPokemonRepository
    {
        Task<IEnumerable<Pokemon>> BuscarTodosPokemonsAsync();
        Task<Pokemon> BuscarPokemonPorIdAsync(int id);
        Task AdicionarPokemonAsync(Pokemon pokemon);
        Task AtualizarPokemonAsync(Pokemon pokemon);
        Task DeletarPokemonAsync(Pokemon pokemon);
        Task DeletarTodosPokemonsAsync();
    }
}
