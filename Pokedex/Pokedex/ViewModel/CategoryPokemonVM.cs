using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Pokedex.Model;

namespace Pokedex.ViewModel
{
    public class CategoryPokemonVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        string url;

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

        public async void FeedList()
        {
            ListPokemon.Clear();
            GetAllPokemons();
        }

        //requisição para buscar todos os pokemons 
        public async void GetAllPokemons()
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

                    //return returnConsult.pokemon.Select(x => x.pokemon).ToList();
                    //TENTATIVA FOTO POKEMON
                    //var listPokemon = retornoConsulta.pokemon.Select(x => x.pokemon).ToList();
                    //var listPokemonName = listPokemon.Select(x => x.name).ToList();
                    //listPokemonFoto = string.Format($"https://img.pokemondb.net/artwork/{listPokemonName.name}.jpg");
                }

                catch (Exception exc)
                {
                    //await DisplayAlert("Erro", "Ocorreu um erro ao buscar os pokemons: " + exc.Message, "OK");
                    //return new List<Results>();
                }
            }
        }
    }
}
