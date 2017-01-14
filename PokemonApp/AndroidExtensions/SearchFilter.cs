using Android.Widget;
using Java.Lang;
using PokemonApp.Utilities;
using PokemonApp.Model.Entity;
using System.Collections.Generic;
using System.Linq;
using Object = Java.Lang.Object;

namespace PokemonApp.AndroidExtensions
{
    public class SearchFilter : Filter
    {
        private CustomImageAdapter _adapter;

        public SearchFilter(CustomImageAdapter adapter)
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