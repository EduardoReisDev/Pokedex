using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pokedex.Model;
using Refit;
using Xamarin.Forms;

namespace Pokedex.Services
{
    
    public class PokemonAPI //IPokemonAPI
    {
        private readonly IPokemonAPI _pokemonAPI;

        public PokemonAPI()
        {
            _pokemonAPI = RestService.For<IPokemonAPI>(CaminhoBaseAPI.BaseUrl);
        }

        public async Task<ICollection<TodosPokemon>> GetAllPokemon()
        {
            return await _pokemonAPI.GetAllPokemon();
        }

        public Task<ICollection<TipoPokemon>> GetAllTipoPokemon()
        {
            throw new NotImplementedException();
        }

        //public Task<Pokemon> GetPokemonAsync(string name)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task GetPokemonAsync(Entry entry_pokemon)
        //{
        //    throw new NotImplementedException();
        //}

        public Task<TipoPokemon> GetTipoPokemonAsync(string tipo)
        {
            throw new NotImplementedException();
        }

        public Task GetTipoPokemonAsync(Entry entry_tipo)
        {
            throw new NotImplementedException();
        }
    }
}
