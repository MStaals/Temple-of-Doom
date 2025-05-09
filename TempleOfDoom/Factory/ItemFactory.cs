using TempleOfDoom.data.DTO;
using TempleOfDoom.data.Models.Items;

namespace TempleOfDoom.Factory;

public static class ItemFactory
{
    public static Item CreateItem(ItemDto itemDto)
    {
        int x = itemDto.x;
        int y = itemDto.y; 

        return itemDto.type switch
        {
            "key" => new Key(x, y, itemDto.color),
            "sankara stone" => new SankaraStone(x, y),
            "pressure plate" => new PressurePlate(x, y),
            "boobytrap" => new Boobytrap(x, y, 1),
            "disappearing boobytrap" => new DisappearingBoobytrap(x, y, itemDto.damage),
            _ => throw new ArgumentException($"Unknown item type: {itemDto.type}")
        };
    }
}