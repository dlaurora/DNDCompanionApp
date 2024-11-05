using System.Windows;
using DNDCompanionApp.ViewModels;
using DNDCompanionApp.Views;
using DNDCompanionApp.Models;

namespace DNDCompanionApp
{
    public partial class MainWindow : Window
    {
        private CharacterViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new CharacterViewModel();
            CharactersListBox.ItemsSource = _viewModel.Characters;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            var searchTerm = SearchTextBox.Text;
            var results = _viewModel.SearchCharacters(searchTerm);
            CharactersListBox.ItemsSource = results;
        }

        private void CreateCharacterButton_Click(object sender, RoutedEventArgs e)
        {
            var createWindow = new CreateCharacterWindow();
            createWindow.ShowDialog();
            // Actualizar la lista de personajes
            _viewModel = new CharacterViewModel();
            CharactersListBox.ItemsSource = _viewModel.Characters;
        }

        private void ViewDetailsButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedCharacter = (CharacterSheet)CharactersListBox.SelectedItem;
            if (selectedCharacter != null)
            {
                var detailsWindow = new CharacterDetailsWindow(selectedCharacter);
                detailsWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Por favor, selecciona un personaje.");
            }
        }

        private void DeleteCharacterButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedCharacter = (CharacterSheet)CharactersListBox.SelectedItem;
            if (selectedCharacter != null)
            {
                var result = MessageBox.Show($"¿Estás seguro de que deseas eliminar a {selectedCharacter.CharacterName}?", "Confirmar Eliminación", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    _viewModel.DeleteCharacter(selectedCharacter.Id);
                    // Actualizar la lista de personajes
                    _viewModel = new CharacterViewModel();
                    CharactersListBox.ItemsSource = _viewModel.Characters;
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecciona un personaje.");
            }
        }
    }
}
