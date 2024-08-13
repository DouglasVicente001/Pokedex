using Microsoft.EntityFrameworkCore;
using Pokedex.Models;
using Pokedex.Services.IServices;
using System.Data;

namespace Pokedex.Services
{
    public class PokemonService : IPokemonServices
    {
        private readonly IPokemonServices _repository;
        public PokemonService(IPokemonServices repository)
        {
           _repository = repository;
        }
        public  Task AdicionaPokemon(Pokemon pokemon)
        {
            return  _repository.AdicionaPokemon(pokemon);
        }

        public Task AtualizaPokemon(Pokemon pokemon)
        {
            return _repository.AtualizaPokemon(pokemon);
        }

        public Task<Pokemon> BuscarPokemonPorId(int id)
        {
            var retorno = _repository.BuscarPokemonPorId(id);

            if (retorno == null)
            {
                throw new Exception("Pokemon não encontrado.");
            }
            return _repository.BuscarPokemonPorId(id);
        }

        public Task<IEnumerable<Pokemon>> BuscarTodosPokemons()
        {   
            return _repository.BuscarTodosPokemons();
        }

        public Task DeletaPokemon(int id)
        {
            return _repository.DeletaPokemon(id);
        }

        public Task DeletarTodosPokemons()
        {
            return _repository.DeletarTodosPokemons();
        }
    }
}
