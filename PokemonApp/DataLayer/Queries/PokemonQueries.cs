using PokemonApp.DataAccess;
using PokemonApp.DataLayer.Tables;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokemonApp.DataLayer.Queries
{
    public class PokemonQueries
    {
        private SQLiteAsyncConnection _sqliteConnection;
        private SQLiteHelper _sqliteHelper;

        public PokemonQueries(SQLiteHelper sqliteHelper)
        {
            _sqliteHelper = sqliteHelper;
            _sqliteConnection = new SQLiteAsyncConnection(_sqliteHelper.GetDbPath());

            _sqliteConnection.CreateTableAsync<Pokemon>();
        }

        public async Task<List<Pokemon>> GetPokemons()
        {
            return await _sqliteConnection.Table<Pokemon>().ToListAsync();
        }

        public async Task<string> GetPokemonEvolution(string pokemonName)
        {
            return await _sqliteConnection.ExecuteScalarAsync<string>($"SELECT ImagePath FROM Pokemon WHERE PokemonName = '{pokemonName.ToLower()}'");
        }
    }    
}