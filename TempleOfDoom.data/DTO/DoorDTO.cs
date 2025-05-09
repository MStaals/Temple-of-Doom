using TempleOfDoom.data.Models.Map;

namespace TempleOfDoom.data.DTO;

public class DoorDto
{
    public string type { get; set; }
    public string? color { get; set; }
    public int? no_of_stones { get; set; }
}