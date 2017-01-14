using Android.App;
using Android.OS;
using PokemonApp.AndroidExtensions;
using PokemonApp.Presenter.Implementation;
using PokemonApp.Presenter.Interface;
using PokemonApp.Views;

namespace PokemonApp.Activities
{
    [Activity(Label = "Pokedex", MainLauncher = true, Icon = "@drawable/icon_pikachu")]
    public class HomeActivity : Activity, IHomeView
    {
        private IHomePresenter _homePresenter;
        private SQLiteHelper _sqliteHelper;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.HomeScreenLayout);

            _sqliteHelper = new SQLiteHelper(ApplicationContext);
            _homePresenter = new HomePresenter(this, _sqliteHelper);

            _homePresenter.InitialiseDb();
            _homePresenter.StartApplication();
        }

        public void NavigateToViewPokemonScreen()
        {
            StartActivity(typeof(DisplayPokemonActivity));
            Finish();
        }
    }
}