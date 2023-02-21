using API_MongoDB.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace API_MongoDB.Controllers;

[Route("api/[controller]")]
public class GameController : ControllerBase
{
    private const string DB_NAME = "Games";
    private readonly string COLLECTION_NAME = "Games";
    private readonly IMongoCollection<Game> _games;

    public GameController(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("MongoDb");
        var mongoClient = new MongoClient(connectionString);
        var database = mongoClient.GetDatabase(DB_NAME);
        _games = database.GetCollection<Game>(COLLECTION_NAME);
    }

    [HttpGet]
    public IActionResult GetGames()
    {
        var gamesList = _games.Find(e => true).ToList();
        return Ok(gamesList);
    }

    [HttpGet("{id}")]
    public IActionResult GetGameById(string id)
    {
        var objectId = ObjectId.Parse(id);
        var foundGame = _games.Find(e => e.Id == objectId).SingleOrDefault();
        if (foundGame == null) return NotFound();
        return Ok(foundGame);
    }

    [HttpPost]
    public IActionResult CreateGame([FromBody] Game game)
    {
        var newGame = new Game(game.Title, game.Description, game.Year);
        _games.InsertOne(newGame);
        return CreatedAtAction(nameof(GetGameById), new { id = newGame.Id.ToString() }, newGame);
    }
}
