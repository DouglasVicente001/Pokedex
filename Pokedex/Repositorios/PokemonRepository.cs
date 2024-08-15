using Microsoft.EntityFrameworkCore;
using Pokedex.Data;
using Pokedex.Models;
using Pokedex.Repository.Interfaces;

namespace Pokedex.Repository
{
    public class PokemonRepository : IPokemonRepository
    {
        private readonly PokemonContext _context;
        public PokemonRepository(PokemonContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Pokemon>> BuscarTodosPokemonsAsync()
        {
            return await _context.Pokemons.ToListAsync();
        }
        public async Task<Pokemon> BuscarPokemonPorIdAsync(int id)
        {
            return await _context.Pokemons.FindAsync(id);
        }

        public async Task AdicionarPokemonAsync(Pokemon pokemon)
        {   
            await _context.Pokemons.AddAsync(pokemon);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarPokemonAsync(Pokemon pokemon)
        {
            _context.Pokemons.Update(pokemon);
            await _context.SaveChangesAsync();
        }

        public async Task DeletarPokemonAsync(Pokemon pokemon)
        {
            _context.Pokemons.Remove(pokemon);
            await _context.SaveChangesAsync();
        }
        public async Task DeletarTodosPokemonsAsync()
        {
            var pokemons = await _context.Pokemons.ToListAsync();
            _context.Pokemons.RemoveRange(pokemons);
            await _context.SaveChangesAsync();
        }
    }
}
