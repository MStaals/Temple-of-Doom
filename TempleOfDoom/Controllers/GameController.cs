using TempleOfDoom.Data;
using TempleOfDoom.data.Models.Map;
using TempleOfDoom.ui;
using System.Data;

namespace TempleOfDoom.Controllers;

public class GameController
{
    private GameWorld? _gameWorld;
    private ConsoleView _view = new();
    private readonly IGameWorldReader _gameWorldReader;

    public GameController(IGameWorldReader gameWorldReader)
    {
        _gameWorldReader = gameWorldReader;
    }

    public void StartGame()
    {
        // Initialize components
        _view = new ConsoleView();

        _gameWorld = _gameWorldReader.LoadGameWorld("../../../../TempleOfDoom.data/Levels/TempleOfDoom_B.json");
        _gameWorld.Player.Position = _gameWorld.Player.GetPlayerStartPosition();

        // Start the game loop
        while (!_gameWorld.IsGameOver)
        {
            _view.DisplayRoom(_gameWorld.Player.currentRoom, _gameWorld.Player);
            var command = _view.GetPlayerArrowInput();
            ProcessCommand(command);
            Console.WriteLine($"Player currentRoom: {_gameWorld.Player.currentRoom.Id}");
            _view.DisplayRoom(_gameWorld.Player.currentRoom, _gameWorld.Player);
        }

        ConsoleView.DisplayGameOver(_gameWorld.Player.HasWon);

    }

    private void ProcessCommand(string command)
    {
        if (string.IsNullOrEmpty(command)) return;

        if (command == "quit")
        {
            Console.WriteLine("Exiting game.");
            Environment.Exit(0);
        }
        else if (command == "space")
        {
            // Schietactie uitvoeren
            _gameWorld.Player.Shoot(_gameWorld.Player.currentRoom);
        }
        else
        {
            _gameWorld.Player.Move(command, _gameWorld.Rooms);
        }
    }
}