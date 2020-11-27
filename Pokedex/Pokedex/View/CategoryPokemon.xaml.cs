using Pokedex.Model;
using Pokedex.ViewModel;
using Xamarin.Forms;

namespace Pokedex.View
{
    public partial class CategoryPokemon : ContentPage
    {

        CategoryPokemonVM categoryPokemonVM;

        public CategoryPokemon()
        {
            InitializeComponent();
            categoryPokemonVM = new CategoryPokemonVM();
            BindingContext = categoryPokemonVM;
        }

        //receber o item selecionado no picker
        private void category_pokemon_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            var selectedItem = category_pokemon.Items[category_pokemon.SelectedIndex];
            categoryPokemonVM.SelectedCategoryPokemon(selectedItem);
        }

        //método de ação quando o usuário clica em um item da lista.
        //ItemTapped do xaml
        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            //e recebe o item convertido para o obejto results da model.
            //variavel contato recebe o meu results (nome e url).
            var contato = e.Item as Results;

            //chamo a view model e o método ItemSelected passando o nome do pokemon por parametro.
            //contato tem o meu objeto que seria o nome e o url, mas como eu quero só o nome, eu uso .name
            categoryPokemonVM.ItemSelected(contato.name);
        }
    }
}
