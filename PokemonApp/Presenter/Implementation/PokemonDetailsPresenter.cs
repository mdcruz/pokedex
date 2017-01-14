using PokemonApp.Presenter.Interface;
using PokemonApp.Views;

namespace PokemonApp.Presenter.Implementation
{
    public class PokemonDetailsPresenter : IPokemonDetailsPresenter
    {
        private IPokemonDetailsView _pokemonDetailsView;

        public PokemonDetailsPresenter(IPokemonDetailsView pokemonDetailsView)
        {
            _pokemonDetailsView = pokemonDetailsView;
        }

        public void ClickPokemon()
        {
            _pokemonDetailsView.SetPokemonMainDetails();
            _pokemonDetailsView.SetPokemonStats();
            _pokemonDetailsView.SetPokemonProfile();
            _pokemonDetailsView.SetPokemonEvolution();
        }
    }
}