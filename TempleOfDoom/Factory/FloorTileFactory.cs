using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.data.DTO;
using TempleOfDoom.data.Models.FloorTiles;
using TempleOfDoom.data.Models.Map;

namespace TempleOfDoom.Factory
{
    public class FloorTileFactory
    {
        public static FloorTile CreateTile(FloorTileDTO floorTileDTO)
        {
            if (floorTileDTO.type.ToLower() == "conveyor belt")
            {
                Direction direction = ParseDirection(floorTileDTO.Direction);
                return new Conveyorbelt(new Position(floorTileDTO.x, floorTileDTO.y), direction);
            }

            throw new ArgumentException($"Unknown floor tile type: {floorTileDTO.type}");

        }
        private static Direction ParseDirection(string direction)
        {
            return direction.ToUpper() switch
            {
                "NORTH" => Direction.NORTH,
                "SOUTH" => Direction.SOUTH,
                "EAST" => Direction.EAST,
                "WEST" => Direction.WEST,
                _ => throw new ArgumentException($"Invalid direction: {direction}")
            };
        }
    }
}
