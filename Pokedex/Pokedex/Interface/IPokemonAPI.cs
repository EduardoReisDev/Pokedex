using System;
using System.Threading.Tasks;
using Pokedex.Model;
using Refit;
using Xamarin.Forms;

namespace Pokedex.Interface
{
    public interface IPokemonAPI
    { 

        [Get("/pokemon/{name}")]
        Task<Pokemon> GetPokemonAsync(string name);
        Task GetPokemonAsync(Entry entry_pokemon);
    }
}