using Android.App;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using Java.Lang;
using PokemonApp.Extensions;
using PokemonApp.Model;
using System.Collections.Generic;
using System.Linq;
using Object = Java.Lang.Object;

namespace PokemonApp.Activities
{
    public class CustomImageAdapter : ArrayAdapter<Pokemon>, IFilterable
    {
        private Activity _activity;
        private List<Pokemon> _pokemonItems;
        private List<Pokemon> _pokemons;
        private int _resourceLayoutId;

        public CustomImageAdapter(Activity activity, int resourceLayoutId, List<Pokemon> pokemons)
            : base(activity, resourceLayoutId, pokemons)
        {
            _activity = activity;
            _resourceLayoutId = resourceLayoutId;
            _pokemons = pokemons;

            Filter = new PokemonFilter(this);
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

        class PokemonFilter : Filter
        {
            private readonly CustomImageAdapter _adapter;

            public PokemonFilter(CustomImageAdapter adapter)
            {
                _adapter = adapter;
            }

            protected override FilterResults PerformFiltering(ICharSequence constraint)
            {
                var filterResults = new FilterResults();
                var results = new List<Pokemon>();

                if (_adapter._pokemonItems == null)
                    _adapter._pokemonItems = _adapter._pokemons;

                if (constraint == null) return filterResults;

                if (_adapter._pokemons != null && _adapter._pokemonItems.Any())
                    results.AddRange(_adapter._pokemonItems.Where(pokemon => pokemon.name.ToLower().Contains(constraint.ToString())));

                filterResults.Values = FromArray(results.Select(r => r.ToJavaObject()).ToArray());
                filterResults.Count = results.Count;

                constraint.Dispose();

                return filterResults;
            }

            protected override void PublishResults(ICharSequence constraint, FilterResults results)
            {
                using (var values = results.Values)
                    _adapter._pokemons = values.ToArray<Object>().Select(r => r.ToNetObject<Pokemon>()).ToList();

                _adapter.NotifyDataSetChanged();

                constraint.Dispose();
                results.Dispose();
            }
        }
    }
}