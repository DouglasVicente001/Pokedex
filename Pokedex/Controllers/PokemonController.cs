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

            if (pokemon == null)
            {
                return NotFound("Este pokemon não existe na nossa pokedex!");
            }
                
            LeituraPokemonDto leituraPokemonDto = _mapper.Map<LeituraPokemonDto>(pokemon);
            return Ok(leituraPokemonDto);
        }

        [HttpPost]
        public IActionResult AdicionaPokemon([FromBody] CriacaoPokemonDto criacaoPokemonDto)
        {
            if (criacaoPokemonDto == null)
            {
                return BadRequest("Os dados do Pokémon não foram fornecidos.");
            }

            try
            {
                // Mapeia o DTO para o modelo de domínio
                Pokemon pokemon = _mapper.Map<Pokemon>(criacaoPokemonDto);

                // Adiciona o novo Pokémon ao contexto
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
            // Encontra o Pokémon existente pelo ID
            var pokemonExistente = _context.Pokemons.FirstOrDefault(p => p.Id == id);

            if (pokemonExistente == null)
            {
                return NotFound("Este pokemon não existe na nossa pokedex!"); // Retorna 404 se o Pokémon não for encontrado
            }

            // Atualiza as propriedades do Pokémon existente com os valores do DTO
            pokemonExistente.NomePokemom = atualizarPokemonDto.NomePokemom;
            pokemonExistente.Altura = atualizarPokemonDto.Altura;
            pokemonExistente.Tipo = atualizarPokemonDto.Tipo;
            pokemonExistente.Peso = atualizarPokemonDto.Peso;

            // Salva as alterações no banco de dados
            _context.SaveChanges();

            // Cria a resposta com o Pokémon atualizado e uma mensagem de sucesso
            var resposta = new PokemonResponse
            {
                Mensagem = "Seu Pokémon foi alterado com sucesso!",
                Pokemon = pokemonExistente
            };

            return Ok(resposta); // Retorna 200 OK com a resposta
        }


        [HttpDelete("{id}")]
        public IActionResult DeletaPokemon(int id)
        {
            var pokemon = _context.Pokemons.FirstOrDefault(p => p.Id == id);
            if (pokemon == null)
            {
                return NotFound("Este pokemon não existe na nossa pokedex!");
            }

            _context.Pokemons.Remove(pokemon);
            _context.SaveChanges();
            return Ok($"Pokemon {pokemon.NomePokemom} deletado com sucesso.");
        }
    }
}