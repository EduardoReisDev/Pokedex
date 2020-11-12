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

                if (pokemonnome == null)
                {
                    info_pokemon.Text = string.Format("Pokemon não encontrado");
                }

                else
                {
                    info_pokemon.Text = string.Format($"Código: {pokemonnome.Id}, Nome: {pokemonnome.Name}, Altura: {pokemonnome.Height}, Peso: { pokemonnome.Weight}, Experiência: {pokemonnome.Base_experience}.");
                }

            }

            catch(Exception e)
            {
                Console.WriteLine("Erro na consulta do Pokemon: " + e.Message);
            }
        }
    }
}
