using PokemonApp.Utilities;
using SQLite;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace PokemonApp.Model.Database
{
    public class PokemonQueries
    {
        private SQLiteAsyncConnection _sqliteConnection;

        public PokemonQueries()
        {
            var dbPath = Path.Combine(Constants.DB_DIRECTORY, Constants.DB_FILENAME);

            _sqliteConnection = new SQLiteAsyncConnection(dbPath);
            _sqliteConnection.CreateTableAsync<PokemonTable>();
        }

        public async Task<List<PokemonTable>> GetPokemons()
        {
            return await _sqliteConnection.Table<PokemonTable>().ToListAsync();
        }

        public async Task<string> GetPokemonEvolution(string pokemonName)
        {
            return await _sqliteConnection.ExecuteScalarAsync<string>($"SELECT ImagePath FROM PokemonTable WHERE PokemonName = '{pokemonName.ToLower()}'");
        }
    }    
}