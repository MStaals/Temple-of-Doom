using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.data.Models.Map;

namespace TempleOfDoom.Data
{
    public interface IGameWorldReader
    {
        GameWorld LoadGameWorld(string path);
    }
}
