using TempleOfDoom.data.Models.Items;

namespace TempleOfDoom.data.Models;

public class Inventory
{
    private readonly List<dynamic> _items = new();

    public void AddItem(Item item)
    {
        _items.Add(item);
    }

    public bool HasKey(String color)
    {
        return _items.OfType<Key>().Any(key => key.Color == color);
    }

    public int GetItemCount<T>() where T : Item
    {
        return _items.OfType<T>().Count();
    }
}
