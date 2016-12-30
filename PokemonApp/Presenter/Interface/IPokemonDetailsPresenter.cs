namespace PokemonApp.Presenter.Interface
{
    public interface IPokemonDetailsPresenter
    {
        void OnPokemonItemClick();
        string GetPokemonName();
        string GetPokemonId();
        int GetPokemonImageResource(string intentKey);
        int GetPokemonHp();
        int GetPokemonAttack();
        int GetPokemonSpeed();
        int GetPokemonDefense();
        string GetPokemonHeight();
        string GetPokemonWeight();
        string GetPokemonCatchRate();
        string GetPokemonHappiness();
        string GetPokemonEvolution();
        string GetPokemonType();
        string GetPokemonEggGroup();
        string GetPokemonAbilities();
    }
}