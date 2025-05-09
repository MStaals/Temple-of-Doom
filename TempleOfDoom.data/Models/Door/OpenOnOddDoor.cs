using TempleOfDoom.data.Models.Map;

namespace TempleOfDoom.data.Models.Door;

public class OpenOnOddDoor : DoorDecorator
{
    public OpenOnOddDoor(Door door) : base(door)
    {
    }

    public override bool CanOpen(Player player)
    {
        return player.Lives % 2 != 0 && base.CanOpen(player);
    }
}