using System;
using UnityEngine;

namespace BoardCoordinateSystem
{
    [Serializable]
    public class CoordinatePositionUtility
    {
        public Vector2 cellSize;
        public bool rowStartFromTop = true;
        public bool columnStartFromLeft = true;
        public Plane plane = Plane.XY;

        public Vector3 ConvertCoordinateToPosition(Coordinate coordinate)
        {
            var row = coordinate.row;
            var column = coordinate.column;
            var x = columnStartFromLeft ? column * cellSize.x : -column * cellSize.x;
            var y = rowStartFromTop ? -row * cellSize.y : row * cellSize.y;

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
                    position = new Vector3(0, y, x);
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
                    x = position.z;
                    y = position.y;
                    break;
            }

            var row = rowStartFromTop ? Mathf.RoundToInt(-y / cellSize.y) : Mathf.RoundToInt(y / cellSize.y);
            var column = columnStartFromLeft ? Mathf.RoundToInt(x / cellSize.x) : Mathf.RoundToInt(-x / cellSize.x);
            return new Coordinate(row, column);
        }
    }
}