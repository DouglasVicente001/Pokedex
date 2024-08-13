using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pokedex.Data;
using Pokedex.Dtos;
using Pokedex.Models;

namespace Pokedex.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PokemonController : ControllerBase
    {
        private readonly PokemonContext _context;
        private readonly IMapper _mapper;

        public PokemonController(PokemonContext context, IMapper mapper)
        {
            _context = context; 
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult RetornaPokemon()   
        {
            var pokemons = _context.Pokemons.ToList();

            if (pokemons != null)
            {
                return Ok(pokemons);
            }
            
            return BadRequest("Lista de pokemons não encontrada");
        }

        [HttpGet("{id}")]
        public IActionResult RetornaPokemonPorId(int id)
        {
            Pokemon pokemon = _context.Pokemons.FirstOrDefault(p => p.Id == id);

            if (pokemon != null)
            {
                LeituraPokemonDto leituraPokemonDto = _mapper.Map<LeituraPokemonDto>(pokemon);
                return Ok(leituraPokemonDto);
            }
                return NotFound();
        }

        [HttpPost]
        public IActionResult AdicionaPokemon([FromBody] CriacaoPokemonDto criacaoPokemonDto)
        {
            if (criacaoPokemonDto == null)
            {
                return BadRequest("Os dados do Pokémon não foram fornecidos.");
            }

            // Verifica se o Pokémon já existe com base no Id
            var pokemonExiste = _context.Pokemons.Any(p => p.Id == criacaoPokemonDto.Id);

            if (pokemonExiste)
            {
                return BadRequest("O Pokémon com este ID já existe.");
            }

            try
            {
                // Mapeia o DTO para o modelo de domínio
                Pokemon pokemon = _mapper.Map<Pokemon>(criacaoPokemonDto);
                _context.Pokemons.Add(pokemon);
                _context.SaveChanges();

                // Retorna a ação criada com a localização do novo recurso
                return CreatedAtAction(nameof(RetornaPokemonPorId), new { id = pokemon.Id }, pokemon);
            }
            catch (Exception ex)
            {
                // Loga e retorna um erro genérico
                // _logger.LogError(ex, "Erro ao adicionar o Pokémon.");
                return StatusCode(500, "Erro interno do servidor.");
            }
        }

        [HttpPut("{id}")]
        public IActionResult AtualizaPokemon(int id, [FromBody] AtualizarPokemonDto atualizarPokemonDto)
        {   
            //Mapeia o atualizarPokemonDto para o Pokemon fazendo que salve nas entidades.
            Pokemon pokemon = _mapper.Map<Pokemon>(atualizarPokemonDto);

            pokemon = _context.Pokemons.FirstOrDefault(p => p.Id == id);
            if (pokemon == null)
            {
                return NotFound();
            }
            pokemon.NomePokemom = atualizarPokemonDto.NomePokemom;
            pokemon.Altura = atualizarPokemonDto.Altura;
            pokemon.Tipo = atualizarPokemonDto.Tipo;
            pokemon.Peso = atualizarPokemonDto.Peso;

            _context.SaveChanges();

            return Ok(pokemon);
        }

        [HttpDelete("{id}")]
        public IActionResult DeletaPokemon(int id)
        {
            var pokemon = _context.Pokemons.FirstOrDefault(p => p.Id == id);
            if (pokemon == null)
            {
                return NotFound();
            }

            _context.Pokemons.Remove(pokemon);
            _context.SaveChanges();
            return Ok($"Pok�mon {pokemon.NomePokemom} deletado com sucesso.");
        }
    }
}