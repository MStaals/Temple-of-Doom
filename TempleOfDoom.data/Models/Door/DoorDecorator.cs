using TempleOfDoom.data.Models.Map;

namespace TempleOfDoom.data.Models.Door;

public abstract class DoorDecorator : Door
{
    protected Door WrappedDoor;

    protected DoorDecorator(Door door) : base(door.TargetRoomId, door.Direction, door.Position)
    {
        WrappedDoor = door;
    }

    public override bool CanOpen(Player player)
    {
        return WrappedDoor.CanOpen(player);
    }
}
