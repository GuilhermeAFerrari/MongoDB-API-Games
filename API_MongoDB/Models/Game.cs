using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace API_MongoDB.Models;

public class Game
{
    public Game(string title, string description, int year)
    {
        Title = title;
        Description = description;
        Year = year;
    }

    [BsonId]
    public ObjectId Id { get; set; }
    [BsonElement]
    public string Title { get; set; }
    [BsonElement]
    public string Description { get; set; }
    [BsonElement]
    public int Year { get; set; }
    [BsonElement]
    public DateTime CreatedAt { get; set; }
}
