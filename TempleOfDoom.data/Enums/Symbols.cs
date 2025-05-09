namespace TempleOfDoom.data.Enums
{
    public enum Symbols
    {
        WALL = '#',               // Muur
        INDY = 'X',               // Startpositie van Indy
        SANKARASTONE = 'S',       // Sankara Stone
        KEY = 'K',                // Sleutel met betreffende kleur
        VERTICALDOOR = '|',       // Deur (verticaal)
        HORIZONTALDOOR = '=',     // Deur (horizontaal)
        PRESSUREPLATE = 'T',      // Drukplek
        TOGGLEDOOR = '⊥',         // Deur die met drukplek open/dicht kan
        CLOSINGGATE = '∩',        // Deur die dicht gaat na doorgaan
        BOOBYTRAP = 'O',          // Boobytrap die schade doet
        DISSAPINGBOOBYTRAP = '@', // Verdwijnt na schade doen
        NODOOR = ' ',             // geen deur aanwezig
        REDVERTICALDOOR = '$',
        REDHORIZONTALDOOR = '%',
        GREENVERTICALDOOR = '&',
        GREENHORIZONTALDOOR = '*',
        CONVEYORBELTNORTH = '^',
        CONVEYORBELTSOUTH = 'v',
        CONVEYORBELTEAST = '>',
        CONVEYORBELTWEST = '<',
        ENEMY = 'E',
        PORTAL = 'O'
    }
}