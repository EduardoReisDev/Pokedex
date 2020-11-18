using System;
using System.Collections.Generic;
using Pokedex.Services;
using Refit;
using Xamarin.Forms;

namespace Pokedex.View
{
    public partial class BuscarPokemon : ContentPage
    {
        public BuscarPokemon()
        {
            InitializeComponent();

            //chamando o método BuscarPokemonAPI ao clicar no botão de busca
            button_pokemon.Clicked += BuscarPokemonAPI;           
        }

        private async void BuscarPokemonAPI(object sender, EventArgs args)
        {
            //string pokemon recebe oque o usuário digita
            //eu uso o ToLower para deixar todas as letras minúsculas.
            //caso o pokemon seja digitado com a letra maiúscula, a busca não é feita com sucesso. 
            String pokemon = entry_pokemon.Text.Trim().ToLower();

            try
            {
                //apiCliente recebe a url
                var apiClient = RestService.For<IPokemonAPI>(CaminhoBaseAPI.BaseUrl);
                //pokemonnome recebe a url com o nome do pokemon
                var pokemonnome = await apiClient.GetPokemonAsync(pokemon);

                //operações feitas para apresentar a altura em metros e o peso em kilos
                float resultHeight = (float) pokemonnome.Height / (float) 10;
                float resultWeight = (float)pokemonnome.Weight / (float) 10;

                //apresentação dos dados na view
                id_pokemon.Text = string.Format($"ID: {pokemonnome.Id}");
                nome_pokemon.Text = string.Format($"Nome: {pokemonnome.Name}");
                altura_pokemon.Text = string.Format($"Altura: {resultHeight} m");
                peso_pokemon.Text = string.Format($"Peso: {resultWeight} kg");
                experiencia_pokemon.Text = string.Format($"Experiência Base: {pokemonnome.Base_experience}");

                //apresentação da imagem do pokemon pelo ID.
                //caso queira buscar pelo nome, use a url abaixo
                //https://img.pokemondb.net/artwork/{pokemonnome.Name}.jpg
                imagem.Source = string.Format($"https://pokeres.bastionbot.org/images/pokemon/{pokemonnome.Id}.png");
            }

            catch(Exception e)
            {
                Console.WriteLine("Erro na consulta do Pokemon: " + e.Message);
            }
        }

        //acessar a página de categoria
        public async void GoCategoriaPokemon(Object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CategoriaPokemon());
        }

        //acessar a página todos os pokemons
        public async void GoTodosPokemon(Object sender, EventArgs args)
        {
            await Navigation.PushAsync(new TodosPokemon());
        }
    }
}
