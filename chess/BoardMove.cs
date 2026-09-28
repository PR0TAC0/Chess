using System;
using System.Collections.Generic;
using System.Text;

namespace chess
{
    public class BoardMove
    {
        Piece p;
        Coord coord;
        public BoardMove(Piece p, Move m)
        {
            this.p = p;
            coord = new Coord(p.coord.x + m.x, p.coord.y + m.y);
        }

        public override string ToString()
        {
            return Program.dict[p.piece] + coord;
        }

        public override bool Equals(object? obj)
        {
           if (obj is BoardMove)
           {
                BoardMove move = (BoardMove) obj;

                return move.p.piece == p.piece && move.coord.Equals(coord);
           }

           return false;
        }
    }
}
