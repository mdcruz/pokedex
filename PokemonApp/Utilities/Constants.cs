using System;

namespace PokemonApp.Utilities
{
    public class Constants
    {
        public static string DB_DIRECTORY = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        public static string DB_FILENAME = "PokemonDB.sqlite";
        public const string baseUrl = "http://pokeapi.co/api/v1/";
        public const string pokemonResourceUrl = "pokemon/{0}";

        public const string imageNotFound = "image_not_found";
        public const string pokemonIcon = "pokemonIcon";
        public const string pokemonName = "pokemonName";
        public const string pokemonId = "pokemonId";
        public const string pokemonHp = "pokemonHp";
        public const string pokemonAttack = "pokemonAttack";
        public const string pokemonSpeed = "pokemonSpeed";
        public const string pokemonWeight = "pokemonWeight";
        public const string pokemonHeight = "pokemonHeight";
        public const string pokemonType = "pokemonType";
        public const string pokemonEggType = "pokemonEggType";
        public const string pokemonAbility = "pokemonAbility";
        public const string pokemonCatchRate = "pokemonCatchRate";
        public const string pokemonDefense = "pokemonDefense";
        public const string pokemonHappiness = "pokemonHappiness";
        public const string pokemonEvolutionTo = "pokemonEvolutionTo";
        public const string pokemonEvolutionLevel = "pokemonEvolutionLevel";
        public const string pokemonEvolutionToIcon = "pokemonEvolutionToIcon";
    }
}