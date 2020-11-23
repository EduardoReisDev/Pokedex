using System;
using System.Collections.Generic;
using Pokedex.Services;
using Refit;
using Xamarin.Forms;

namespace Pokedex.View
{
    public partial class SearchPokemon : ContentPage
    {
        //REFIT
        public SearchPokemon()
        {
            InitializeComponent();

            //chamando o método BuscarPokemonAPI ao clicar no botão de busca
            button_pokemon.Clicked += SearchPokemonAPI;           
        }

        private async void SearchPokemonAPI(object sender, EventArgs args)
        {
            //string pokemon recebe oque o usuário digita
            //eu uso o ToLower para deixar todas as letras minúsculas.
            //caso o pokemon seja digitado com a letra maiúscula, a busca não é feita com sucesso. 
            String pokemon = entry_pokemon.Text.Trim().ToLower();

            try
            {
                //apiCliente recebe a url
                var apiClient = RestService.For<IPokemonAPI>(BasePathAPI.pathUrl);
                //pokemonnome recebe a url com o nome do pokemon
                var pokemonname = await apiClient.GetPokemonAsync(pokemon);

                //operações feitas para apresentar a altura em metros e o peso em kilos
                float resultHeight = (float) pokemonname.Height / (float) 10;
                float resultWeight = (float)pokemonname.Weight / (float) 10;

                //apresentação dos dados na view
                id_pokemon.Text = string.Format($"ID: {pokemonname.Id}");
                name_pokemon.Text = string.Format($"Nome: {pokemonname.Name}");
                height_pokemon.Text = string.Format($"Altura: {resultHeight} m");
                weight_pokemon.Text = string.Format($"Peso: {resultWeight} kg");
                experience_pokemon.Text = string.Format($"Experiência Base: {pokemonname.Base_experience}");

                //apresentação da imagem do pokemon pelo ID.
                //caso queira buscar pelo nome, use a url abaixo
                //https://img.pokemondb.net/artwork/{pokemonnome.Name}.jpg
                image.Source = string.Format($"https://pokeres.bastionbot.org/images/pokemon/{pokemonname.Id}.png");
            }

            catch(Exception e)
            {
                Console.WriteLine("Erro na consulta do Pokemon: " + e.Message);
            }
        }

        //acessar a página de categoria
        public async void GoCategoryPokemon(Object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CategoryPokemon());
        }

        //acessar a página todos os pokemons
        public async void GoAllPokemon(Object sender, EventArgs args)
        {
            await Navigation.PushAsync(new AllPokemon());
        }
    }
}
