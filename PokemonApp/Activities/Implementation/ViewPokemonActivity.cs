using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using PokemonApp.Presenter.Implementation;
using PokemonApp.Presenter.Interface;
using PokemonApp.Views;

namespace PokemonApp.Activities
{
    [Activity(Label = "Pokedex")]
    public class ViewPokemonActivity : Activity, IDisplayPokemonView
    {
        #region UI Control properties

        private GridView PokemonGridView { get; set; }
        private SearchView PokemonSearch { get; set; }
        private ProgressDialog ProgressDialog { get; set; }

        #endregion

        private CustomImageAdapter _customAdapter;
        private IViewPokemonPresenter _viewPokemonPresenter;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.GridViewLayout);
            InitialiseControls();

            _viewPokemonPresenter = new ViewPokemonPresenter(this);
            _viewPokemonPresenter.FillGridWithPokemon();

            PokemonSearch.QueryTextChange += PokemonSearch_QueryTextChange;
            PokemonGridView.ItemClick += PokemonGridView_ItemClick;
        }

        #region Override methods from IDisplayPokemonView

        public void NavigateToPokemonDetailsScreen(Intent intent)
        {
            StartActivity(intent);
        }

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
            _customAdapter = new CustomImageAdapter(this, Resource.Layout.ViewPokemonLayout, _viewPokemonPresenter.GetPokemons());
            PokemonGridView.Adapter = _customAdapter;
        }

        public CustomImageAdapter GetImageAdapter()
        {
            return _customAdapter;
        }

        #endregion

        private void PokemonSearch_QueryTextChange(object sender, SearchView.QueryTextChangeEventArgs e)
        {
            _viewPokemonPresenter.OnItemSearch(e.NewText);
        }

        private void PokemonGridView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            _viewPokemonPresenter.OnItemClick(e.Position);
            _viewPokemonPresenter.SetPokemonDetails(new Intent(this, typeof(PokemonDetailsActivity)));
        }

        private void InitialiseControls()
        {
            PokemonGridView = FindViewById<GridView>(Resource.Id.gridview);
            PokemonSearch = FindViewById<SearchView>(Resource.Id.searchview);
        }
    }
}