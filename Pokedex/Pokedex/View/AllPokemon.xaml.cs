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
    public partial class AllPokemon : ContentPage
    {
        AllPokemonVM allPokemonVM;

        public AllPokemon()
        {
            InitializeComponent();
            BindingContext = allPokemonVM = new AllPokemonVM();
        }
    }
}