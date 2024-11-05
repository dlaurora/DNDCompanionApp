using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace DNDCompanionApp.Models
{
    public class CharacterSheet
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string CharacterName { get; set; }
        public string PlayerName { get; set; }
        public string Class { get; set; }
        public string Race { get; set; }
        public int Level { get; set; }
        public Attributes Attributes { get; set; }
        public List<string> Skills { get; set; }
        public List<string> Equipment { get; set; }
        public string Backstory { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
