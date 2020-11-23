using System;
namespace Pokedex.Services
{
    public class BasePathAPI
    {
        //caminho base para não precisar utilizar a url bruta em toda hora que for feito uma nova consulta.
        public static string pathUrl => "https://pokeapi.co/api/v2";
    }
}
