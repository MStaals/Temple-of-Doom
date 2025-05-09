using System.Data;
using TempleOfDoom.Controllers;
using TempleOfDoom.Data;

namespace TempleOfDoom;

internal class Program
{
    private static void Main(string[] args)
    {
        var useXml = args.Contains("--xml");
        IGameWorldReader dataReader = useXml ? new XmlDataReader() : new JsonDataReader();
        var gameController = new GameController(dataReader);
        gameController.StartGame();
    }
}
