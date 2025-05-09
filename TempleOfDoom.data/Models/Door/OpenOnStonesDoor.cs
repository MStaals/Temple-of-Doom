using TempleOfDoom.data.Models.Map;

namespace TempleOfDoom.data.Models.Door;

public class OpenOnStonesDoor : DoorDecorator
{
    private readonly int? RequiredStones;

    public OpenOnStonesDoor(Door door, int? requiredStones) : base(door)
    {
        RequiredStones = requiredStones;
    }

    public override bool CanOpen(Player player)
    {
        var stonesInRoom = player.currentRoom.Items.Count(item => item.Type == "sankara stone");
        return stonesInRoom == RequiredStones && base.CanOpen(player);
    }
}