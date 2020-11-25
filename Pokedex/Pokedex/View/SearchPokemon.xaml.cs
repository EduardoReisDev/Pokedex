using System;
using System.Collections.Generic;
using Pokedex.Services;
using Refit;
using Xamarin.Forms;
using Pokedex.ViewModel;

namespace Pokedex.View
{
    public partial class SearchPokemon : ContentPage
    {
        SearchPokemonVM searchPokemonVM;

        public SearchPokemon()
        {
            InitializeComponent();
            searchPokemonVM = new SearchPokemonVM();
            BindingContext = searchPokemonVM;
        }
    }
}
