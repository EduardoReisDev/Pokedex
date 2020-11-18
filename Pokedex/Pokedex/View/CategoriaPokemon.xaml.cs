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
    public partial class CategoriaPokemon : ContentPage
    {
        //Utilizando e instanciando HttpClient.
        private readonly HttpClient _client = new HttpClient();
        string url;

        public CategoriaPokemon()
        {
            InitializeComponent();
        }

        //Pegando a categoria selecionada no picker
        public void CategoriaSelecionada(object sender, EventArgs args)
        {
            var itemSelecionado = categoria_pokemon.Items[categoria_pokemon.SelectedIndex];

            //montando a URL com a categoria selecionada
            url = string.Format($"https://pokeapi.co/api/v2/type/{itemSelecionado}");

            //chamando o método pra popular a lista
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
                //deserializa o json e monta o objeto para popular lista.
                string content = await _client.GetStringAsync(url);
                var retornoConsulta = JsonConvert.DeserializeObject<PokeCategoriaModel>(content);
                //linq para fazer a consulta
                return retornoConsulta.pokemon.Select(x => x.pokemon).ToList();

                //TENTATTIVA FOTO POKEMON
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
