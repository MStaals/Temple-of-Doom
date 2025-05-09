namespace TempleOfDoom.data.DTO;

public class ItemDto
{
    public string type { get; set; }
    public int x { get; set; }  // Add X and Y coordinates
    public int y { get; set; }
    public string? color { get; set; }  // Add KeyColor property
    public int? damage { get; set; }  // Add Damage property
}