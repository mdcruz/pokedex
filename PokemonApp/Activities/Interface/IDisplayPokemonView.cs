using System.Collections.Generic;

namespace PokemonApp.Views
{
    public interface IDisplayPokemonView
    {
        void NavigateToPokemonDetailsScreen();
        void SetProgressDialogMessage();
        void HideProgressDialogMessage();
        void SetImageAdapter();
        void SetIntentValue(string intentKey, string intentValue);
        void SetIntentValue(string intentKey, int intentValue);
        void SetIntentValue(string intentKey, IList<string> intentValue);
        void SetIntent();
    }
}