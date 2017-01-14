using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using PokemonApp.AndroidExtensions;
using PokemonApp.Presenter.Implementation;
using PokemonApp.Presenter.Interface;
using PokemonApp.Views;
using System.Collections.Generic;

namespace PokemonApp.Activities
{
    [Activity(Label = "Pokedex")]
    public class DisplayPokemonActivity : Activity, IDisplayPokemonView
    {
        #region UI Control properties

        private GridView PokemonGridView { get; set; }
        private SearchView PokemonSearch { get; set; }
        private ProgressDialog ProgressDialog { get; set; }

        #endregion

        private CustomImageAdapter _customAdapter;
        private IDisplayPokemonPresenter _displayPokemonPresenter;
        private Intent _intent;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.GridViewLayout);
            InitialiseControls();

            _displayPokemonPresenter = new DisplayPokemonPresenter(this);
            _displayPokemonPresenter.FillGridWithPokemon();

            PokemonSearch.QueryTextChange += OnPokemonSearch_QueryTextChange;
            PokemonGridView.ItemClick += OnPokemonGridView_ItemClick;
        }

        #region Override methods from IDisplayPokemonView

        public void SetProgressDialogMessage()
        {
            ProgressDialog = ProgressDialog.Show(this, GetString(Resource.String.ProgressHeaderMessage), 
                GetString(Resource.String.LoadingMessage), true);
        }

        public void HideProgressDialogMessage()
        {
            RunOnUiThread(() => ProgressDialog.Hide());
        }

        public void SetImageAdapter()
        {
            _customAdapter = new CustomImageAdapter(this, Resource.Layout.ViewPokemonLayout, _displayPokemonPresenter.GetPokemons());
            PokemonGridView.Adapter = _customAdapter;
        }

        public void NavigateToPokemonDetailsScreen()
        {
            StartActivity(_intent);
        }

        public void SetIntent()
        {
            _intent = new Intent(this, typeof(PokemonDetailsActivity));
        }

        public void SetIntentValue(string intentKey, string intentValue)
        {
            _intent.PutExtra(intentKey, intentValue);
        }

        public void SetIntentValue(string intentKey, int intentValue)
        {
            _intent.PutExtra(intentKey, intentValue);
        }

        public void SetIntentValue(string intentKey, IList<string> intentValue)
        {
            _intent.PutStringArrayListExtra(intentKey, intentValue);
        }

        #endregion

        private void OnPokemonSearch_QueryTextChange(object sender, SearchView.QueryTextChangeEventArgs e)
        {
            _customAdapter.Filter.InvokeFilter(e.NewText);
        }

        private void OnPokemonGridView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            _customAdapter.NotifyDataSetChanged();
            _displayPokemonPresenter.SetPokemonDetails(_customAdapter.GetItemAtPosition(e.Position));
        }

        private void InitialiseControls()
        {
            PokemonGridView = FindViewById<GridView>(Resource.Id.gridview);
            PokemonSearch = FindViewById<SearchView>(Resource.Id.searchview);
        }
    }
}