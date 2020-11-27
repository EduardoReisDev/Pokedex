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
        //public event PropertyChangedEventHandler PropertyChanged;

        //Com a utilização do MVVM, todas as List foram substituidas por ObservableCollection
        //A ObservableCollection fornece notificações quando os itens são adicionados, removidos, ou quando toda a coleção é atualizada.
        public ObservableCollection<Results> ListPokemon { get; set; }

        private bool _isRunLoading;
        public bool IsRunLoading
        {
            get { return _isRunLoading; }
            set { SetProperty(ref _isRunLoading, value); }
        }

        public Command LoadingMore { get; }

        public AllPokemonVM()
        {
            IsRunLoading = false;
            ListPokemon = new ObservableCollection<Results>();
            LoadingMore = new Command(LoadingMorePokemon);
            //diferente da categoria, eu já chama o método aqui pra quando o usuário abrir a página, a consulta já ser feita
            FeedList();
        }

        public void FeedList()
        {
            ListPokemon = GetAllPokemons();
        }

        //método responsável por encaminhar o nome do pokemon para a SearchPokemon.
        public async void ItemSelected(string pokemonName)
        {
            //passando (enviando) a mensagem do tipo string que é o nome do pokemon, e a mensagem para a outra tela receber.
            MessagingCenter.Send<string>(pokemonName, "Pokemon");
            await App.Current.MainPage.Navigation.PopAsync();
        }

        public async void LoadingMorePokemon()
        {
            //activity rodando
            IsRunLoading = true;

            //quantidade que já tem mais 20
            int amountMorePokemon = ListPokemon.Count + 20;

            //lista de pokemon completa
            var listPokemonsFull = new ObservableCollection<Results>();

            //executando em segundo plano
            await Task.Run(() =>
            {
                //buscando todos os pokemons setando a quantidade.
                listPokemonsFull = GetAllPokemons(amountMorePokemon);
                //limpo a lista, como vai buscar os mesmos +20, se não fica duplicado
                ListPokemon.Clear();

                //foreach passando a lista do getallpokemons
                //le todos os itens
                foreach (var pokemon in listPokemonsFull)
                {
                    //adiciona o pokemon na lista
                    ListPokemon.Add(pokemon);
                }
            });

            //activity some
            IsRunLoading = false;
        }

        //requisição para buscar todos os pokemons
        public ObservableCollection<Results> GetAllPokemons(int amountMorePokemon = 20)
        {
            //url com todos os pokemons
            var url = "https://pokeapi.co/api/v2/pokemon/?offset=0&limit=" + amountMorePokemon;

            using (var httpClient = new HttpClient())
            {
                try
                {
                    //requisição para buscar todos os pokemons
                    //passando a url da API junto com a quantidade de pokemons
                    var content = httpClient.GetStringAsync(url).Result;

                    //deserializa o JSON que a API retorna para um objeto
                    var returnConsult = JsonConvert.DeserializeObject<AllPokemonModel>(content);

                    //results é a lista retornada da model(AllPokemonModel)
                    return new ObservableCollection<Results>(returnConsult.results);
                }

                catch (Exception exc)
                {
                    IsRunLoading = false;
                    Console.WriteLine(exc.Message);
                    return new ObservableCollection<Results>();
                }
            }
        }
    }
}
