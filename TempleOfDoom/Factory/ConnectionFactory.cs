using TempleOfDoom.data.DTO;
using TempleOfDoom.data.Enums;
using TempleOfDoom.data.Models.Door;
using TempleOfDoom.data.Models.Map;
using TempleOfDoom.data.Models.Portals;

namespace TempleOfDoom.Factory
{
    public static class ConnectionFactory
    {
        /// <summary>
        /// Creëert deuren tussen kamers op basis van verbindingen.
        /// </summary>
        public static void CreateRoomDoors(List<Room> rooms, List<ConnectionDto> connections)
        {
            foreach (var connection in connections)
            {
                foreach (var direction in new[] { Direction.NORTH, Direction.SOUTH, Direction.EAST, Direction.WEST })
                {
                    var targetRoomId = direction switch
                    {
                        Direction.NORTH => connection.north,
                        Direction.SOUTH => connection.south,
                        Direction.EAST => connection.east,
                        Direction.WEST => connection.west,
                        _ => null
                    };

                    if (targetRoomId.HasValue)
                        AddConnection(rooms, connection, direction);
                }
            }
        }

        /// <summary>
        /// Creëert portalen tussen kamers op basis van verbindingen.
        /// </summary>
        public static void CreateRoomPortals(List<Room> rooms, List<ConnectionDto> connections)
        {
            foreach (var connection in connections)
            {
                if (connection.Portals == null || connection.Portals.Count != 2)
                    continue; // Zorg ervoor dat een portal altijd een paar heeft

                var portalA = connection.Portals[0];
                var portalB = connection.Portals[1];

                var roomA = rooms.FirstOrDefault(r => r.Id == portalA.roomId);
                var roomB = rooms.FirstOrDefault(r => r.Id == portalB.roomId);

                if (roomA != null && roomB != null)
                {
                    var newPortalA = new Portal(new Position(portalA.x, portalA.y), roomB.Id);
                    var newPortalB = new Portal(new Position(portalB.x, portalB.y), roomA.Id);

                    roomA.Portals.Add(newPortalA);
                    roomB.Portals.Add(newPortalB);
                }
            }
        }

        /// <summary>
        /// Voegt een verbinding tussen kamers toe, inclusief deuren indien opgegeven.
        /// </summary>
        private static void AddConnection(List<Room> rooms, ConnectionDto connection, Direction direction)
        {
            var (currentRoomId, targetRoomId) = direction switch
            {
                Direction.NORTH => (connection.south, connection.north),
                Direction.SOUTH => (connection.north, connection.south),
                Direction.WEST => (connection.east, connection.west),
                Direction.EAST => (connection.west, connection.east),
                _ => (null, null)
            };

            if (currentRoomId.HasValue && targetRoomId.HasValue)
            {
                var currentRoom = rooms.FirstOrDefault(r => r.Id == currentRoomId.Value);
                var targetRoom = rooms.FirstOrDefault(r => r.Id == targetRoomId.Value);

                if (currentRoom != null && targetRoom != null)
                {
                    // Controleer of er geen deuren zijn opgegeven, en voeg een open doorgang toe
                    if (connection.Doors == null || !connection.Doors.Any())
                    {
                        DoorFactory.AddDoor(currentRoom, targetRoom, direction, new List<DoorDto>
                        {
                            new DoorDto { type = "open" }
                        });
                    }
                    else
                    {
                        DoorFactory.AddDoor(currentRoom, targetRoom, direction, connection.Doors);
                    }
                }
            }
        }
    }
}
