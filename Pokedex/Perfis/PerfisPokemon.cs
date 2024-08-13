using AutoMapper;
using Pokedex.Dtos;
using Pokedex.Models;

namespace Pokedex.Perfis
{
    public class PerfisPokemon : Profile
    {
        public PerfisPokemon()
        {
            CreateMap<AtualizarPokemonDto, Pokemon>();
            CreateMap<CriacaoPokemonDto, Pokemon>();
            CreateMap<LeituraPokemonDto, Pokemon>();
            CreateMap<Pokemon, AtualizarPokemonDto>();
            CreateMap<Pokemon, CriacaoPokemonDto>();
            CreateMap<Pokemon, LeituraPokemonDto>();
        }
    }
}
