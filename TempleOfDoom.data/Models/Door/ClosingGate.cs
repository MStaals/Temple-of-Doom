using TempleOfDoom.data.Enums;
using TempleOfDoom.data.Models.Map;

namespace TempleOfDoom.data.Models.Door;

public class ClosingGate : DoorDecorator
{
    private bool _canOpen;
    public ClosingGate(Door door) : base(door)
    {
        Symbol = (char)Symbols.CLOSINGGATE;
        _canOpen = true;
    }

    public override bool CanOpen(Player player)
    {
        return _canOpen;
    }
    public void SetClosed()
    {
        _canOpen = false;
    }
}