using Android.Content;
using PokemonApp.Presenter.Interface;
using PokemonApp.Views;
using System.Globalization;

namespace PokemonApp.Presenter.Implementation
{
    public class PokemonDetailsPresenter : IPokemonDetailsPresenter
    {
        private IPokemonDetailsView _pokemonDetailsView;
        private Intent _intent;

        public PokemonDetailsPresenter(IPokemonDetailsView pokemonDetailsView, Intent intent)
        {
            _pokemonDetailsView = pokemonDetailsView;
            _intent = intent;
        }

        public void OnPokemonItemClick()
        {
            _pokemonDetailsView.SetPokemonMainDetails();
            _pokemonDetailsView.SetPokemonStats();
            _pokemonDetailsView.SetPokemonProfile();
            _pokemonDetailsView.SetPokemonEvolution();
        }

        public string GetPokemonEvolution()
        {
            var evolutionDesc = _intent.GetStringExtra("pokemonEvolutionTo") != null
                ? $"{GetPokemonName()} evolves to {_intent.GetStringExtra("pokemonEvolutionTo")} at level {_intent.GetStringExtra("pokemonEvolutionLevel")}"
                : $"{GetPokemonName()} has no evolution";

            return evolutionDesc;
        }

        public string GetPokemonName()
        {
            return _intent.GetStringExtra("pokemonName");
        }

        public string GetPokemonId()
        {
            return $"#{_intent.GetStringExtra("pokemonId")}";
        }

        public int GetPokemonImageResource(string intentKey)
        {
            var intentValue = _intent.GetStringExtra(intentKey);
            return (int)typeof(Resource.Drawable).GetField(intentValue).GetValue(null);
        }

        public int GetPokemonHp()
        {
            return _intent.GetIntExtra("pokemonHp", 0);
        }

        public int GetPokemonAttack()
        {
            return _intent.GetIntExtra("pokemonAttack", 0);
        }

        public int GetPokemonSpeed()
        {
            return _intent.GetIntExtra("pokemonSpeed", 0);
        }

        public int GetPokemonDefense()
        {
            return _intent.GetIntExtra("pokemonDefense", 0);
        }

        public string GetPokemonHeight()
        {
            return $"{_intent.GetStringExtra("pokemonHeight")} m";
        }

        public string GetPokemonWeight()
        {
            return $"{_intent.GetStringExtra("pokemonWeight")} kg";
        }

        public string GetPokemonCatchRate()
        {
            return $"{_intent.GetStringExtra("pokemonCatchRate")}%";
        }

        public string GetPokemonHappiness()
        {
            return _intent.GetStringExtra("pokemonHappiness");
        }

        public string GetPokemonType()
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(string.Join(", ", _intent.GetStringArrayListExtra("pokemonType")).ToLower());
        }

        public string GetPokemonEggGroup()
        {
            return string.Join(", ", _intent.GetStringArrayListExtra("pokemonEggType"));
        }

        public string GetPokemonAbilities()
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(string.Join(", ", _intent.GetStringArrayListExtra("pokemonAbility")).ToLower());
        }
    }
}