using System;
using System.Collections.Generic;
using Pokedex.Interface;
using Pokedex.Model;
using Refit;
using Xamarin.Forms;

namespace Pokedex.View
{
    public partial class TodosPokemon : ContentPage
    {
        public TodosPokemon()
        {
            InitializeComponent();
        }

        public async void BuscarPokemonAPI(object sender, EventArgs args)
        {
            try
            {
                string pokemon = "pikachu";
                var apiClient = RestService.For<IPokemonAPI>("https://pokeapi.co/api/v2");
                var pokemonnome = await apiClient.GetPokemonAsync(pokemon);

                //id_pokemon = string.Format($"ID: {pokemonnome.Id}");
                //nome_pokemon.Text = string.Format($"Nome: {pokemonnome.Name}");
                //imagem.Source = string.Format($"https://pokeres.bastionbot.org/images/pokemon/{pokemonnome.Id}.png");
            }

            catch (Exception e)
            {
                Console.WriteLine("Erro na consulta do Pokemon: " + e.Message);
            }
        }
    }
}
