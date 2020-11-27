using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Pokedex.Model;
using Xamarin.Forms;

namespace Pokedex.ViewModel
{
    public class CategoryPokemonVM
    {
        string url;

        //Com a utilização do MVVM, todas as List foram substituidas por ObservableCollection
        //A ObservableCollection fornece notificações quando os itens são adicionados, removidos, ou quando toda a coleção é atualizada.
        private ObservableCollection<Results> _listPokemon;
        public ObservableCollection<Results> ListPokemon
        {
            get { return _listPokemon; }
            set
            {
                _listPokemon = value;
            }
        }

        public CategoryPokemonVM()
        {
            ListPokemon = new ObservableCollection<Results>();
        }

        public void SelectedCategoryPokemon(string pokemonName)
        {
            //montando a URL com a categoria selecionada
            url = string.Format($"https://pokeapi.co/api/v2/type/{pokemonName}");

            //chamando o método pra popular a lista
            FeedList();
        }

        //metodo que alimenta a lista
        public void FeedList()
        {
            //limpo os pokemons que já estavam na lista
            ListPokemon.Clear();
            //metodo
            GetAllPokemons();
        }

        //método responsável por encaminhar o nome do pokemon para a SearchPokemon.
        public async void ItemSelected(string pokemonName)
        {
            //passando (enviando) a mensagem do tipo string que é o nome do pokemon, e a mensagem para a outra tela receber.
            MessagingCenter.Send<string>(pokemonName, "Pokemon");
            await App.Current.MainPage.Navigation.PopAsync();
        }

        //requisição para buscar todos os pokemons 
        public void GetAllPokemons()
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    //deserializa o json e monta o objeto para popular lista.
                    string content = httpClient.GetStringAsync(url).Result;
                    var returnConsult = JsonConvert.DeserializeObject<PokemonCategoryModel>(content);

                    //linq para fazer a consulta
                    foreach (var item in returnConsult.pokemon.Select(x => x.pokemon).ToList())
                    {
                        ListPokemon.Add(item);
                    }
                }

                catch (Exception exc)
                {
                    Console.WriteLine(exc.Message);
                }
            }
        }
    }
}
