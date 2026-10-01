using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.XPath;

namespace chess
{
    internal class Knight: Piece
    {
        Move? GetMoveForDirection(Piece[,] boardPieces, bool horizontal, bool right, bool up)
        {
            int coord1 = coord.y;
            int coord2 = coord.x;

            Func<int, bool> function1 = Program.negativeFunction;
            Func<int, bool> function2 = Program.negativeFunction;

            int adder1 = -1;
            int adder2 = -1;

            if (horizontal)
            {
                coord1 = coord.x;
                coord2 = coord.y;

                if (right)
                {
                    function1 = Program.positiveFunction;
                    adder1 = 1;
                }

                if (up)
                {
                    function2 = Program.positiveFunction;
                    adder2 = 1;
                }
            }
            else // vertical
            {
                coord1 = coord.y;
                coord2 = coord.x;

                if (right)
                {
                    function2 = Program.positiveFunction;
                    adder2 = 1;
                }

                if (up)
                {
                    function1 = Program.positiveFunction;
                    adder1 = 1;
                }
            }

            int delta1 = adder1 * 2;
            int delta2 = adder2;

            int newCoord1 = coord1 + delta1;
            int newCoord2 = coord2 + delta2;

            if (function1(newCoord1) && function2(newCoord2))
            {
                Piece piece;
                Move move;
                
                if (horizontal)
                {
                    piece = boardPieces[newCoord1, newCoord2];
                    move = new Move(delta1, delta2);
                }
                else
                {
                    piece = boardPieces[newCoord2, newCoord1];
                    move = new Move(delta2, delta1);
                }

                if (!(piece != null && piece.white == white))
                {
                    return move;
                }
            }

            return null;
        }


        public override List<Move> GetMoves(Piece[,] boardPieces)
        {
            List<Move> moves = new List<Move>();

            bool[] states = { true, false };

            foreach(bool horizontal in states)
            {
                foreach (bool right in states)
                {
                    foreach (bool up in states)
                    {
                        Move? move = GetMoveForDirection(boardPieces, horizontal, right, up);
                        
                        if (move != null)
                        {
                            moves.Add(move);
                        }
                    }
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
