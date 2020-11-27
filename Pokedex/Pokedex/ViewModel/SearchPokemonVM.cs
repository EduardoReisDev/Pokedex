using System;
using Pokedex.Services;
using Refit;
using System.ComponentModel;
using Pokedex.View;
using Xamarin.Forms;
using Pokedex.ViewModel;

namespace Pokedex.ViewModel
{
    public class SearchPokemonVM : HelperViewModel
    {
        //public event PropertyChangedEventHandler PropertyChanged;

        public SearchPokemonVM()
        {
            //chamando o método FilterPokemonByName ao clicar no botão de busca
            //button_pokemon.Clicked += SearchPokemonAPI;

            GoCategoryPokemon = new Command(OpenCategoryPokemon);
            GoAllPokemon = new Command(OpenAllPokemon);
            SearchPokemon = new Command(FilterPokemonAction);

            //messagingCenter esperando receber a mensagem com o mesmo nome.
            //quando receber, aciona o método FilterPokemonByName.
            MessagingCenter.Subscribe<string>(this, "Pokemon", (sender) =>
            {
                FilterPokemonByName(sender);
            });
        }

        private string _idPokemon;
        public string IdPokemon
        {
            get { return _idPokemon; }
            set { SetProperty(ref _idPokemon, value); }
        }

        private string _namePokemon;
        public string NamePokemon
        {
            get { return _namePokemon; }
            set { SetProperty(ref _namePokemon, value); }
        }

        private string _heightPokemon;
        public string HeightPokemon
        {
            get { return _heightPokemon; }
            set { SetProperty(ref _heightPokemon, value); }
        }

        private string _weightPokemon;
        public string WeightPokemon
        {
            get { return _weightPokemon; }
            set { SetProperty(ref _weightPokemon, value); }
        }

        private string _experiencePokemon;
        public string ExperiencePokemon
        {
            get { return _experiencePokemon; }
            set { SetProperty(ref _experiencePokemon, value); }
        }

        private string _image;
        public string Image
        {
            get { return _image; }
            set { SetProperty(ref _image, value); }
        }

        private string _filterPokemon;
        public string FilterPokemon
        {
            get { return _filterPokemon; }
            set { SetProperty(ref _filterPokemon, value); }
        }

        public Command GoCategoryPokemon { get; }
        public Command GoAllPokemon { get; }
        public Command SearchPokemon { get; }

        //acessar a página de categoria
        public async void OpenCategoryPokemon()
        {
            await App.Current.MainPage.Navigation.PushAsync(new CategoryPokemon());
        }

        //acessar a página todos os pokemons
        public async void OpenAllPokemon()
        {
            //await Navigation.PushAsync(new AllPokemon());
            await App.Current.MainPage.Navigation.PushAsync(new AllPokemon());
        }

        private void FilterPokemonAction()
        {
            FilterPokemonByName();
        }

        private async void FilterPokemonByName(string nomePokemon = null)
        {
            //string pokemon recebe oque o usuário digita
            //eu uso o ToLower para deixar todas as letras minúsculas.
            //caso o pokemon seja digitado com a letra maiúscula, a busca não é feita com sucesso.

            if (string.IsNullOrEmpty(FilterPokemon))
                FilterPokemon = "";

            String pokemon = FilterPokemon.Trim().ToLower();

            if (!string.IsNullOrEmpty(nomePokemon))
                pokemon = nomePokemon;

            try
            {
                //apiCliente recebe a url
                var apiClient = RestService.For<IPokemonAPI>(BasePathAPI.pathUrl);
                //pokemonnome recebe a url com o nome do pokemon
                var pokemonname = await apiClient.GetPokemonAsync(pokemon);

                //operações feitas para apresentar a altura em metros e o peso em kilos
                float resultHeight = (float)pokemonname.Height / (float)10;
                float resultWeight = (float)pokemonname.Weight / (float)10;

                //apresentação dos dados na view
                IdPokemon = string.Format($"ID: {pokemonname.Id}");
                NamePokemon = string.Format($"Nome: {pokemonname.Name}");
                HeightPokemon = string.Format($"Altura: {resultHeight} m");
                WeightPokemon = string.Format($"Peso: {resultWeight} kg");
                ExperiencePokemon = string.Format($"Experiência Base: {pokemonname.Base_experience}");
                Image = string.Format($"https://pokeres.bastionbot.org/images/pokemon/{pokemonname.Id}.png");
            }

            catch (Exception e)
            {
                Console.WriteLine("Erro na consulta do Pokemon: " + e.Message);
            }
        }
    }
}
