using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pokedex.Model;
using Refit;
using Xamarin.Forms;

namespace Pokedex.Services
{
    public interface IPokemonAPI
    {
        //REFIT
        //interface com o caminho da url que será feito o get na api
        //metodod para fazer o get na api passando em patrameto o nome do pokemon que o usuário vai digitar
        [Get("/pokemon/{name}")]
        Task<PokemonSearchModel> GetPokemonAsync(string name);
        Task GetPokemonAsync(Entry entry_pokemon);
    }
}
