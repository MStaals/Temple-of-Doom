using TempleOfDoom.data.Models.Door;

namespace TempleOfDoom.data.Interfaces;

public interface IDoorStrategy
{
    void OpenDoor(Door door);
}