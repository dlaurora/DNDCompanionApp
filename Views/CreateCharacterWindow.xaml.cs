using System;
using System.Linq;
using System.Windows;
using DNDCompanionApp.Models;
using DNDCompanionApp.DataAccess;
using System.Xml.Linq;

namespace DNDCompanionApp.Views
{
    public partial class CreateCharacterWindow : Window
    {
        private readonly DatabaseConnection _dbConnection;

        public CreateCharacterWindow()
        {
            InitializeComponent();
            _dbConnection = new DatabaseConnection();
        }

        private void CreateCharacterButton_Click(object sender, RoutedEventArgs e)
        {
            // Validación de Entrada
            if (string.IsNullOrWhiteSpace(CharacterNameTextBox.Text) ||
                string.IsNullOrWhiteSpace(PlayerNameTextBox.Text))
            {
                MessageBox.Show("El Nombre del Personaje y el Nombre del Jugador son obligatorios.");
                return;
            }

            // Parsear Nivel
            if (!int.TryParse(LevelTextBox.Text, out int level))
            {
                MessageBox.Show("El Nivel debe ser un número válido.");
                return;
            }

            // Parsear Atributos
            var attributes = new Attributes();

            attributes.Strength = int.TryParse(StrengthTextBox.Text, out int strength) ? strength : 0;
            attributes.Dexterity = int.TryParse(DexterityTextBox.Text, out int dexterity) ? dexterity : 0;
            attributes.Constitution = int.TryParse(ConstitutionTextBox.Text, out int constitution) ? constitution : 0;
            attributes.Intelligence = int.TryParse(IntelligenceTextBox.Text, out int intelligence) ? intelligence : 0;
            attributes.Wisdom = int.TryParse(WisdomTextBox.Text, out int wisdom) ? wisdom : 0;
            attributes.Charisma = int.TryParse(CharismaTextBox.Text, out int charisma) ? charisma : 0;

            // Crear objeto CharacterSheet
            var character = new CharacterSheet
            {
                CharacterName = CharacterNameTextBox.Text,
                PlayerName = PlayerNameTextBox.Text,
                Class = ClassTextBox.Text,
                Race = RaceTextBox.Text,
                Level = level,
                Attributes = attributes,
                Skills = SkillsTextBox.Text.Split(',').Select(s => s.Trim()).ToList(),
                Equipment = EquipmentTextBox.Text.Split(',').Select(s => s.Trim()).ToList(),
                Backstory = BackstoryTextBox.Text,
                DateCreated = DateTime.Now,
                LastUpdated = DateTime.Now
            };

            try
            {
                // Guardar en la base de datos
                _dbConnection.CharacterSheets.InsertOne(character);
                MessageBox.Show("¡Personaje creado exitosamente!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al guardar el personaje: {ex.Message}");
            }
        }
    }
}
