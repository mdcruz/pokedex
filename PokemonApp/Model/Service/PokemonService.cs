using Akavache;
using Newtonsoft.Json;
using PokemonApp.Model.Database;
using PokemonApp.Model.Entity;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace PokemonApp.Model.Service
{
    public class PokemonService
    {
        private const string _baseUrl = "http://pokeapi.co/api/v1/";
        private const string _pokemonResourceUrl = "pokemon/{0}";
        private SQLiteHelper _sqliteHelper;
        private PokemonQueries _pokemonQueries;

        public PokemonService(SQLiteHelper sqliteHelper)
        {
            _sqliteHelper = sqliteHelper;
            _pokemonQueries = new PokemonQueries(_sqliteHelper);
        }

        public async Task<List<Pokemon>> GetPokemons()
        {
            var pokemonModelList = new List<Pokemon>();

            var cachedPokemons = BlobCache.LocalMachine.GetAndFetchLatest(
                "Pokemons",
                () => GetPokemonsFromService(),
                offset =>
                {
                    TimeSpan elapsed = DateTimeOffset.Now - offset;
                    return elapsed > new TimeSpan(hours: 24, minutes: 0, seconds: 0);
            });

            cachedPokemons.Subscribe(x => {
                pokemonModelList = x;
            });

            pokemonModelList = await cachedPokemons;
            return pokemonModelList;
        }

        private async Task<List<Pokemon>> GetPokemonsFromService()
        {
            var pokemonModelList = new List<Pokemon>();
            Pokemon pokemonModel = null;

            var pokemonList = await _pokemonQueries.GetPokemons();

            foreach (var pokemon in pokemonList)
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync(_baseUrl + string.Format(_pokemonResourceUrl, pokemon.Id));

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        pokemonModel = JsonConvert.DeserializeObject<Pokemon>(content);
                        pokemonModel.image_resource = pokemon.ImagePath;

                        await SetPokemonEvolutionIcon(pokemonModel);
                      
                        pokemonModelList.Add(pokemonModel);
                    }
                }
            }

            return pokemonModelList;
        }

        private async Task SetPokemonEvolutionIcon(Pokemon pokemonModel)
        {
            if (pokemonModel.evolutions.Count > 0)
            {
                var pokemonEvolution = await _pokemonQueries.GetPokemonEvolution(pokemonModel.evolutions[0].to);

                pokemonModel.evolutions[0].image_resource = !string.IsNullOrEmpty(pokemonEvolution)
                    ? pokemonEvolution
                    : "image_not_found";
            }
        }
    }
}