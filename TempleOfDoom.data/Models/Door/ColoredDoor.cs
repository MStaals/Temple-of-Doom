using TempleOfDoom.data.Enums;
using TempleOfDoom.data.Models.Map;

namespace TempleOfDoom.data.Models.Door;

public class ColoredDoor : DoorDecorator
{
    private readonly string KeyColor;

    public ColoredDoor(Door door, string keyColor, Direction direction) : base(door)
    {
        KeyColor = keyColor;
        if (keyColor == "RED")
        {
            Symbol = direction switch
            {
                Direction.NORTH => (char)Symbols.REDHORIZONTALDOOR,
                Direction.SOUTH => (char)Symbols.REDHORIZONTALDOOR,
                Direction.EAST => (char)Symbols.REDVERTICALDOOR,
                Direction.WEST => (char)Symbols.REDVERTICALDOOR,
            };
        }
        else if (keyColor == "GREEN")
        {
            Symbol = direction switch
            {
                Direction.NORTH => (char)Symbols.GREENHORIZONTALDOOR,
                Direction.SOUTH => (char)Symbols.GREENHORIZONTALDOOR,
                Direction.EAST => (char)Symbols.GREENVERTICALDOOR,
                Direction.WEST => (char)Symbols.GREENVERTICALDOOR,
            };
        }
        else
        {
            Symbol = direction switch
            {
                Direction.NORTH => (char)Symbols.HORIZONTALDOOR,
                Direction.SOUTH => (char)Symbols.HORIZONTALDOOR,
                Direction.EAST => (char)Symbols.VERTICALDOOR,
                Direction.WEST => (char)Symbols.VERTICALDOOR,
            };
        }



    }

    public override bool CanOpen(Player player)
    {
        return player.HasKey(KeyColor);
    }
}