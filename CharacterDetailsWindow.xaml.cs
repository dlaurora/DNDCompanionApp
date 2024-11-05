using System.Windows;
using DNDCompanionApp.Models;

namespace DNDCompanionApp.Views
{
    public partial class CharacterDetailsWindow : Window
    {
        public CharacterDetailsWindow(CharacterSheet character)
        {
            InitializeComponent();
            DataContext = character;
        }
    }
}
