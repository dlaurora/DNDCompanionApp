using MongoDB.Driver;
using DNDCompanionApp.Models;

namespace DNDCompanionApp.DataAccess
{
    public class DatabaseConnection
    {
        private readonly IMongoDatabase _database;

        public DatabaseConnection()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            _database = client.GetDatabase("DNDCharacters");
        }

        public IMongoCollection<CharacterSheet> CharacterSheets
        {
            get { return _database.GetCollection<CharacterSheet>("CharacterSheets"); }
        }
    }
}
