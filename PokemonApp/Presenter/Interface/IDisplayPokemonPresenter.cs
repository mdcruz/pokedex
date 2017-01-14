using PokemonApp.Model.Entity;
using System.Collections.Generic;

namespace PokemonApp.Presenter.Interface
{
    public interface IDisplayPokemonPresenter
    {
        void FillGridWithPokemon();
        List<Pokemon> GetPokemons();
        void SetPokemonDetails(Pokemon selectedPokemon);
    }
}