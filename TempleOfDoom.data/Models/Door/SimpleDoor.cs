using TempleOfDoom.data.Models.Map;
using TempleOfDoom.data.Enums;

namespace TempleOfDoom.data.Models.Door;

public class SimpleDoor : Door
{
    public SimpleDoor(int targetRoomId, Direction direction, Position position)
        : base(targetRoomId, direction, position)
    {
        Symbol = (char)Symbols.NODOOR;
    }
    public override bool CanOpen(Player player)
    {
        return true; 
    }
}
