
namespace TempleOfDoom.data.Models.Map;

public class GameWorld(Player player, List<Room> rooms)
{
    public Player Player { get; } = player;
    public List<Room> Rooms { get; } = rooms;
    public bool IsGameOver => Player?.Lives <= 0 || Player?.HasWon == true;
}