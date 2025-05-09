using TempleOfDoom.data.Models.Map;

namespace TempleOfDoom.data.Models.Items;

public class DisappearingBoobytrap : Item
{
    public DisappearingBoobytrap(int x, int y, int? damage)
            : base(x, y, "disappearing boobytrap")  // Call the base constructor with x, y, and type
    {
        Damage = (int)damage;
    }

    public int Damage { get; }

    public void Activate(Player player)
    {
        player.Lives -= Damage;
    }
}