using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.XPath;

namespace chess
{
    internal class Knight: Piece
    {
        public override List<Move> GetMoves(Board board)
        {
            List<Move> moves = new List<Move>();

            if (coord.x + 2 < 8)
            {
                if (coord.y + 1 < 8)
                {
                    moves.Add(new Move(2, 1));
                }

                if (coord.y - 1 > -1)
                {
                    moves.Add(new Move(2, -1));
                }
            }

            if (coord.x - 2 > -1)
            {
                if (coord.y + 1 < 8)
                {
                    moves.Add(new Move(-2, 1));
                }

                if (coord.y -1 > -1)
                {
                    moves.Add(new Move(-2, -1));
                }
            }

            if (coord.y + 2 < 8)
            {
                if (coord.x + 1 < 8)
                {
                    moves.Add(new Move(1, 2));
                }

                if (coord.x -1 > -1)
                {
                    moves.Add(new Move(-1, 2));
                }
            }

            if (coord.y -2 > -1)
            {
                if (coord.x + 1 < 8)
                {
                    moves.Add(new Move(1, -2));
                }

                if (coord.x - 1 > -1)
                {
                    moves.Add(new Move(-1, -2));
                }
            }

            return moves;
        }

        public Knight(int x, int y, bool white) :
        base
        (
            white,
            x, y
        )
        {
            piece = Program.Epiece.knight;
        }
    }
}
