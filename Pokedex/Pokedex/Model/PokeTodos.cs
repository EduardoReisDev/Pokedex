using System;
using System.Collections.Generic;

namespace Pokedex.Model
{
    //model TodosPokemon
    public class PokeTodos
    {
        public int count { get; set; }
        public string next { get; set; }
        public string previous { get; set; }
        public List<Results> results { get; set; }
    }

    public class Results
    {
        public string name { get; set; }
        public string imagem { get; set; }
    }
}
