using Android.App;
using Android.OS;
using Android.Widget;
using PokemonApp.DataAccess;
using System.Timers;

namespace PokemonApp.Activities
{
    [Activity(Label = "Pokedex", MainLauncher = true, Icon = "@drawable/icon_pikachu")]
    public class HomeScreen : Activity
    {
        Button ViewPokemonsButton { get; set; }

        Timer timer;
        SQLiteHelper _sqliteHelper;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.HomeScreenLayout);

            _sqliteHelper = new SQLiteHelper(this);
            _sqliteHelper.CopyDatabase();

            timer = new Timer();
            timer.Interval = 500;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            timer.Stop();
            StartActivity(typeof(ViewPokemonScreen));
            Finish();
        }
    }
}