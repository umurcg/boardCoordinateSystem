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
        
        
  private Vector3 CalculateBoardCenterOffset(int rowCount, int columnCount)
    {
        // Calculate the total size of the board
        float boardWidth = (columnCount - 1) * cellSize.x;
        float boardHeight = (rowCount - 1) * cellSize.y;

        // Calculate the offset to the center of the board
        Vector3 centerOffset = Vector3.zero;

        switch (plane)
        {
            case Plane.XY:
                centerOffset = new Vector3(boardWidth / 2f, -boardHeight / 2f, 0f);
                break;
            case Plane.XZ:
                centerOffset = new Vector3(boardWidth / 2f, 0f, -boardHeight / 2f);
                break;
            case Plane.YZ:
                centerOffset = new Vector3(0f, boardHeight / 2f, -boardWidth / 2f);
                break;
        }

        return centerOffset;
    }

    public Vector3 ConvertCoordinateToPosition(Coordinate coordinate, Transform transform, int rowCount, int columnCount)
    {
        // Convert the coordinate to position using the utility class
        Vector3 localPosition = ConvertCoordinateToPosition(coordinate);

        // Calculate the offset to the center of the board
        Vector3 boardCenterOffset = CalculateBoardCenterOffset(rowCount, columnCount);

        // Adjust the local position by subtracting the center offset
        localPosition -= boardCenterOffset;

        // Convert the local position to world coordinates
        Vector3 worldPosition = transform.TransformPoint(localPosition);

        return worldPosition;
    }

    public Coordinate ConvertPositionToCoordinate(Vector3 worldPosition,Transform transform, int rowCount, int columnCount)
    {
        // Convert the world position to local coordinates
        Vector3 localPosition = transform.InverseTransformPoint(worldPosition);

        // Calculate the offset to the center of the board
        Vector3 boardCenterOffset = CalculateBoardCenterOffset(rowCount, columnCount);

        // Adjust the local position by adding the center offset
        localPosition += boardCenterOffset;

        // Convert the local position to coordinate using the utility class
        Coordinate coordinate = ConvertPositionToCoordinate(localPosition);

        return coordinate;
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
