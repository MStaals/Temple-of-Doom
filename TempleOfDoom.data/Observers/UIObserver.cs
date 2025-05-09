namespace TempleOfDoom.data.Models;

public class UiObserver
{
    /// <summary>
    /// Method to handle UI updates with the provided message.
    /// </summary>
    /// <param name="message">The message to display in the UI.</param>
    protected void Update(string message)
    {
        Console.WriteLine($"UI Update: {message}");
    }
    
    protected void Clear()
    {
        Console.Clear();
    }
}