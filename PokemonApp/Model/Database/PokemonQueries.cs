using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokemonApp.Model.Database
{
    public class PokemonQueries
    {
        private SQLiteAsyncConnection _sqliteConnection;
        private SQLiteHelper _sqliteHelper;

        public PokemonQueries(SQLiteHelper sqliteHelper)
        {
            _sqliteHelper = sqliteHelper;
            _sqliteConnection = new SQLiteAsyncConnection(_sqliteHelper.GetDbPath());

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