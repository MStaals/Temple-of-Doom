using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.data.Models.Map;

namespace TempleOfDoom.Data
{
    public class JsonDataReader : IGameWorldReader
    {
        public GameWorld LoadGameWorld(string path)
        {
            return JsonFileReader.LoadGameWorld(path);
        }
    }
}
