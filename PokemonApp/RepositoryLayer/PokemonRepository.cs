using PokemonApp.Model.Database;
using PokemonApp.Model.Entity;
using PokemonApp.Model.Service;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokemonApp.RepositoryLayer
{
    public class PokemonRepository : IPokemonRepository
    {
        private readonly PokemonService _pokemonService;

        public PokemonRepository(SQLiteHelper sqliteHelper)
        {
            _pokemonService = new PokemonService(sqliteHelper);
        }

        public async Task<List<Pokemon>> GetPokemons()
        {
            return await _pokemonService.GetPokemons();
        }
    }
}