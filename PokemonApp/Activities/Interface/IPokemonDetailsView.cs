namespace PokemonApp.Views
{
    public interface IPokemonDetailsView
    {
        void SetPokemonMainDetails();
        void SetPokemonStats();
        void SetPokemonProfile();
        void SetPokemonEvolution();
        string GetPokemonName();
        int GetPokemonImageResource(string key);
        string GetPokemonEvolution();
    }
}