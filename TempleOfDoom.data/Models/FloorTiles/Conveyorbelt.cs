using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.data.Models.Map;
using TempleOfDoom.data.Enums;

namespace TempleOfDoom.data.Models.FloorTiles
{
    public class Conveyorbelt : FloorTile
    {
        public Direction Direction { get; }

        public Conveyorbelt(Position position, Direction direction) : base(position)
        {
            Direction = direction;
            Symbol = GetSymbolForDirection(direction); // Zet het juiste symbool in
        }

        private static char GetSymbolForDirection(Direction direction)
        {
            return direction switch
            {
                Direction.NORTH => (char)Symbols.CONVEYORBELTNORTH,
                Direction.SOUTH => (char)Symbols.CONVEYORBELTSOUTH,
                Direction.EAST => (char)Symbols.CONVEYORBELTEAST,
                Direction.WEST => (char)Symbols.CONVEYORBELTWEST,
                _ => throw new ArgumentException("Invalid conveyor belt direction")
            };
        }

        public override void Effect(Player player, Room room)
        {
            Position newPosition = Direction switch
            {
                Direction.NORTH => new Position(player.Position.X, player.Position.Y - 1),
                Direction.SOUTH => new Position(player.Position.X, player.Position.Y + 1),
                Direction.EAST => new Position(player.Position.X + 1, player.Position.Y),
                Direction.WEST => new Position(player.Position.X - 1, player.Position.Y),
                _ => player.Position
            };

            if (room.IsPositionWalkable(newPosition))
            {
                player.Position = newPosition;
            }
        }
    }
}
