using System;
using System.Collections.Generic;
using System.Text;

namespace chess
{
    public class Coord
    {
        public int x 
        {
            get; 
            set 
            { 
                field = value;

                Check(x);
            } 
        }
        public int y 
        { 
            get; 
            set 
            { 
                field = value;

                Check(y);
            } 
        }

        public Coord(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public void Move(Coord move)
        {
            this.x += move.x;
            this.y += move.y;
        }

        void Check(int value)
        {
            if (value > 7)
            {
                throw new ArgumentException(value.ToString());
            }

            if (value < 0)
            {
                throw new ArgumentException(value.ToString());
            }
        }

        public override bool Equals(object? obj)
        {
            if (obj is Coord)
            {
                Coord coord = (Coord) obj;

                return coord.x == x && coord.y == y;
            }

            return false;
        }

        public override string ToString()
        {
            return $"{((char)(x + 97))}{(y + 1)}";
        }
    }
}
