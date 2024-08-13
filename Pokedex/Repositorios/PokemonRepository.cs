using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pokedex.Data;
using Pokedex.Dtos;
using Pokedex.Models;
using Pokedex.Repositorios.Interfaces;

namespace Pokedex.Repositorios
{
    public class PokemonRepositorio : IPokemonRepository
    {
        private readonly PokemonContext _context;
        private readonly IMapper _mapper;
        public PokemonRepositorio(PokemonContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<Pokemon>> BuscarTodosPokemons()
        {
            return await _context.Pokemons.ToListAsync();
        }
        public async Task DeletarTodosPokemons()
        {
            var pokemons = await _context.Pokemons.ToListAsync();
            _context.Pokemons.RemoveRange(pokemons);
            await _context.SaveChangesAsync();
        }
        public async Task<Pokemon> BuscarPokemonPorId(int id)
        {
            Pokemon pokemon = _context.Pokemons.FirstOrDefault(p => p.Id == id);

            LeituraPokemonDto leituraPokemonDto = _mapper.Map<LeituraPokemonDto>(pokemon);

            return await _context.Pokemons.FindAsync(id);
        }

        public async Task AdicionaPokemon(Pokemon pokemon)
        {
            await _context.Pokemons.AddAsync(pokemon);
        }

        public async Task AtualizaPokemon(Pokemon pokemon)
        {
            _context.Pokemons.Update(pokemon);
        }

        public async Task DeletaPokemon(int id)
        {
            var pokemon = await _context.Pokemons.FindAsync(id);
            if (pokemon != null)
            {
                _context.Pokemons.Remove(pokemon);
            }
        }
    }
}
