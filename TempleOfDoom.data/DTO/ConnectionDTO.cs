namespace TempleOfDoom.data.DTO;

public class ConnectionDto
{
    public int? north { get; set; }
    public int? south { get; set; }
    public int? east { get; set; }
    public int? west { get; set; }
    public List<DoorDto> Doors { get; set; }
    public List<PortalDTO> Portals { get; set; }
}
