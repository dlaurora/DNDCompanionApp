using System.Collections.ObjectModel;
using DNDCompanionApp.Models;
using DNDCompanionApp.DataAccess;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;

namespace DNDCompanionApp.ViewModels
{
    public class CharacterViewModel
    {
        private readonly DatabaseConnection _dbConnection;

        public ObservableCollection<CharacterSheet> Characters { get; set; }

        public CharacterViewModel()
        {
            _dbConnection = new DatabaseConnection();
            Characters = new ObservableCollection<CharacterSheet>(GetAllCharacters());
        }

        public IEnumerable<CharacterSheet> GetAllCharacters()
        {
            return _dbConnection.CharacterSheets.Find(FilterDefinition<CharacterSheet>.Empty).ToList();
        }

        public IEnumerable<CharacterSheet> SearchCharacters(string searchTerm)
        {
            var filter = Builders<CharacterSheet>.Filter.Or(
                Builders<CharacterSheet>.Filter.Regex("CharacterName", new BsonRegularExpression(searchTerm, "i")),
                Builders<CharacterSheet>.Filter.Regex("Class", new BsonRegularExpression(searchTerm, "i")),
                Builders<CharacterSheet>.Filter.Regex("Race", new BsonRegularExpression(searchTerm, "i"))
            );
            return _dbConnection.CharacterSheets.Find(filter).ToList();
        }

        public void DeleteCharacter(ObjectId id)
        {
            var filter = Builders<CharacterSheet>.Filter.Eq("_id", id);
            _dbConnection.CharacterSheets.DeleteOne(filter);
        }
    }
}
