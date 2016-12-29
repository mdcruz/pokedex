using SQLite;

namespace PokemonApp.DataLayer.Tables
{
    public class Pokemon
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull]
        public string PokemonName { get; set; }

        [NotNull]
        public string ImagePath { get; set; }
    }
}