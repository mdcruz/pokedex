
using Android.App;
using Android.OS;
using Android.Widget;
using PokemonApp.Presenter.Implementation;
using PokemonApp.Presenter.Interface;
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

            _pokemonDetailsPresenter = new PokemonDetailsPresenter(this, Intent);
            _pokemonDetailsPresenter.OnPokemonItemClick();
        }

        #region Override methods from IPokemonDetailsView

        public void SetPokemonMainDetails()
        {
            PokemonItemName.Text = _pokemonDetailsPresenter.GetPokemonName();
            PokemonId.Text = _pokemonDetailsPresenter.GetPokemonId();
            PokemonItemImage.SetImageResource(_pokemonDetailsPresenter.GetPokemonImageResource("pokemonIcon"));
        }

        public void SetPokemonStats()
        {
            PokemonHp.Progress = _pokemonDetailsPresenter.GetPokemonHp();
            PokemonAttack.Progress = _pokemonDetailsPresenter.GetPokemonAttack();
            PokemonSpeed.Progress = _pokemonDetailsPresenter.GetPokemonSpeed();
            PokemonDefense.Progress = _pokemonDetailsPresenter.GetPokemonDefense();
        }

        public void SetPokemonProfile()
        {
            PokemonHeight.Text = _pokemonDetailsPresenter.GetPokemonHeight();
            PokemonWeight.Text = _pokemonDetailsPresenter.GetPokemonWeight();
            PokemonType.Text = _pokemonDetailsPresenter.GetPokemonType();
            PokemonEggType.Text = _pokemonDetailsPresenter.GetPokemonEggGroup();
            PokemonAbility.Text = _pokemonDetailsPresenter.GetPokemonAbilities();
            PokemonCatchRate.Text = _pokemonDetailsPresenter.GetPokemonCatchRate();
            PokemonHappiness.Text = _pokemonDetailsPresenter.GetPokemonHappiness();
        }

        public void SetPokemonEvolution()
        {
            EvolutionTextView.Text = _pokemonDetailsPresenter.GetPokemonEvolution();
            PokemonEvolutionIcon.SetImageResource(_pokemonDetailsPresenter.GetPokemonImageResource("pokemonIcon"));
            PokemonEvolutionIconTo.SetImageResource(_pokemonDetailsPresenter.GetPokemonImageResource("pokemonEvolutionToIcon"));
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