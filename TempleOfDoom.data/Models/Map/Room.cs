using TempleOfDoom.data.Enums;
using TempleOfDoom.data.Models.Door;
using TempleOfDoom.data.Models.Enemies;
using TempleOfDoom.data.Models.FloorTiles;
using TempleOfDoom.data.Models.Items;
using TempleOfDoom.data.Models.Portals;

namespace TempleOfDoom.data.Models.Map;

public class Room : UiObserver
{
    public int Id { get; }  // Unique identifier for the room
    public int Width { get; }  // Width of the room
    public int Height { get; }  // Height of the room
    public List<Door.Door> Doors { get; set; }  // List of doors in the room
    public List<Item> Items { get; set; }  // List of items in the room
    public List<FloorTile> FloorTiles { get; set; }  // List of floor tiles in the room
    public List<Enemy> Enemies { get; set; }  // List of enemies in the room
    public List<Portal> Portals { get; set; }  // List of portals in the room
    public char[,] Layout { get; private set; }  // 2D array representing the layout of the room

    // Constructor to initialize the room with basic data
    public Room(int id, int width, int height, List<Door.Door> doors, List<Item> items, List<FloorTile> floorTiles, List<Enemy> enemies, List<Portal> portals)
    {
        Id = id;
        Width = width;
        Height = height;
        Doors = doors;
        Items = items;
        FloorTiles = floorTiles;
        Enemies = enemies;
        Portals = portals;
        Layout = new char[height, width];  // Initialize the layout as a 2D array of chars
    }

    // Check if the specified position is walkable
    public bool IsPositionWalkable(Position position)
    {
        if (Layout == null)
            throw new NullReferenceException("Room layout is not initialized.");

        if (position.Y < 0 || position.Y >= Layout.GetLength(0) ||
            position.X < 0 || position.X >= Layout.GetLength(1))
            return false;  // Out of bounds

        return Layout[position.Y, position.X] != '#';  // If the tile is not a wall
    }

    // Initialize the room layout with walls, doors, items, etc.
    public void InitializeRoomLayout()
    {
        // Step 1: Fill all positions with empty space
        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                Layout[y, x] = ' ';
            }
        }

        // Step 2: Add walls around the edges
        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                if (y == 0 || y == Height - 1 || x == 0 || x == Width - 1)
                {
                    Layout[y, x] = (Layout[y, x] == ' ' || Layout[y, x] == '\0') ? (char)Symbols.WALL : Layout[y, x];
                }
            }
        }

        // Step 3: Add doors to the layout
        foreach (var door in Doors)
        {
            if (door.Position.X >= 0 && door.Position.X < Width && door.Position.Y >= 0 && door.Position.Y < Height)
            {
                Layout[door.Position.Y, door.Position.X] = door.Symbol;  // Place the door symbol in the layout
            }
        }

        // Add portals to the layout
        foreach (var portal in Portals)
        {
            if (portal.Position.X >= 0 && portal.Position.X < Width && portal.Position.Y >= 0 && portal.Position.Y < Height)
            {
                Layout[portal.Position.Y, portal.Position.X] = portal.Symbol;  // Place the portal symbol in the layout
            }
        }

        // Add items to the layout
        foreach (var item in Items)
        {
            if (item.X >= 0 && item.X < Width && item.Y >= 0 && item.Y < Height)
            {
                var itemSymbol = item switch
                {
                    Item keyItem when keyItem.Type == "key" => (char)Symbols.KEY,  // Set key symbol
                    Item sankaraStoneItem when sankaraStoneItem.Type == "sankara stone" => (char)Symbols.SANKARASTONE,  // Set sankara stone symbol
                    Item boobytrapItem when boobytrapItem.Type == "boobytrap" => (char)Symbols.BOOBYTRAP,  // Set boobytrap symbol
                    Item disappearingBoobytrapItem when disappearingBoobytrapItem.Type == "disappearing boobytrap" => (char)Symbols.DISSAPINGBOOBYTRAP,  // Set disappearing boobytrap symbol
                    Item pressurePlateItem when pressurePlateItem.Type == "pressure plate" => (char)Symbols.PRESSUREPLATE,  // Set pressure plate symbol
                    _ => ' '  // Default symbol for unknown item types
                };

                Layout[item.Y, item.X] = itemSymbol;  // Place item symbol in layout
            }
        }

        // Add floor tiles to the layout
        foreach (var floorTile in FloorTiles)
        {
            if (floorTile.position.X >= 0 && floorTile.position.X < Width && floorTile.position.Y >= 0 &&
                floorTile.position.Y < Height)
            {
                Layout[floorTile.position.Y, floorTile.position.X] = floorTile.Symbol;  // Place floor tile symbol in layout
            }
        }
    }

    // Handle player interaction with objects in the room (items, traps, etc.)
    public void HandlePlayerInteraction(Player player)
    {
        if (Layout == null)
            throw new NullReferenceException("Room layout is not initialized.");

        var currentTile = Layout[player.Position.Y, player.Position.X];  // Get the current tile the player is standing on

        var floorTile = FloorTiles.FirstOrDefault(tile =>
        tile.position.X == player.Position.X && tile.position.Y == player.Position.Y);

        if (floorTile is Conveyorbelt conveyorBelt)  // If the player steps on a conveyor belt
        {
            conveyorBelt.Effect(player, this);  // Apply the effect of the conveyor belt
        }

        var item = Items.FirstOrDefault(i => i.X == player.Position.X && i.Y == player.Position.Y);  // Check if there is an item at the player's position

        if (item == null)
        {
            return; // No item to interact with
        }

        // Handle interaction with different types of items
        switch (item)
        {
            case Key:
                player.Inventory.AddItem(item);  // Add key to player's inventory
                Items.Remove(item);  // Remove item from the room
                Layout[player.Position.Y, player.Position.X] = ' ';  // Clear item from layout
                break;

            case SankaraStone:
                player.Inventory.AddItem(item);  // Add Sankara stone to inventory
                Items.Remove(item);
                Layout[player.Position.Y, player.Position.X] = ' ';
                if (player.Inventory.GetItemCount<SankaraStone>() == 5)
                {
                    player.HasWon = true;  // If the player has 5 Sankara stones, they win
                }
                break;

            case Boobytrap boobytrap:
                boobytrap.Trigger(player);  // Trigger boobytrap
                break;

            case DisappearingBoobytrap disappearingBoobytrap:
                disappearingBoobytrap.Activate(player);  // Activate disappearing boobytrap
                Items.Remove(disappearingBoobytrap);
                Layout[player.Position.Y, player.Position.X] = ' ';
                break;

            case PressurePlate pressurePlate:
                pressurePlate.StepOn(this);  // Step on pressure plate
                break;
        }

    }

    // Move enemies in the room and handle their actions
    public void MoveEnemies(Room currentRoom)
    {
        // Create a list to track enemies that need to be removed (dead enemies)
        var deadEnemies = new List<Enemy>();

        foreach (var enemy in Enemies)
        {
            enemy.Move(currentRoom);  // Move each enemy

            // Check if the enemy is dead and add it to the removal list
            if (enemy.IsDead())
            {
                deadEnemies.Add(enemy);
            }
        }

        // Remove the dead enemies from the room
        foreach (var deadEnemy in deadEnemies)
        {
            Enemies.Remove(deadEnemy);
        }
    }
}
