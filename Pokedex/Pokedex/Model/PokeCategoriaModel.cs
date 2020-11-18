using System;
using System.Collections.Generic;

namespace Pokedex.Model
{
    //model CategoriaPokemon
    public class PokeCategoriaModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public List<PokemonCategoria> pokemon { get; set; }
    }

    public class PokemonCategoria
    {
        public int slot { get; set; }
        public Results pokemon { get; set; }
    }
}
