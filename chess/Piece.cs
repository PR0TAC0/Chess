using System;
using System.Collections.Generic;
using System.Text;

namespace chess
{
    public abstract class Piece
    {
        public Coord coord;
        public bool white;
        public Program.Epiece piece;

        public virtual void SetCoord(Coord coord)
        {
           this.coord.Move(coord);
        }

        public abstract List<Move> GetMoves(Piece[,] boardPieces, ref List<Coord> attacks);

        public override string ToString()
        {
            return Program.dict[piece] + coord;    
        }

        public Piece(bool white, int x, int y)
        {
            coord = new Coord(x, y);
            this.white = white;
        }
    }
}
