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

        private void category_pokemon_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            var selectedItem = category_pokemon.Items[category_pokemon.SelectedIndex];
            categoryPokemonVM.SelectedCategoryPokemon(selectedItem);
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var contato = e.Item as Results;

            categoryPokemonVM.ItemSelected(contato.name);
        }
    }
}
