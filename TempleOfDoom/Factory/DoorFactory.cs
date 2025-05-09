using TempleOfDoom.data.DTO;
using TempleOfDoom.data.Models.Door;
using TempleOfDoom.data.Models.Map;

namespace TempleOfDoom.Factory;

public static class DoorFactory
{
    public static void AddDoor(Room currentRoom, Room targetRoom, Direction direction, List<DoorDto> doors)
    {
        foreach (var doorDto in doors)
        {
            Door door = CreateDoor(doorDto, currentRoom, targetRoom, direction);
            currentRoom.Doors.Add(door);
        }
    }

    private static Door CreateDoor(DoorDto dto, Room room, Room targetRoom, Direction direction)
    {
        var position = CalculateDoorPosition(room, direction);
        Door baseDoor = new SimpleDoor(targetRoom.Id, direction, position);

        return dto.type switch
        {
            "colored" => new ColoredDoor(baseDoor, dto.color.ToUpper(), direction),
            "toggle" => new ToggleDoor(baseDoor),
            "closing gate" => new ClosingGate(baseDoor),
            "open on odd" => new OpenOnOddDoor(baseDoor),
            "open on stones in room" => new OpenOnStonesDoor(baseDoor, dto.no_of_stones),
            "open" => baseDoor
        };
    }

    private static Position CalculateDoorPosition(Room room, Direction direction)
    {
        return direction switch
        {
            Direction.NORTH => new Position(room.Width / 2, 0),
            Direction.SOUTH => new Position(room.Width / 2, room.Height - 1),
            Direction.WEST => new Position(0, room.Height / 2),
            Direction.EAST => new Position(room.Width - 1, room.Height / 2),
            _ => throw new Exception("Invalid direction")
        };
    }
}