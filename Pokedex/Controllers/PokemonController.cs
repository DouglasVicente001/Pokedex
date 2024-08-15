using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Pokedex.Data;
using Pokedex.Dtos;
using Pokedex.Models;
using Pokedex.Services.IServices;

namespace Pokedex.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PokemonController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPokemonServices _service;
        public PokemonController(PokemonContext context, IMapper mapper, IPokemonServices services)
        {
            _mapper = mapper;
            _service = services;
        }

        [HttpGet]
        public async Task<IActionResult> BuscarTodosPokemons()
        {
            var pokemons = await _service.BuscarTodosPokemons();
            return Ok(pokemons);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPokemonPorId(int id)
        {
            try
            {
                var pokemon = await _service.BuscarPokemonPorId(id);
                return Ok(pokemon);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AdicionaPokemon([FromBody] CriacaoPokemonDto criacaoPokemonDto)
        {
            try
            {
                await _service.AdicionarPokemon(criacaoPokemonDto);
                var pokemon = _mapper.Map<Pokemon>(criacaoPokemonDto);
                return CreatedAtAction(nameof(BuscarPokemonPorId), new { id = pokemon.Id }, pokemon);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno do servidor.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizaPokemon(int id, [FromBody] AtualizarPokemonDto atualizarPokemonDto)
        {
            try
            {
                await _service.AtualizarPokemon(id, atualizarPokemonDto);
                var pokemonAtualizado = await _service.BuscarPokemonPorId(id);
                return Ok(new { Mensagem = "Seu Pokémon foi alterado com sucesso!", Pokemon = pokemonAtualizado });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletaPokemon(int id)
        {
            try
            {
                await _service.DeletarPokemon(id);
                return Ok($"Pokémon {id} deletado com sucesso.");
            }
            catch
            {
                return NotFound("Pokemon não encontrado");
            }
        }
        [HttpDelete]
        public async Task<IActionResult> DeletarTodosPokemons()
        {
            try
            {
                await _service.DeletarTodosPokemons();
                return Ok("Todos os Pokémons foram deletados com sucesso.");
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}