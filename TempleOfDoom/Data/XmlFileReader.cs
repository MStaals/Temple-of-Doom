using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using TempleOfDoom.data.DTO;
using TempleOfDoom.data.Models.Map;
using TempleOfDoom.Factory;

namespace TempleOfDoom.Data
{
    public static class XmlFileReader
    {
        /// <summary>
        /// Laadt de gamewereld vanuit een XML-bestand.
        /// </summary>
        public static GameWorld LoadGameWorld(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"File not found: {filePath}");
            }

            var serializer = new XmlSerializer(typeof(GameWorldXmlData));
            GameWorldXmlData xmlData;

            using (var stream = File.OpenRead(filePath))
            {
                xmlData = (GameWorldXmlData)serializer.Deserialize(stream);
            }

            InitializeConnections(xmlData.Connections);
            InitializeRooms(xmlData.Rooms);

            var rooms = RoomFactory.CreateRooms(xmlData.Rooms, xmlData.Connections);
            var player = CreatePlayer(xmlData.Player);

            // Zet de startkamer van de speler
            player.currentRoom = rooms.FirstOrDefault(r => r.Id == xmlData.Player.StartRoomId);

            return new GameWorld(player, rooms);
        }

        /// <summary>
        /// Initialiseert connecties en zorgt ervoor dat lijsten correct zijn ingesteld.
        /// </summary>
        private static void InitializeConnections(List<ConnectionDto> connections)
        {
            foreach (var connection in connections)
            {
                connection.Doors ??= new List<DoorDto>();
                connection.Portals ??= null; // Mogelijk uitbreidbaar in de toekomst
            }
        }

        /// <summary>
        /// Initialiseert kamers en zorgt ervoor dat lijsten correct zijn ingesteld.
        /// </summary>
        private static void InitializeRooms(List<RoomDto> rooms)
        {
            foreach (var room in rooms)
            {
                room.Items ??= new List<ItemDto>();
                room.FloorTiles ??= new List<FloorTileDTO>();
                room.Enemies ??= new List<EnemyDto>();
            }
        }

        /// <summary>
        /// Creëert een speler op basis van XML-data.
        /// </summary>
        private static Player CreatePlayer(PlayerXmlData playerData)
        {
            return new Player(playerData.Lives, new Position(playerData.StartX, playerData.StartY));
        }
    }

    /// <summary>
    /// Representatie van de gamewereld in XML-formaat.
    /// </summary>
    [XmlRoot("GameWorld")]
    public class GameWorldXmlData
    {
        [XmlElement("Rooms")]
        public List<RoomDto> Rooms { get; set; }

        [XmlElement("Connections")]
        public List<ConnectionDto> Connections { get; set; }

        [XmlElement("Player")]
        public PlayerXmlData Player { get; set; }
    }

    /// <summary>
    /// XML-representatie van de speler.
    /// </summary>
    public class PlayerXmlData
    {
        [XmlElement("Lives")]
        public int Lives { get; set; }

        [XmlElement("StartX")]
        public int StartX { get; set; }

        [XmlElement("StartY")]
        public int StartY { get; set; }

        [XmlElement("StartRoomId")]
        public int StartRoomId { get; set; }
    }
}
