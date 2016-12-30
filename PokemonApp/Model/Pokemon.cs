using System.Collections.Generic;

namespace PokemonApp.Model
{
    public class Ability
    {
        public string name { get; set; }
    }

    public class EggGroup
    {
        public string name { get; set; }
    }

    public class Evolution
    {
        public int level { get; set; }
        public string to { get; set; }
        public string image_resource { get; set; }
    }

    public class Type
    {
        public string name { get; set; }
    }

    public class Pokemon
    {
        public List<Ability> abilities { get; set; }
        public int attack { get; set; }
        public int catch_rate { get; set; }
        public int defense { get; set; }
        public List<EggGroup> egg_groups { get; set; }
        public List<Evolution> evolutions { get; set; }
        public int happiness { get; set; }
        public string height { get; set; }
        public int hp { get; set; }
        public string name { get; set; }
        public int pkdx_id { get; set; }
        public int speed { get; set; }
        public List<Type> types { get; set; }
        public string weight { get; set; }
        public string image_resource { get; set; }
    }
    

}