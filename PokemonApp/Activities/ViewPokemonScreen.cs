using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using PokemonApp.DataAccess;
using PokemonApp.Model;
using PokemonApp.RepositoryLayer;
using System;
using System.Linq;
using System.Collections.Generic;

namespace PokemonApp.Activities
{
    [Activity(Label = "Pokedex")]
    public class ViewPokemonScreen : Activity
    {
        #region UI Control properties

        private GridView PokemonGridView { get; set; }
        private SearchView PokemonSearch { get; set; }

        #endregion

        private List<Pokemon> _pokemons;
        private PokemonRepository _pokemonRepository;
        private ProgressDialog _progressDialog;
        private CustomImageAdapter _customAdapter;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.GridViewLayout);
            InitialiseControls();

            _pokemonRepository = new PokemonRepository(new SQLiteHelper());

            FillGridView();
        }

		private async void FillGridView()
        {
            _progressDialog = ProgressDialog.Show(this, "Please wait...", GetString(Resource.String.LoadingMessage), true);
            _pokemons = await _pokemonRepository.GetPokemons();

            RunOnUiThread(() => _progressDialog.Hide());

            _customAdapter = new CustomImageAdapter(this, Resource.Layout.ViewPokemonLayout, _pokemons);

            PokemonGridView.Adapter = _customAdapter;

            PokemonSearch.QueryTextChange += PokemonSearch_QueryTextChange;
            PokemonGridView.ItemClick += PokemonGridView_ItemClick;
        }

        private void PokemonSearch_QueryTextChange(object sender, SearchView.QueryTextChangeEventArgs e)
        {
            _customAdapter.Filter.InvokeFilter(e.NewText);
        }

        private void PokemonGridView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            _customAdapter.NotifyDataSetChanged();

            var selectedPokemon = _customAdapter.GetItemAtPosition(e.Position);
         
            var intent = new Intent(this, typeof(PokemonDetailsScreen));
            intent.PutExtra("pokemonIcon", selectedPokemon.image_resource);
            intent.PutExtra("pokemonName", selectedPokemon.name);
            intent.PutExtra("pokemonId", selectedPokemon.pkdx_id.ToString());
            intent.PutExtra("pokemonHp", selectedPokemon.hp);
            intent.PutExtra("pokemonAttack", selectedPokemon.attack);
            intent.PutExtra("pokemonSpeed", selectedPokemon.speed);
            intent.PutExtra("pokemonSpAttack", selectedPokemon.sp_atk);
            intent.PutExtra("pokemonWeight", selectedPokemon.weight);
            intent.PutExtra("pokemonHeight", selectedPokemon.height);
            intent.PutStringArrayListExtra("pokemonType", selectedPokemon.types.Select(x => x.name).ToList());
            intent.PutStringArrayListExtra("pokemonEggType", selectedPokemon.egg_groups.Select(x => x.name).ToList());
            intent.PutStringArrayListExtra("pokemonAbility", selectedPokemon.abilities.Select(x => x.name).ToList());
            intent.PutExtra("pokemonCatchRate", selectedPokemon.catch_rate.ToString());
            intent.PutExtra("pokemonDefense", selectedPokemon.defense);
            intent.PutExtra("pokemonDefenseAttack", selectedPokemon.sp_def);
            intent.PutExtra("pokemonHappiness", selectedPokemon.happiness);

            if(selectedPokemon.evolutions.Count > 0)
            {
                intent.PutExtra("pokemonEvolutionTo", selectedPokemon.evolutions[0].to);
                intent.PutExtra("pokemonEvolutionLevel", selectedPokemon.evolutions[0].level.ToString());
                intent.PutExtra("pokemonEvolutionToIcon", selectedPokemon.evolutions[0].image_resource);
            }

            else
            {
                intent.PutExtra("pokemonEvolutionToIcon", "image_not_found");
            }

            StartActivity(intent);
        }

        private void InitialiseControls()
        {
            PokemonGridView = FindViewById<GridView>(Resource.Id.gridview);
            PokemonSearch = FindViewById<SearchView>(Resource.Id.searchview);
        }
    }
}