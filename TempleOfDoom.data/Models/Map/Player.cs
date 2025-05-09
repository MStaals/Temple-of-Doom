using System.Numerics;
using TempleOfDoom.data.Models.Door;
using TempleOfDoom.data.Models.FloorTiles;
using TempleOfDoom.data.Models.Items;
using TempleOfDoom.data.Models.Portals;

namespace TempleOfDoom.data.Models.Map;

public class Player : UiObserver
{
    public int Lives { get; set; }
    public bool HasWon { get; set; }
    public Position Position { get; set; }
    public Inventory Inventory { get; set; }
    public int StartingRoomId { get; set; }
    public Room currentRoom { get; set; }

    public Player(int lives, Position position)
    {
        Lives = lives;
        HasWon = false;
        Inventory = new Inventory();
        Position = position;
    }

    public bool HasKey(string keyColor)
    {
        return Inventory.HasKey(keyColor);
    }

    public Position GetPlayerStartPosition()
    {
        return Position;
    }

    public void Move(string command, List<Room> rooms)
    {
        // Bereken de nieuwe positie
        var newPosition = new Position
        (
            command switch
            {
                "up" => Position.X,
                "down" => Position.X,
                "left" => Position.X - 1,
                "right" => Position.X + 1,
                _ => Position.X
            },
            command switch
            {
                "up" => Position.Y - 1,
                "down" => Position.Y + 1,
                "left" => Position.Y,
                "right" => Position.Y,
                _ => Position.Y
            }
        );

        //**Controleer of er een vijand op de nieuwe positie staat (botsing vóór beweging)**
        foreach (var enemy in currentRoom.Enemies)
        {
            if (newPosition.X == enemy.Position.X && newPosition.Y == enemy.Position.Y)
            {
                Lives--;  // Verminder het aantal levens van de speler
                Update("You got hit by an enemy!");
                return; // Stop de beweging (blijf op dezelfde plek)
            }
        }

        // Controleer of de nieuwe positie binnen de kamergrenzen ligt
        if (currentRoom.IsPositionWalkable(newPosition))
        {
            Position = newPosition;
        }

        // 🔹 Check of de speler op een Conveyor Belt staat en voer het effect direct uit
        var currentTile = currentRoom.FloorTiles.FirstOrDefault(tile =>
            tile.position.X == Position.X && tile.position.Y == Position.Y);

        if (currentTile is Conveyorbelt conveyorBelt)
        {
            conveyorBelt.Effect(this, currentRoom);
        }

        // 🔹 Nu pas interacties afhandelen (items, deuren, etc.)
        currentRoom.HandlePlayerInteraction(this);

        currentRoom.MoveEnemies(currentRoom);

        // Controleer nogmaals of er een vijand op de speler staat (botsing na beweging)
        foreach (var enemy in currentRoom.Enemies)
        {
            if (Position.X == enemy.Position.X && Position.Y == enemy.Position.Y)
            {
                Lives--;  // Verminder nogmaals levens als de speler eindigt op een vijand
                Update("You got hit by an enemy!");

                // Verwijder de vijand als hij dood is
                if (enemy.IsDead())
                {
                    currentRoom.Enemies.Remove(enemy);
                    Console.WriteLine("Enemy defeated!");
                }
            }
        }

        // Controleer of de speler een deur heeft bereikt
        foreach (var door in currentRoom.Doors)
        {
            if (door.Position.X == Position.X && door.Position.Y == Position.Y)
            {
                // Controleer of de deur geopend kan worden
                if (!door.CanOpen(this))
                {
                    return;
                }

                var targetRoom = rooms.FirstOrDefault(r => r.Id == door.TargetRoomId);

                if (door is ClosingGate gate)
                {
                    gate.SetClosed();
                    foreach (var newDoor in targetRoom.Doors.OfType<ClosingGate>())
                    {
                        newDoor.SetClosed();
                    }
                }



                if (currentRoom.Doors.OfType<ToggleDoor>().Any(d => d.IsOpen))
                {
                    targetRoom.Doors.OfType<ToggleDoor>().ToList().ForEach(d => d.Toggle());
                }

                // Vind de nieuwe kamer
                if (targetRoom == null)
                {
                    Update($"Error: Target room with ID={door.TargetRoomId} not found!");
                    return;
                }

                // Vind de corresponderende deur in de nieuwe kamer
                var correspondingDoor = targetRoom.Doors.FirstOrDefault(d =>
                    d.TargetRoomId == currentRoom.Id && d.Direction == GetOppositeDirection(door.Direction));

                if (correspondingDoor == null)
                {
                    Update($"Error: Corresponding door not found in Room ID={targetRoom.Id} for Direction={GetOppositeDirection(door.Direction)}.");
                    return;
                }

                // Teleporteer de speler naar de nieuwe kamer
                Position = correspondingDoor.Position;
                currentRoom = targetRoom;

                Update($"Teleported to Room ID={currentRoom.Id} at Position=({Position.X}, {Position.Y})");
                break;

            }
        }
        // Controleer of de speler op een portaal staat
        foreach (var portal in currentRoom.Portals)
        {
            if (portal.Position.X == Position.X && portal.Position.Y == Position.Y)
            {
                TeleportToRoom(portal, rooms);
                break;
            }
        }
    }

