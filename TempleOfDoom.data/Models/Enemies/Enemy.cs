using System;
using TempleOfDoom.data.Models.Map;

namespace TempleOfDoom.data.Models.Enemies
{
    public enum EnemyMovementType
    {
        Horizontal,
        Vertical
    }

    public class Enemy
    {
        public EnemyMovementType MovementType { get; }
        public Position Position { get; private set; }
        public int MinX { get; }
        public int MaxX { get; }
        public int MinY { get; }
        public int MaxY { get; }
        public int Lives { get; private set; }
        private bool MovingForward { get; set; }

        public Enemy(EnemyMovementType movementType, Position position, int minX, int maxX, int minY, int maxY, int lives = 2)
        {
            MovementType = movementType;
            Position = position;
            MinX = minX;
            MaxX = maxX;
            MinY = minY;
            MaxY = maxY;
            Lives = lives;
            MovingForward = true;
        }

        public void Move(Room currentroom)
        {
            int deltaX = 0, deltaY = 0;

            switch (MovementType)
            {
                case EnemyMovementType.Horizontal:
                    deltaX = MovingForward ? 1 : -1;
                    Position = new Position(Math.Clamp(Position.X + deltaX, MinX, MaxX), Position.Y);
                    if (Position.X == MinX || Position.X == MaxX) MovingForward = !MovingForward;
                    break;

                case EnemyMovementType.Vertical:
                    deltaY = MovingForward ? 1 : -1;
                    Position = new Position(Position.X, Math.Clamp(Position.Y + deltaY, MinY, MaxY));
                    if (Position.Y == MinY || Position.Y == MaxY) MovingForward = !MovingForward;
                    break;
            }
        }

        public void TakeDamage()
        {
            if (Lives > 0) Lives--;
        }

        public bool IsDead() => Lives <= 0;
    }
}
