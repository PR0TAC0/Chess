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
            if (!(move is Castle))
            {
                this.x = move.x;
                this.y = move.y;
            }
            else
            {
                Castle castle = (Castle) move;
                Rook rook = castle.rook;
                King king = castle.king;

                int rookX = rook.coord.x;
                int y = rook.coord.y;
                int kingX = king.coord.x;;

                if (castle.rook.coord.x == 7)
                {
                    rook.SetCoord(new Coord(rookX - 2,  y));
                    king.SetCoord(new Coord(kingX + 2 , y));
                }
                else // long castle
                {
                    rook.SetCoord(new Coord(rookX + 2, y));
                    king.SetCoord(new Coord(kingX - 3, y));
                }    
            }
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
