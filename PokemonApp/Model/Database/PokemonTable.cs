using SQLite;

namespace PokemonApp.Model.Database
{
    public class PokemonTable
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull]
        public string PokemonName { get; set; }

        [NotNull]
        public string ImagePath { get; set; }
    }
}