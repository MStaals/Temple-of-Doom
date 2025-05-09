using TempleOfDoom.data.Enums;
using TempleOfDoom.data.Models.Items;
using TempleOfDoom.data.Models.Map;

namespace TempleOfDoom.ui;

public class ConsoleView
{
    public void DisplayRoom(Room room, Player player)
    {
        Console.Clear();
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.SetCursorPosition(0, Console.WindowHeight - 2);
        Console.WriteLine($"Player: ({player.Position.X}, {player.Position.Y})");
        Console.WriteLine("Aantal levens: " + player.Lives);
        Console.WriteLine("Aantal Sankara Stones: " + player.Inventory.GetItemCount<SankaraStone>());

        int consoleWidth = Console.WindowWidth;
        int consoleHeight = Console.WindowHeight;
        int startX = (consoleWidth / 2) - (room.Width / 2);
        int startY = (consoleHeight / 2) - (room.Height / 2);

        startX = Math.Max(0, startX);
        startY = Math.Max(0, startY);

        for (var y = 0; y < room.Height; y++)
        {
            for (var x = 0; x < room.Width; x++)
            {
                int cursorX = startX + x;
                int cursorY = startY + y;

                Console.SetCursorPosition(cursorX, cursorY);

                // Check if the player is at this position
                if (player.Position.X == x && player.Position.Y == y)
                {
                    Console.Write((char)Symbols.INDY);
                }
                else
                {
                    if (room.Enemies.Any(e => e.Position.X == x && e.Position.Y == y))
                    {
                        Console.Write((char)Symbols.ENEMY); // Weergave voor vijand
                    }

                    // Check if there's an item at this position
                    var item = room.Items.FirstOrDefault(i => i.X == x && i.Y == y);
                    if (item != null)
                    {
                        switch (item)
                        {
                            case Key keyItem:
                                SetConsoleColor(keyItem.Color); // Stel de kleur in voor de sleutel
                                Console.Write((char)Symbols.KEY);
                                Console.ResetColor(); // Reset de kleur naar standaard na gebruik
                                break;
                            case SankaraStone:
                                Console.Write((char)Symbols.SANKARASTONE);
                                break;
                            case Boobytrap:
                                Console.Write((char)Symbols.BOOBYTRAP);
                                break;
                            case DisappearingBoobytrap:
                                Console.Write((char)Symbols.DISSAPINGBOOBYTRAP);
                                break;
                            case PressurePlate:
                                Console.Write((char)Symbols.PRESSUREPLATE);
                                break;
                            default:
                                Console.Write('?');
                                break;
                        }
                    }
                    else
                    {
                        // Default to the room layout if no item is present
                        switch (room.Layout[y, x])
                        {
                            case (char)Symbols.WALL:
                                SetConsoleColor("Yellow");
                                Console.Write(room.Layout[y, x]);
                                Console.ResetColor();
                                break;
                            case (char)Symbols.REDHORIZONTALDOOR:
                                SetConsoleColor("red");
                                Console.Write(room.Layout[y, x]);
                                Console.ResetColor();
                                break;
                            case (char)Symbols.REDVERTICALDOOR:
                                SetConsoleColor("red");
                                Console.Write(room.Layout[y, x]);
                                Console.ResetColor();
                                break;
                            case (char)Symbols.GREENHORIZONTALDOOR:
                                SetConsoleColor("green");
                                Console.Write(room.Layout[y, x]);
                                Console.ResetColor();
                                break;
                            case (char)Symbols.GREENVERTICALDOOR:
                                SetConsoleColor("green");
                                Console.Write(room.Layout[y, x]);
                                Console.ResetColor();
                                break;
                            default:
                                Console.Write(room.Layout[y, x]);
                                break;
                        }
                    }
                }
            }
        }

        Console.SetCursorPosition(0, consoleHeight-1); 
    }

    public static void SetConsoleColor(string color)
    {
        switch (color.ToLower())
        {
            case "red":
                Console.ForegroundColor = ConsoleColor.Red;
                break;
            case "green":
                Console.ForegroundColor = ConsoleColor.Green;
                break;
            case "blue":
                Console.ForegroundColor = ConsoleColor.Blue;
                break;
            case "yellow":
                Console.ForegroundColor = ConsoleColor.Yellow;
                break;
            case "cyan":
                Console.ForegroundColor = ConsoleColor.Cyan;
                break;
            case "magenta":
                Console.ForegroundColor = ConsoleColor.Magenta;
                break;
            default:
                Console.ForegroundColor = ConsoleColor.White; // Default kleur
                break;
        }
    }

    public static void DisplayGameOver(bool hasWon)
    {
        Console.WriteLine();
        Console.WriteLine(hasWon ? "You have won! " : "Game over! ");
    }

    public string GetPlayerArrowInput()
    {
        var keyInfo = Console.ReadKey(true);

        return keyInfo.Key switch
        {
            ConsoleKey.UpArrow => "up",
            ConsoleKey.DownArrow => "down",
            ConsoleKey.LeftArrow => "left",
            ConsoleKey.RightArrow => "right",
            ConsoleKey.Q => "quit",
            ConsoleKey.Spacebar => "space",
            _ => string.Empty 
        };
    }
}