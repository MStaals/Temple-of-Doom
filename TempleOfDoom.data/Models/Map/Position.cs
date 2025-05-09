namespace TempleOfDoom.data.Models.Map;

public class Position
{
    public Position(int startX, int startY)
    {
        X = startX;
        Y = startY;
    }

    public int X { get; set; }
    public int Y { get; set; }
}