    private void TeleportToRoom(Portal portal, List<Room> rooms)
    {
        var targetRoom = rooms.FirstOrDefault(r => r.Id == portal.RoomId);

        if (targetRoom == null)
        {
            Update($"Error: Target room with ID={portal.RoomId} not found!");
            return;
        }

        // Vind het corresponderende portaal in de nieuwe kamer
        var correspondingPortal = targetRoom.Portals.FirstOrDefault(p => p.RoomId == currentRoom.Id);

        if (correspondingPortal == null)
        {
            Update($"Error: Corresponding portal not found in Room ID={targetRoom.Id}.");
            return;
        }
        // Teleporteer de speler naar de nieuwe kamer en positie
        Position = correspondingPortal.Position;
        currentRoom = targetRoom;

        Update($"Teleported to Room ID={currentRoom.Id} at Position=({Position.X}, {Position.Y})");
    }

    public void Shoot(Room currentRoom)
    {
        // Bepaal de aangrenzende posities rondom de speler
        var adjacentPositions = new List<Position>
    {
        new Position(Position.X, Position.Y - 1), // Noord
        new Position(Position.X, Position.Y + 1), // Zuid
        new Position(Position.X - 1, Position.Y), // West
        new Position(Position.X + 1, Position.Y)  // Oost
    };

        // Loop door de aangrenzende posities en controleer op vijanden
        foreach (var position in adjacentPositions)
        {
            // Zoek naar vijand op de huidige positie
            var enemy = currentRoom.Enemies
                .FirstOrDefault(e => e.Position.X == position.X && e.Position.Y == position.Y);

            // Als er een vijand is, breng schade toe
            if (enemy != null)
            {
                enemy.TakeDamage();
                Console.WriteLine($"Hit enemy at position ({position.X}, {position.Y})!");

                // Als de vijand dood is, verwijder deze
                if (enemy.IsDead())
                {
                    currentRoom.Enemies.Remove(enemy);
                    Console.WriteLine("Enemy defeated!");
                }
            }
        }
    }

    public int GetItemCount<T>() where T : Item
    {
        return Inventory.GetItemCount<T>();
    }
    

    //Check if the player has collected all required Sankara Stones
    public bool CheckWinCondition(int requiredStones = 5)
    {
        int sankaraStones = Inventory.GetItemCount<SankaraStone>();

        if (sankaraStones >= requiredStones)
        {
            HasWon = true;
            Update("");
            Update($"You have collected all {requiredStones} Sankara Stones! You win!");
            return true;
        }

        return false;
    }

    private Direction GetOppositeDirection(Direction direction)
    {
        return direction switch
        {
            Direction.NORTH => Direction.SOUTH,
            Direction.SOUTH => Direction.NORTH,
            Direction.EAST => Direction.WEST,
            Direction.WEST => Direction.EAST,
            _ => throw new Exception("Invalid direction")
        };
    }
}