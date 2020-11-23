using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Pokedex.Model;
using Xamarin.Forms;

namespace Pokedex.View
{
    public partial class CategoryPokemon : ContentPage
    {
        //Utilizando e instanciando HttpClient.
        private readonly HttpClient _client = new HttpClient();
        string url;

        public CategoryPokemon()
        {
            InitializeComponent();
        }

        //Pegando a categoria selecionada no picker
        public void SelectedCategory(object sender, EventArgs args)
        {
            var selectedItem = category_pokemon.Items[category_pokemon.SelectedIndex];

            //montando a URL com a categoria selecionada
            url = string.Format($"https://pokeapi.co/api/v2/type/{selectedItem}");

            //chamando o método pra popular a lista
            FeedList();
        }

        public async void FeedList()
        {
            list_pokemons.ItemsSource = await GetAllPokemons();
        }

        //requisição para buscar todos os pokemons 
        public async Task<List<Results>> GetAllPokemons()
        {
            try
            {
                //deserializa o json e monta o objeto para popular lista.
                string content = await _client.GetStringAsync(url);
                var returnConsult = JsonConvert.DeserializeObject<PokemonCategoryModel>(content);
                //linq para fazer a consulta
                return returnConsult.pokemon.Select(x => x.pokemon).ToList();

                //TENTATIVA FOTO POKEMON
                //var listPokemon = retornoConsulta.pokemon.Select(x => x.pokemon).ToList();
                //var listPokemonName = listPokemon.Select(x => x.name).ToList();
                //listPokemonFoto = string.Format($"https://img.pokemondb.net/artwork/{listPokemonName.name}.jpg");
            }

            catch (Exception exc)
            {
                await DisplayAlert("Erro", "Ocorreu um erro ao buscar os pokemons: " + exc.Message, "OK");
                return new List<Results>();
            }
        }
    }
}
