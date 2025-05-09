using TempleOfDoom.data.Models.Door;
using TempleOfDoom.data.Models.Map;

namespace TempleOfDoom.data.Models.Items;

public class PressurePlate : Item
{
    public PressurePlate(int x, int y)
            : base(x, y, "pressure plate")  // Call the base constructor with required parameters
    {
        IsActive = false;
    }

    public bool IsActive { get; set; }
    public event Action Activated;

    public void StepOn(Room currentRoom)
    {
        foreach (var door in currentRoom.Doors.OfType<ToggleDoor>())
        {
            door.Toggle();
        }
    }
}