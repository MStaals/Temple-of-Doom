using System.Drawing;
using TempleOfDoom.data.Enums;

namespace TempleOfDoom.data.Models.Items;

public class Key : Item
{
    public string Color { get; set; }

    public Key(int x, int y, string keyColor) : base(x, y, "key")
    {
        Color = ((ColorEnum)Enum.Parse(typeof(ColorEnum), keyColor, true)).ToString().ToUpper();
    }
}