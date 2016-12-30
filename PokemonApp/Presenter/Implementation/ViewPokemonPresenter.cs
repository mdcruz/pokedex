using Android.Content;
using PokemonApp.Model.Database;
using PokemonApp.Model.Entity;
using PokemonApp.Model.Service;
using PokemonApp.Presenter.Interface;
using PokemonApp.Views;
using System.Collections.Generic;
using System.Linq;

namespace PokemonApp.Presenter.Implementation
{
    public class ViewPokemonPresenter : IViewPokemonPresenter
    {
        private IDisplayPokemonView _displayPokemonView;
        private PokemonService _pokemonService;
        private List<Pokemon> _pokemons;
        private Pokemon _selectedPokemon;

        public ViewPokemonPresenter(IDisplayPokemonView displayPokemonView)
        {
            _displayPokemonView = displayPokemonView;
            _pokemonService = new PokemonService(new SQLiteHelper());
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

        public void OnItemClick(int itemPosition)
        {
            _displayPokemonView.GetImageAdapter().NotifyDataSetChanged();
            _selectedPokemon = _displayPokemonView.GetImageAdapter().GetItemAtPosition(itemPosition);
        }

        public void OnItemSearch(string searchString)
        {
            _displayPokemonView.GetImageAdapter().Filter.InvokeFilter(searchString);
        }

        public void SetPokemonDetails(Intent intent)
        {
            intent.PutExtra("pokemonIcon", _selectedPokemon.image_resource);
            intent.PutExtra("pokemonName", _selectedPokemon.name);
            intent.PutExtra("pokemonId", _selectedPokemon.pkdx_id.ToString());
            intent.PutExtra("pokemonHp", _selectedPokemon.hp);
            intent.PutExtra("pokemonAttack", _selectedPokemon.attack);
            intent.PutExtra("pokemonSpeed", _selectedPokemon.speed);
            intent.PutExtra("pokemonWeight", _selectedPokemon.weight);
            intent.PutExtra("pokemonHeight", _selectedPokemon.height);
            intent.PutStringArrayListExtra("pokemonType", _selectedPokemon.types.Select(x => x.name).ToList());
            intent.PutStringArrayListExtra("pokemonEggType", _selectedPokemon.egg_groups.Select(x => x.name).ToList());
            intent.PutStringArrayListExtra("pokemonAbility", _selectedPokemon.abilities.Select(x => x.name).ToList());
            intent.PutExtra("pokemonCatchRate", _selectedPokemon.catch_rate.ToString());
            intent.PutExtra("pokemonDefense", _selectedPokemon.defense);
            intent.PutExtra("pokemonHappiness", _selectedPokemon.happiness.ToString());

            if (_selectedPokemon.evolutions.Count > 0)
            {
                intent.PutExtra("pokemonEvolutionTo", _selectedPokemon.evolutions[0].to);
                intent.PutExtra("pokemonEvolutionLevel", _selectedPokemon.evolutions[0].level.ToString());
                intent.PutExtra("pokemonEvolutionToIcon", _selectedPokemon.evolutions[0].image_resource);
            }

            else
            {
                intent.PutExtra("pokemonEvolutionToIcon", "image_not_found");
            }

            _displayPokemonView.NavigateToPokemonDetailsScreen(intent);
        }
    }
}