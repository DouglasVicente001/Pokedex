using AutoMapper;
using Pokedex.Dtos;
using Pokedex.Models;
using Pokedex.Repository.Interfaces;
using Pokedex.Services.IServices;

namespace Pokedex.Services
{
    public class PokemonService : IPokemonServices
    {
        private readonly IPokemonRepository _repository;
        private readonly IMapper _mapper;
        public PokemonService(IPokemonRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LeituraPokemonDto>> BuscarTodosPokemons()
        {
            var pokemons = await _repository.BuscarTodosPokemonsAsync();
            return _mapper.Map<IEnumerable<LeituraPokemonDto>>(pokemons);
        }
        public async Task<LeituraPokemonDto> BuscarPokemonPorId(int id)
        {
            var pokemon = await _repository.BuscarPokemonPorIdAsync(id);

            if (pokemon == null)
            {
                throw new Exception("Pokémon não encontrado.");
            }

            return _mapper.Map<LeituraPokemonDto>(pokemon); 
        }

        public async Task AdicionarPokemon(CriacaoPokemonDto dto)
        {
            //var pokemon = _mapper.Map<Pokemon>(dto);
            var pokemon = new Pokemon
            {
                Altura = dto.Altura,
                NomePokemom = dto.NomePokemom,
                Peso = dto.Peso,
                Tipo = dto.Tipo
            };
            await _repository.AdicionarPokemonAsync(pokemon);
  
        }

        public async Task AtualizarPokemon(int id,AtualizarPokemonDto dto)
        {
            var pokemonExistente = await _repository.BuscarPokemonPorIdAsync(id);

            if (pokemonExistente == null)
            {
                throw new Exception("Pokémon não encontrado para atualizar.");
            };

            _mapper.Map(dto, pokemonExistente);
            _repository.AtualizarPokemonAsync(pokemonExistente);
            await _repository.AtualizarPokemonAsync(pokemonExistente);
        }
        
        public async Task DeletarPokemon(int id)
        {
            var pokemon = await _repository.BuscarPokemonPorIdAsync(id);
            if (pokemon == null)
            {
                throw new Exception("Pokémon não encontrado para atualizar.");
            };

            await _repository.DeletarPokemonAsync(pokemon);
        }
        public async Task DeletarTodosPokemons()
        {
            await _repository.DeletarTodosPokemonsAsync();
        }
    }
}
