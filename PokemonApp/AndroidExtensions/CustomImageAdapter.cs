using Android.App;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using PokemonApp.Model.Entity;
using System.Collections.Generic;

namespace PokemonApp.AndroidExtensions
{
    public class CustomImageAdapter : ArrayAdapter<Pokemon>, IFilterable
    {
        private Activity _activity;
        public List<Pokemon> _pokemonItems;
        public List<Pokemon> _pokemons;
        private int _resourceLayoutId;

        public CustomImageAdapter(Activity activity, int resourceLayoutId, List<Pokemon> pokemons)
            : base(activity, resourceLayoutId, pokemons)
        {
            _activity = activity;
            _resourceLayoutId = resourceLayoutId;
            _pokemons = pokemons;

            Filter = new SearchFilter(this);
        }

        public override Filter Filter { get; }

        public override int Count
        {
            get
            {
                return _pokemons.Count;
            }
        }

        public Pokemon GetItemAtPosition(int position)
        {
            return _pokemons[position];
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? _activity.LayoutInflater.Inflate(_resourceLayoutId, parent, false);

            var pokemonImageIcon = view.FindViewById<ImageView>(Resource.Id.pokemonIcon);
            var pokemonText = view.FindViewById<TextView>(Resource.Id.pokemonName);

            pokemonText.Text = _pokemons[position].name;

            var resourceId = (int)typeof(Resource.Drawable).GetField(_pokemons[position].image_resource).GetValue(null);
            pokemonImageIcon.SetImageBitmap(BitmapFactory.DecodeResource(_activity.Resources, resourceId));

            return view;
        }
    }
}