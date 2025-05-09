using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.data.DTO;
using TempleOfDoom.data.Models.Enemies;
using TempleOfDoom.data.Models.Map;

namespace TempleOfDoom.Factory
{
    public static class EnemyFactory
    {
        public static Enemy CreateEnemy(EnemyDto enemyDTO)
        {
            var position = new Position(enemyDTO.X, enemyDTO.Y);

            // Zet de string om naar de enum
            if (!Enum.TryParse(enemyDTO.Type, true, out EnemyMovementType movementType))
            {
                throw new ArgumentException($"Invalid enemy type: {enemyDTO.Type}");
            }

            return new Enemy(movementType, position, enemyDTO.MinX, enemyDTO.MaxX, enemyDTO.MinY, enemyDTO.MaxY);
        }
    }
}
