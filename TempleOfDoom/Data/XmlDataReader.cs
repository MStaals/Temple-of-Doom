using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.data.Models.Map;

namespace TempleOfDoom.Data
{
    public class XmlDataReader : IGameWorldReader
    {
        public GameWorld LoadGameWorld(string filePath)
        {
            return XmlFileReader.LoadGameWorld(filePath);
        }
    }
}
