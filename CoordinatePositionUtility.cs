using System;
using UnityEngine;

namespace BoardCoordinateSystem
{
    [Serializable]
    public class CoordinatePositionUtility
    {
        public Vector2 cellSize;
        public Direction rowStartDirection = Direction.Up;
        public Direction columnStartDirection = Direction.Left;
        public Plane plane = Plane.XY;

        public Vector3 ConvertCoordinateToPosition(Coordinate coordinate)
        {
            var row = coordinate.row;
            var column = coordinate.column;
            var x = columnStartDirection == Direction.Left ? column * cellSize.x : -column * cellSize.x;
            var y = rowStartDirection == Direction.Up ? row * cellSize.y : -row * cellSize.y;

            Vector3 position = Vector3.zero;
            switch (plane)
            {
                case Plane.XY:
                    position = new Vector3(x, y, 0);
                    break;
                case Plane.XZ:
                    position = new Vector3(x, 0, y);
                    break;
                case Plane.YZ:
                    position = new Vector3(0, x, y);
                    break;
            }

            return position;
        }

        public Coordinate ConvertPositionToCoordinate(Vector3 position)
        {
            float x = 0;
            float y = 0;
            switch (plane)
            {
                case Plane.XY:
                    x = position.x;
                    y = position.y;
                    break;
                case Plane.XZ:
                    x = position.x;
                    y = position.z;
                    break;
                case Plane.YZ:
                    x = position.y;
                    y = position.z;
                    break;
            }

            var row = rowStartDirection == Direction.Up ? y / cellSize.y : -y / cellSize.y;
            var column = columnStartDirection == Direction.Left ? x / cellSize.x : -x / cellSize.x;
            return new Coordinate((int)row, (int)column);
        }
    }
}