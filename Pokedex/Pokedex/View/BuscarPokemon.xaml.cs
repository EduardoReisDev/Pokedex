using System;
using System.Collections.Generic;
using Pokedex.Interface;
using Refit;
using Xamarin.Forms;

namespace Pokedex.View
{
    public partial class BuscarPokemon : ContentPage
    {
        public BuscarPokemon()
        {
            InitializeComponent();
            button_pokemon.Clicked += BuscarPokemonAPI;           
        }

        private async void BuscarPokemonAPI(object sender, EventArgs args)
        {
                
            String pokemon = entry_pokemon.Text.Trim();

            try
            {
                var apiClient = RestService.For<IPokemonAPI>("https://pokeapi.co/api/v2");
                var pokemonnome = await apiClient.GetPokemonAsync(pokemon);

                id_pokemon.Text = string.Format($"ID: {pokemonnome.Id}");
                nome_pokemon.Text = string.Format($"Nome: {pokemonnome.Name}");
                altura_pokemon.Text = string.Format($"Altura: {pokemonnome.Height}");
                peso_pokemon.Text = string.Format($"Peso: {pokemonnome.Weight}");
                imagem.Source = string.Format($"https://pokeres.bastionbot.org/images/pokemon/{pokemonnome.Id}.png");
            }

            catch(Exception e)
            {
                Console.WriteLine("Erro na consulta do Pokemon: " + e.Message);
            }
        }

        public async void GoCategoriaPokemon(Object sender, EventArgs args)
        {
            await Navigation.PushAsync(new CategoriaPokemon());
        }

        public async void GoTodosPokemon(Object sender, EventArgs args)
        {
            await Navigation.PushAsync(new TodosPokemon());
        }
    }
}
