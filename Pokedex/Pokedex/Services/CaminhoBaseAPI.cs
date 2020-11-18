using System;
namespace Pokedex.Services
{
    public class CaminhoBaseAPI
    {
        //caminho base para não precisar utilizar a url bruta em toda hora que for feito uma nova consulta.
        public static string BaseUrl => "https://pokeapi.co/api/v2";
    }
}
