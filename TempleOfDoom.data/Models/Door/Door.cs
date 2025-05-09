using TempleOfDoom.data.Enums;
using TempleOfDoom.data.Models.Map;

namespace TempleOfDoom.data.Models.Door;

public abstract class Door : UiObserver
{
    public int Id { get; set; }
    public bool IsOpen { get; set; }
    public int TargetRoomId { get; set; }
    public Position Position { get; private set; }
    public Direction Direction { get; private set; }
    public char Symbol { get; protected set; }

    public Door(int targetRoomId, Direction direction, Position position)
    {
        TargetRoomId = targetRoomId;
        Direction = direction;
        Position = position;
        IsOpen = false;
        Symbol = direction switch
        {
            Direction.NORTH => (char)Symbols.HORIZONTALDOOR,
            Direction.SOUTH => (char)Symbols.HORIZONTALDOOR,
            Direction.EAST => (char)Symbols.VERTICALDOOR,
            Direction.WEST => (char)Symbols.VERTICALDOOR,
            _ => '?'
        };
    }
    
    public abstract bool CanOpen(Player player);
}
