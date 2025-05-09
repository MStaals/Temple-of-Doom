using TempleOfDoom.data.Enums;
using TempleOfDoom.data.Models.Map;

namespace TempleOfDoom.data.Models.Door;

public class ToggleDoor : DoorDecorator
{
    public ToggleDoor(Door door) : base(door)
    {
        Symbol = (char)Symbols.TOGGLEDOOR;
    }

    public override bool CanOpen(Player player)
    {
        return IsOpen;
    }
    public void Toggle()
    {
        IsOpen = !IsOpen;
    }
}