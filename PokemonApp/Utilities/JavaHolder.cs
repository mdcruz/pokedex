namespace PokemonApp.Utilities
{
    public class JavaHolder : Java.Lang.Object
    {
        public readonly object Instance;

        public JavaHolder(object instance)
        {
            Instance = instance;
        }
    }
}

