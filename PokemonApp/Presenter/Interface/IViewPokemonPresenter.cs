using Android.Content;
using PokemonApp.Model.Entity;
using System.Collections.Generic;

namespace PokemonApp.Presenter.Interface
{
    public interface IViewPokemonPresenter
    {
        void FillGridWithPokemon();
        void OnItemClick(int itemPosition);
        void OnItemSearch(string searchString);
        List<Pokemon> GetPokemons();
        void SetPokemonDetails(Intent intent);
    }
}