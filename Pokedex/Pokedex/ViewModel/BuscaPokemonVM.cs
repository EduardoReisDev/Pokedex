using System;
using System.ComponentModel;
using Pokedex.Services;
using Refit;

namespace Pokedex.ViewModel
{
    public class BuscaPokemonVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public BuscaPokemonVM()
        {
            
        }
    }
}
