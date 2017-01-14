using Android.App;
using Android.OS;
using Android.Widget;
using PokemonApp.Presenter.Implementation;
using PokemonApp.Presenter.Interface;
using PokemonApp.Utilities;
using PokemonApp.Views;

namespace PokemonApp.Activities
{
    [Activity(Label = "ViewPokemonScreen")]
    public class PokemonDetailsActivity : Activity, IPokemonDetailsView
    {
        #region UI Control properties

        private ImageView PokemonItemImage { get; set; }
        private TextView PokemonItemName { get; set; }
        private TextView PokemonId { get; set; }
        private ProgressBar PokemonHp { get; set; }
        private ProgressBar PokemonAttack { get; set; }
        private ProgressBar PokemonSpeed { get; set; }
        private ProgressBar PokemonDefense { get; set; }
        private TextView PokemonHeight { get; set; }
        private TextView PokemonWeight { get; set; }
        private TextView PokemonType { get; set; }
        private TextView PokemonEggType { get; set; }
        private TextView PokemonAbility { get; set; }
        private TextView PokemonCatchRate { get; set; }
        private TextView PokemonHappiness { get; set; }
        private TextView EvolutionTextView { get; set; }
        private ImageView PokemonEvolutionIcon { get; set; }
        private ImageView PokemonEvolutionIconTo { get; set; }

        #endregion

        private IPokemonDetailsPresenter _pokemonDetailsPresenter;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.PokemonDetailsLayout);
            InitialiseControls();

            _pokemonDetailsPresenter = new PokemonDetailsPresenter(this);
            _pokemonDetailsPresenter.ClickPokemon();
        }

        #region Override methods from IPokemonDetailsView

        public void SetPokemonMainDetails()
        {
            PokemonItemName.Text = GetPokemonName();
            PokemonId.Text = $"#{Intent.GetStringExtra(Constants.pokemonId)}";
            PokemonItemImage.SetImageResource(GetPokemonImageResource(Constants.pokemonIcon));
        }

        public void SetPokemonStats()
        {
            PokemonHp.Progress = Intent.GetIntExtra(Constants.pokemonHp, 0);
            PokemonAttack.Progress = Intent.GetIntExtra(Constants.pokemonAttack, 0);
            PokemonSpeed.Progress = Intent.GetIntExtra(Constants.pokemonSpeed, 0);
            PokemonDefense.Progress = Intent.GetIntExtra(Constants.pokemonDefense, 0);
        }

        public void SetPokemonProfile()
        {
            PokemonHeight.Text = $"{Intent.GetStringExtra(Constants.pokemonHeight)} m";
            PokemonWeight.Text = $"{Intent.GetStringExtra(Constants.pokemonWeight)} kg";
            PokemonType.Text = Intent.GetStringArrayListExtra(Constants.pokemonType).JoinList().ToLower().ToTitleCase();
            PokemonEggType.Text = Intent.GetStringArrayListExtra(Constants.pokemonEggType).JoinList();
            PokemonAbility.Text = Intent.GetStringArrayListExtra(Constants.pokemonAbility).JoinList().ToLower().ToTitleCase();
            PokemonCatchRate.Text = $"{Intent.GetStringExtra(Constants.pokemonCatchRate)}%";
            PokemonHappiness.Text = Intent.GetStringExtra(Constants.pokemonHappiness);
        }

        public void SetPokemonEvolution()
        {
            EvolutionTextView.Text = GetPokemonEvolution();
            PokemonEvolutionIcon.SetImageResource(GetPokemonImageResource(Constants.pokemonIcon));
            PokemonEvolutionIconTo.SetImageResource(GetPokemonImageResource(Constants.pokemonEvolutionToIcon));
        }

        public int GetPokemonImageResource(string intentKey)
        {
            var intentValue = Intent.GetStringExtra(intentKey);
            return (int)typeof(Resource.Drawable).GetField(intentValue).GetValue(null);
        }

        public string GetPokemonEvolution()
        {
            var evolutionDesc = Intent.GetStringExtra(Constants.pokemonEvolutionTo) != null
                ? $"{GetPokemonName()} evolves to {Intent.GetStringExtra(Constants.pokemonEvolutionTo)} at level {Intent.GetStringExtra(Constants.pokemonEvolutionLevel)}"
                : $"{GetPokemonName()} has no evolution";

            return evolutionDesc;
        }

        public string GetPokemonName()
        {
            return Intent.GetStringExtra(Constants.pokemonName);
        }

        #endregion

        private void InitialiseControls()
        {
            PokemonItemImage = FindViewById<ImageView>(Resource.Id.pokemon_item_image);
            PokemonItemName = FindViewById<TextView>(Resource.Id.pokemon_item_name);
            PokemonId = FindViewById<TextView>(Resource.Id.pokemon_item_id);
            PokemonHp = FindViewById<ProgressBar>(Resource.Id.pokemon_hp);
            PokemonAttack = FindViewById<ProgressBar>(Resource.Id.pokemon_attack);
            PokemonSpeed = FindViewById<ProgressBar>(Resource.Id.pokemon_speed);
            PokemonDefense = FindViewById<ProgressBar>(Resource.Id.pokemon_defense);
            PokemonHeight = FindViewById<TextView>(Resource.Id.pokemon_height);
            PokemonWeight = FindViewById<TextView>(Resource.Id.pokemon_weight);
            PokemonType = FindViewById<TextView>(Resource.Id.pokemon_type);
            PokemonEggType = FindViewById<TextView>(Resource.Id.pokemon_egg_type);
            PokemonAbility = FindViewById<TextView>(Resource.Id.pokemon_ability);
            PokemonCatchRate = FindViewById<TextView>(Resource.Id.pokemon_catch_rate);
            PokemonHappiness = FindViewById<TextView>(Resource.Id.pokemon_happiness);
            EvolutionTextView = FindViewById<TextView>(Resource.Id.pokemon_evolution_description);
            PokemonEvolutionIcon = FindViewById<ImageView>(Resource.Id.pokemon_evolution_image);
            PokemonEvolutionIconTo = FindViewById<ImageView>(Resource.Id.pokemon_evolution_image_to);
        }
    }
}