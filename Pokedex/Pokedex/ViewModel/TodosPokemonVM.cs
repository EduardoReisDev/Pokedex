using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Pokedex.Model;
using Pokedex.Services;
using Refit;

namespace Pokedex.ViewModel
{
    public class TodosPokemonVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public TodosPokemonVM()
        {
            //Não foi implementado MVVM
        }
    }
}
