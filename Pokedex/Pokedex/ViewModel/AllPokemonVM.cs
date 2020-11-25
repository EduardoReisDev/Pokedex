using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Pokedex.Model;
using Xamarin.Forms;

namespace Pokedex.ViewModel
{
    public class AllPokemonVM : HelperViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Results> ListPokemon { get; set; }

        private bool _isRunLoading;
        public bool IsRunLoading
        {
            get { return _isRunLoading; }
            set { SetProperty(ref _isRunLoading, value); }
        }

        private bool _showLoadingMore;
        public bool ShowLoadingMore
        {
            get { return _showLoadingMore; }
            set { SetProperty(ref _showLoadingMore, value); }
        }

        public Command LoadingMore { get; }

        public AllPokemonVM()
        {
            IsRunLoading = false;
            ShowLoadingMore = false;
            ListPokemon = new ObservableCollection<Results>();
            LoadingMore = new Command(LoadingMorePokemon);
            //diferente da categoria, eu já chama o método aqui pra quando o usuário abrir a página, a consulta já ser feita
            FeedList();
        }

        public async void FeedList()
        {
            ListPokemon = await GetAllPokemons();
        }

        public async void LoadingMorePokemon()
        {
            IsRunLoading = true;
            ShowLoadingMore = false;

            int amountMorePokemon = ListPokemon.Count + 20;

            var listPokemonsFull = new ObservableCollection<Results>();

            await Task.Run(async () =>
            {
                listPokemonsFull = await GetAllPokemons(amountMorePokemon);
                ListPokemon.Clear();

                foreach(var pokemon in listPokemonsFull)
                {
                    ListPokemon.Add(pokemon);
                }
            });

            ShowLoadingMore = true;
            IsRunLoading = false;
        }

        //requisição para buscar todos os pokemons
        public async Task<ObservableCollection<Results>> GetAllPokemons(int amountMorePokemon = 20)
        {
            //url com todos os pokemons
            var url = "https://pokeapi.co/api/v2/pokemon/?offset=20&limit="+amountMorePokemon;

            using (var httpClient = new HttpClient())
            {
                try
                {
                    var content = httpClient.GetStringAsync(url).Result;

                    var returnConsult = JsonConvert.DeserializeObject<AllPokemonModel>(content);

                    //results é a lista retornada da model(PokeTodos)
                    return new ObservableCollection<Results>(returnConsult.results);
                }

                catch (Exception exc)
                {
                    IsRunLoading = false;
                    ShowLoadingMore = true;
                    Console.WriteLine(exc.Message);
                    //await DisplayAlert("Erro", "Ocorreu um erro ao buscar os pokemons: " + exc.Message, "OK");
                    return new ObservableCollection<Results>();
                }
            }
        }
    }
}
