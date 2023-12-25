using System;
using System.Collections.Generic;
using UnityEngine;

namespace BoardCoordinateSystem
{
    [Serializable]
    public struct Coordinate
    {
        public Coordinate(int r, int c)
        {
            R = r;
            C = c;
        }

        public int row
        {
            get { return R; }
            set { R = value; }
        }

        public int column
        {
            get { return C; }
            set { C = value; }
        }

        public int R;
        public int C;


        public Coordinate GetCoordinateAtDirection(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    return new Coordinate(R + 1, C);
                case Direction.Down:
                    return new Coordinate(R - 1, C);
                case Direction.Left:
                    return new Coordinate(R, C - 1);
                case Direction.Right:
                    return new Coordinate(R, C + 1);
   
            }

            return Coordinate.Zero;
        }
        
        
        public Coordinate GetCoordinateAtDirection(Direction direction, int distance)
        {
            Coordinate coordinate = this;
            for (int i = 0; i < distance; i++)
            {
                coordinate = coordinate.GetCoordinateAtDirection(direction);
            }
            return coordinate;
        }

        public Coordinate GetCoordinateAtDirection(Vector2 direction)
        {
            if (direction == Vector2.up)
            {
                return new Coordinate(R + 1, C);
            }
            else if (direction == Vector2.down)
            {
                return new Coordinate(R - 1, C);
            }
            else if (direction == Vector2.left)
            {
                return new Coordinate(R, C - 1);
            }
            else if (direction == Vector2.right)
            {
                return new Coordinate(R, C + 1);
            }
            return Coordinate.Zero;
        }

        public static Coordinate Zero { get; set; }

        //Sum 
        public static Coordinate operator +(Coordinate c1, Coordinate c2)
        {
            return new Coordinate(c1.R + c2.R, c1.C + c2.C);
        }
        
        //Equal comparison
        public static bool operator ==(Coordinate c1, Coordinate c2)
        {
            return c1.R == c2.R && c1.C == c2.C;
        }

        public static bool operator !=(Coordinate c1, Coordinate c2)
        {
            return !(c1 == c2);
        }
        
        

        public static Coordinate operator -(Coordinate c1, Coordinate c2)
        {
            return new Coordinate(c1.R - c2.R, c1.C - c2.C);
        }
        
        public Direction GetDirectionToCoordinate(Coordinate coordinate)
        {
            if (coordinate.R > R)
            {
                return Direction.Up;
            }
            else if (coordinate.R < R)
            {
                return Direction.Down;
            }
            else if (coordinate.C > C)
            {
                return Direction.Right;
            }
            else if (coordinate.C < C)
            {
                return Direction.Left;
            }

            return Direction.Zero;
        }


        public int PerpendicularDistance(Coordinate coordinate)
        {
            int dr = Mathf.Abs(R - coordinate.R);
            int dc = Mathf.Abs(C - coordinate.C);
            return dr + dc;
        }
        
        //Get distance between two coordinates
        //Diagonal distance count as 1
        public int Distance(Coordinate coordinate)
        {
            int dr = Mathf.Abs(R - coordinate.R);
            int dc = Mathf.Abs(C - coordinate.C);
            
            //Diagonal distance count as 1
            return Mathf.Max(dr, dc);
            
        }
        
        /// <summary>
        /// Check distance to another coordinate. Diagonal distance count as 1
        /// </summary>
        /// <param name="coordinate"></param>
        /// <returns></returns>
        public int MagnitudeDistance(Coordinate coordinate)
        {
            int dr = Mathf.Abs(R - coordinate.R);
            int dc = Mathf.Abs(C - coordinate.C);
            return Mathf.Max(dr, dc);
        }
        
        public bool IsAdjacent(Coordinate coordinate)
        {
            return Mathf.Abs(R - coordinate.R) + Mathf.Abs(C - coordinate.C) == 1;
        }

        public Direction[] GetPerpendicularDirections()
        {
            Direction[] directions = new Direction[4];
            directions[0] = Direction.Left;
            directions[1] = Direction.Right;
            directions[2] = Direction.Up;
            directions[3] = Direction.Down;
            return directions;
        }


        public Coordinate[] GetPerpendicularNeighbours()
        {
            Coordinate[] neighbors = new Coordinate[4];
            neighbors[0] = GetCoordinateAtDirection(Direction.Left);
            neighbors[1] = GetCoordinateAtDirection(Direction.Right);
            neighbors[2] = GetCoordinateAtDirection(Direction.Up);
            neighbors[3] = GetCoordinateAtDirection(Direction.Down);
            return neighbors;
        }

        public Coordinate[] GetAllNeighbours()
        {
            Coordinate[] neighbors = new Coordinate[8];
            neighbors[0] = GetCoordinateAtDirection(Direction.Left);
            neighbors[1] = GetCoordinateAtDirection(Direction.Right);
            neighbors[2] = GetCoordinateAtDirection(Direction.Up);
            neighbors[3] = GetCoordinateAtDirection(Direction.Down);

            neighbors[4] = GetCoordinateThroughDirections(new List<Direction>() {Direction.Up, Direction.Left});
            neighbors[5] = GetCoordinateThroughDirections(new List<Direction>() {Direction.Up, Direction.Right});
            neighbors[6] = GetCoordinateThroughDirections(new List<Direction>() {Direction.Down, Direction.Left});
            neighbors[7] = GetCoordinateThroughDirections(new List<Direction>() {Direction.Down, Direction.Right});

            return neighbors;
        }

        public Coordinate GetCoordinateThroughDirections(List<Direction> directions)
        {
            Coordinate coordinate = this;
            foreach (Direction direction in directions)
            {
                coordinate = coordinate.GetCoordinateAtDirection(direction);
            }

            return coordinate;
        }
        
        public Coordinate Clone()
        {
            return new Coordinate(R, C);
        }

        //To string
        public override string ToString()
        {
            return "(" + R + "," + C + ")";
        }
    }
}