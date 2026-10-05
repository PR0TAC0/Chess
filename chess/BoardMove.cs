using System;
using System.Collections.Generic;
using System.Text;

namespace chess
{
    public class BoardMove
    {
        public Piece p { get; }
        public Coord coord { get; }
        public BoardMove(Piece p, Move m)
        {
            this.p = p;
            coord = new Coord(p.coord.x + m.x, p.coord.y + m.y);
        }

        public BoardMove(Castle castle)
        {
            coord = castle;
        }

        public BoardMove(EnPassant enPassant)
        {
            coord = enPassant;
        }

        public override string ToString()
        {
            if (coord is Castle)
            {
                Castle castle = (Castle) coord;

                return $"C:{Program.dict[Program.Epiece.rook]}{castle.rook.coord}";
            }

            if (coord is EnPassant)
            {
                EnPassant enPassant = (EnPassant) coord;

                int y = 3;

                if(enPassant.attacker.white)
                {
                    y = 5;
                }

                Coord newCoord = new Coord(enPassant.victim.coord.x, y);

                return $"{Program.dict[Program.Epiece.pawn]}{newCoord}";
            }
            
            return Program.dict[p.piece] + coord;
        }

        public override bool Equals(object? obj)
        {
           if (obj is BoardMove)
           {    
                BoardMove move = (BoardMove) obj;

                return move.coord.Equals(coord) && move.p.piece == p.piece;
           }

           return false;
        }
    }
}
