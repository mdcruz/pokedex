using PokemonApp.Model.Entity;
using PokemonApp.Model.Service;
using PokemonApp.Presenter.Interface;
using PokemonApp.Utilities;
using PokemonApp.Views;
using System.Collections.Generic;
using System.Linq;

namespace PokemonApp.Presenter.Implementation
{
    public class DisplayPokemonPresenter : IDisplayPokemonPresenter
    {
        private IDisplayPokemonView _displayPokemonView;
        private PokemonService _pokemonService;
        private List<Pokemon> _pokemons;

        public DisplayPokemonPresenter(IDisplayPokemonView displayPokemonView)
        {
            _displayPokemonView = displayPokemonView;
            _pokemonService = new PokemonService();
        }

        public async void FillGridWithPokemon()
        {
            _displayPokemonView.SetProgressDialogMessage();

             _pokemons = await _pokemonService.GetPokemons();

            _displayPokemonView.HideProgressDialogMessage();
            _displayPokemonView.SetImageAdapter();
        }

        public List<Pokemon> GetPokemons()
        {
            return _pokemons;
        }

        public void SetPokemonDetails(Pokemon selectedPokemon)
        {
            _displayPokemonView.SetIntent();

            _displayPokemonView.SetIntentValue(Constants.pokemonIcon, selectedPokemon.image_resource);
            _displayPokemonView.SetIntentValue(Constants.pokemonName, selectedPokemon.name);
            _displayPokemonView.SetIntentValue(Constants.pokemonId, selectedPokemon.pkdx_id.ToString());
            _displayPokemonView.SetIntentValue(Constants.pokemonHp, selectedPokemon.hp);
            _displayPokemonView.SetIntentValue(Constants.pokemonAttack, selectedPokemon.attack);
            _displayPokemonView.SetIntentValue(Constants.pokemonSpeed, selectedPokemon.speed);
            _displayPokemonView.SetIntentValue(Constants.pokemonWeight, selectedPokemon.weight);
            _displayPokemonView.SetIntentValue(Constants.pokemonHeight, selectedPokemon.height);
            _displayPokemonView.SetIntentValue(Constants.pokemonType, selectedPokemon.types.Select(x => x.name).ToList());
            _displayPokemonView.SetIntentValue(Constants.pokemonEggType, selectedPokemon.egg_groups.Select(x => x.name).ToList());
            _displayPokemonView.SetIntentValue(Constants.pokemonAbility, selectedPokemon.abilities.Select(x => x.name).ToList());
            _displayPokemonView.SetIntentValue(Constants.pokemonCatchRate, selectedPokemon.catch_rate.ToString());
            _displayPokemonView.SetIntentValue(Constants.pokemonDefense, selectedPokemon.defense);
            _displayPokemonView.SetIntentValue(Constants.pokemonHappiness, selectedPokemon.happiness.ToString());

            if (selectedPokemon.evolutions.Count > 0)
            {
                _displayPokemonView.SetIntentValue(Constants.pokemonEvolutionTo, selectedPokemon.evolutions[0].to);
                _displayPokemonView.SetIntentValue(Constants.pokemonEvolutionLevel, selectedPokemon.evolutions[0].level.ToString());
                _displayPokemonView.SetIntentValue(Constants.pokemonEvolutionToIcon, selectedPokemon.evolutions[0].image_resource);
            }

            else
            {
                _displayPokemonView.SetIntentValue(Constants.pokemonEvolutionToIcon, Constants.imageNotFound);
            }

            _displayPokemonView.NavigateToPokemonDetailsScreen();
        }
    }
}