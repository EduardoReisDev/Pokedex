using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Pokedex.Model;
using Pokedex.ViewModel;
using Xamarin.Forms;

namespace Pokedex.View
{
    public partial class TodosPokemon : ContentPage
    {
        //utilizando e instanciando httpClient
        private readonly HttpClient _client = new HttpClient();

        public TodosPokemon()
        {
            InitializeComponent();
            //diferente da categoria, eu já chama o método aqui pra quando o usuário abrir a página,
            //a consulta já ser feita
            PopulaLista();
        }

        public async void PopulaLista()
        {
            list_pokemons.ItemsSource = await GetAllPokemons();
        }

        //requisição para buscar todos os pokemons
        public async Task<List<Results>> GetAllPokemons()
        {
            try
            {
                //url com todos os pokemons
                var url = "https://pokeapi.co/api/v2/pokemon/?offset=20&limit=50";

                string content = await _client.GetStringAsync(url);

                var retornoConsulta = JsonConvert.DeserializeObject<PokeTodos>(content);

                //results é a lista retornada da model(PokeTodos)
                return retornoConsulta.results;
            }

            catch (Exception exc)
            {
                await DisplayAlert("Erro", "Ocorreu um erro ao buscar os pokemons: " + exc.Message, "OK");
                return new List<Results>();
            }
        }
    }
}