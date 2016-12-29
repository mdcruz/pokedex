
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using System.Globalization;

namespace PokemonApp.Activities
{
    [Activity(Label = "ViewPokemonScreen")]
    public class PokemonDetailsScreen : Activity
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

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.PokemonDetailsLayout);

            InitialiseControls();
            SetPokemonDetails();
        }

        private void SetPokemonDetails()
        {
            var pokemonName = Intent.GetStringExtra("pokemonName");
            var pokemonIcon = Intent.GetStringExtra("pokemonIcon");
            var pokemonEvolutionIcon = Intent.GetStringExtra("pokemonEvolutionToIcon");

            var resourceId = (int)typeof(Resource.Drawable).GetField(pokemonIcon).GetValue(null);
            var pokemonEvolutionId = (int)typeof(Resource.Drawable).GetField(pokemonEvolutionIcon).GetValue(null);
            var pokemonType = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(string.Join(", ", Intent.GetStringArrayListExtra("pokemonType")).ToLower());
            var abilities = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(string.Join(", ", Intent.GetStringArrayListExtra("pokemonAbility")).ToLower());

            var evolutionDesc = Intent.GetStringExtra("pokemonEvolutionTo") != null
                ? $"{pokemonName} evolves to {Intent.GetStringExtra("pokemonEvolutionTo")} at level {Intent.GetStringExtra("pokemonEvolutionLevel")}"
                : $"{pokemonName} has no evolution";

            PokemonItemImage.SetImageResource(resourceId);
            PokemonId.Text = $"#{Intent.GetStringExtra("pokemonId")}";
            PokemonItemName.Text = pokemonName;     
            PokemonHp.Progress = Intent.GetIntExtra("pokemonHp", 0);
            PokemonAttack.Progress = Intent.GetIntExtra("pokemonAttack", 0);
            PokemonSpeed.Progress = Intent.GetIntExtra("pokemonSpeed", 0);
            PokemonDefense.Progress = Intent.GetIntExtra("pokemonDefense", 0);
            PokemonHeight.Text = $"{Intent.GetStringExtra("pokemonHeight")} m";
            PokemonWeight.Text = $"{Intent.GetStringExtra("pokemonWeight")} kg";
            PokemonType.Text = pokemonType;
            PokemonEggType.Text = string.Join(", ", Intent.GetStringArrayListExtra("pokemonEggType"));
            PokemonAbility.Text = abilities;
            PokemonCatchRate.Text = $"{Intent.GetStringExtra("pokemonCatchRate")}%";
            PokemonHappiness.Text = Intent.GetIntExtra("pokemonHappiness", 0).ToString();
            EvolutionTextView.Text = evolutionDesc;
            PokemonEvolutionIcon.SetImageResource(resourceId);
            PokemonEvolutionIconTo.SetImageResource(pokemonEvolutionId);
        }

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