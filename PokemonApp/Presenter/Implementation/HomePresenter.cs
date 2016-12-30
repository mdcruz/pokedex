using Android.Content;
using PokemonApp.Model.Database;
using PokemonApp.Presenter.Interface;
using PokemonApp.Views;
using System.Timers;

namespace PokemonApp.Presenter.Implementation
{
    public class HomePresenter : IHomePresenter
    {
        private IHomeView _homeView;
        private Timer _timer;
        private SQLiteHelper _sqLiteHelper;

        public HomePresenter(IHomeView homeView, Context context)
        {
            _homeView = homeView;
            _sqLiteHelper = new SQLiteHelper(context);
        }

        public void InitialiseDb()
        {
            _sqLiteHelper.CreateDatabase();
        }

        public void OnStart()
        {
            _timer = new Timer();
            _timer.Interval = 500;
            _timer.Elapsed += Timer_Elapsed;
            _timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _timer.Stop();
            _homeView.NavigateToViewPokemonScreen();
        }
    }
}