using Android.Content;
using PokemonApp.Presenter.Implementation;

namespace PokemonApp.Views
{
    public interface IDisplayPokemonView
    {
        void NavigateToPokemonDetailsScreen(Intent intent);
        void SetProgressDialogMessage();
        void HideProgressDialogMessage();
        void SetImageAdapter();
        CustomImageAdapter GetImageAdapter();
    }
}