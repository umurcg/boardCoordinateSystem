using System.Collections.Generic;
using UnityEngine;

namespace BoardCoordinateSystem
{
    public static class DirectionUtility
    {
        
        public static List<Direction> AllDirections = new List<Direction>()
        {
            Direction.Up,
            Direction.Down,
            Direction.Left,
            Direction.Right
        };
        
        public static Vector3 ConvertDirectionToVector(Direction pullDirection)
        {
            switch (pullDirection)
            {
                case Direction.Up:
                    return Vector3.forward;
                case Direction.Down:
                    return Vector3.back;
                case Direction.Left:
                    return Vector3.left;
                case Direction.Right:
                    return Vector3.right;
                default:
                    return Vector3.zero;
            }
        
        }
        
        /// <summary>
        /// Converts a vector to a direction with tolerance
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public static Direction ConvertRotationToDirection(Quaternion direction,float tolerance=.005f)
        {
            var euler = direction.eulerAngles;
            var y=euler.y;
            if (y > 360 - tolerance || y < 0 + tolerance)
            {
                return Direction.Up;
            }
            else if (y > 90 - tolerance && y < 90 + tolerance)
            {
                return Direction.Right;
            }
            else if (y > 180 - tolerance && y < 180 + tolerance)
            {
                return Direction.Down;
            }
            else if (y > 270 - tolerance && y < 270 + tolerance)
            {
                return Direction.Left;
            }
            else
            {
                return Direction.Zero;
            }
        }
            
        public static Direction ReverseDirection(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    return Direction.Down;
                case Direction.Down:
                    return Direction.Up;
                case Direction.Left:
                    return Direction.Right;
                case Direction.Right:
                    return Direction.Left;
                default:
                    return Direction.Zero;
            }
        }

    }
}