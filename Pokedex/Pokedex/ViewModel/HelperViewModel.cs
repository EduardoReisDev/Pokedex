using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace Pokedex.ViewModel
{
    //https://hub.packtpub.com/xamarin-how-to-add-a-mvvm-pattern-to-an-app-tutorial/
    //https://julianocustodio.com/2019/07/27/mvvmcoffee/
    //https://bertuzzi.medium.com/o-x-do-xamarin-forms-mvvm-helpers-7b73b821fc34

    //Para que eu não precise implementar a inteface INotifyPropertyChanged em todas as ViewModel, eu crio uma ViewModel
    //já com o INotifyPropertyChanged e apenas referêncio essa ViewModel nas outras ViewModel.

    //No MVVM é necessário a INotifyPropertyChanged para notificar a tela de mudanças que houve na ViewModel.
    public class HelperViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public HelperViewModel()
        {

        }

        //Os dois métodos abaixo foram encontrados no site abaixo.
        //https://www.fabiosilvalima.net/inotifypropertychanged-xamarin-forms/
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //string, objeto, objeto
        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
            {
                return false;
            }

            field = value;
            OnPropertyChanged(propertyName);

            return true;
        }

    }
}
