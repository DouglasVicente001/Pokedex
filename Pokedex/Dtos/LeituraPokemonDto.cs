namespace Pokedex.Dtos
{
    public class LeituraPokemonDto
    {
        public int Id { get; set; }
        public string NomePokemom { get; set; }
        public float Peso { get; set; }
        public float Altura { get; set; }
        public string Tipo { get; set; }
        public DateTime HoraConsulta { get; set; }
    }
}
