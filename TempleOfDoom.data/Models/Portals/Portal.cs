using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.data.Enums;
using TempleOfDoom.data.Models.Map;

namespace TempleOfDoom.data.Models.Portals
{
    public class Portal
    {
        public Position Position { get; set; }
        public int RoomId { get; set; }
        public int travelTo { get; set; }
        public char Symbol { get; set; } = (char)Symbols.PORTAL;

        public Portal(Position position, int roomId) 
        { 
            RoomId = roomId;
            Position = position;
        }
    }
}
