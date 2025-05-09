using TempleOfDoom.data.Interfaces;
using TempleOfDoom.data.Models.Door;
using TempleOfDoom.data.Models.Map;

namespace TempleOfDoom.data.Models.Items;

public class StandardKeyStrategy : IDoorStrategy
{
    public void OpenDoor(Door.Door door)
    {
        throw new NotImplementedException("OpenDoor not implemented for StandardKeyStrategy");
    }

    public bool CanOpen(Door.Door door, Player player)
    {
        // return door is ColoredDoor coloredDoor && player.HasKey(coloredDoor);
        return false;
    }
}