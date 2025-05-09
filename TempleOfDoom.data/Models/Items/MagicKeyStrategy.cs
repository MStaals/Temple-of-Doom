using TempleOfDoom.data.Interfaces;
using TempleOfDoom.data.Models.Door;
using TempleOfDoom.data.Models.Map;

namespace TempleOfDoom.data.Models.Items;

public class MagicKeyStrategy : IDoorStrategy
{
    public void OpenDoor(Door.Door door)
    {
        throw new NotImplementedException("OpenDoor not implemented for MagicKeyStrategy");
    }

    public bool CanOpen(Door.Door door, Player player)
    {
        return true; // Magic keys can open any door
    }
}