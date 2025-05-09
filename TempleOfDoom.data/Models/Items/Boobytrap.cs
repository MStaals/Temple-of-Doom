using TempleOfDoom.data.Models.Map;

namespace TempleOfDoom.data.Models.Items;

public class Boobytrap : Item
{
    public Boobytrap(int x, int y, int value)
            : base(x, y, "boobytrap")  // Pass parameters to the base Item constructor
    {
        Damage = value;
    }

    public int Damage { get; set; }

    public void Trigger(Player player)
    {
        player.Lives -= Damage;
    }
}