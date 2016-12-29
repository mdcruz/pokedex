using PokemonApp.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokemonApp.RepositoryLayer
{
    public interface IPokemonRepository
    {
        Task<List<Pokemon>> GetPokemons();
        //Task<string> GetPokemonEvolutionIcon(string pokemonName);
    }
}