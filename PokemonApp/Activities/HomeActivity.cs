using Android.App;
using Android.OS;
using PokemonApp.Presenter.Implementation;
using PokemonApp.Presenter.Interface;
using PokemonApp.Views;

namespace PokemonApp.Activities
{
    [Activity(Label = "Pokedex", MainLauncher = true, Icon = "@drawable/icon_pikachu")]
    public class HomeActivity : Activity, IHomeView
    {
        private IHomePresenter _homePresenter;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.HomeScreenLayout);

            _homePresenter = new HomePresenter(this, ApplicationContext);
            _homePresenter.InitialiseDb();
            _homePresenter.OnStart();
        }

        public void NavigateToViewPokemonScreen()
        {
            StartActivity(typeof(ViewPokemonActivity));
            Finish();
        }
    }
}