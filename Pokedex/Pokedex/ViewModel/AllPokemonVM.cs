using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Pokedex.Model;

namespace Pokedex.ViewModel
{
    public class AllPokemonVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public List<Results> ListPokemon { get; set; }

        public AllPokemonVM()
        {
            ListPokemon = new List<Results>();
            //diferente da categoria, eu já chama o método aqui pra quando o usuário abrir a página,
            //a consulta já ser feita
            FeedList();
        }

        public async void FeedList()
        {
            ListPokemon = await GetAllPokemons();
        }

        //requisição para buscar todos os pokemons
        public async Task<List<Results>> GetAllPokemons()
        {
            //url com todos os pokemons
            var url = "https://pokeapi.co/api/v2/pokemon/?offset=20&limit=50";

            using (var httpClient = new HttpClient())
            {
                try
                {
                    var content = httpClient.GetStringAsync(url).Result;

                    var returnConsult = JsonConvert.DeserializeObject<AllPokemonModel>(content);

                    //results é a lista retornada da model(PokeTodos)
                    return returnConsult.results;
                }

                catch (Exception exc)
                {
                    Console.WriteLine(exc.Message);
                    //await DisplayAlert("Erro", "Ocorreu um erro ao buscar os pokemons: " + exc.Message, "OK");
                    return new List<Results>();
                }
            }
        }
    }
}
