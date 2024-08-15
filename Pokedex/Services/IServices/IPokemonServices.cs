using Pokedex.Dtos;

namespace Pokedex.Services.IServices
{
    public interface IPokemonServices
    {
        Task<IEnumerable<LeituraPokemonDto>> BuscarTodosPokemons();
        Task<LeituraPokemonDto> BuscarPokemonPorId(int id);
        Task AdicionarPokemon(CriacaoPokemonDto dto);
        Task AtualizarPokemon(int id, AtualizarPokemonDto dto);
        Task DeletarPokemon(int id);
        Task DeletarTodosPokemons();

    }
}
