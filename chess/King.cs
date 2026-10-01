using System;
using System.Collections.Generic;
using System.Text;

namespace chess
{
    public class King: Piece
    {
        void GetValuesForMove(int direction, ref Func<int, bool> function, ref int adder)
        {
            function = n => true;
            adder = 0;

            switch (direction)
            {
                case -1:
                    function = Program.negativeFunction;
                    adder = -1;

                    break;

                case 1:
                    function = Program.positiveFunction;
                    adder = 1;

                    break;
            }
        }

        Move? GetMoveForDirection(int right, int up, Piece[,] boardPieces)
        {
            int xAdder = 5;
            Func<int, bool> xFunction = n => false;

            GetValuesForMove(right, ref xFunction, ref xAdder);

            int yAdder = 5;
            Func<int, bool> yFunction = n => false;

            GetValuesForMove(up, ref yFunction, ref yAdder);

            int x = coord.x + xAdder;
            int y = coord.y + yAdder;

            if (xFunction(x) && yFunction(y))
            {
                Piece piece = boardPieces[x, y];
                
                if (piece != null)
                {
                    if (piece.white == white)
                    {
                        return null;
                    }
                }
                
                return new Move(xAdder, yAdder);
            }
            else
            {
                return null;
            }
        }
        
        public override List<Move> GetMoves(Piece[,] boardPieces)
        {
            List<Move> moves = new List<Move>();

            for (int i = -1; i < 2; i ++)
            {
                for (int j = -1; j < 2; j ++)
                {
                    if (!(i == 0 && j == 0)) // skip the "move" where the king does nothing
                    {
                        Move move = GetMoveForDirection(i, j, boardPieces);

                        if (move != null)
                        {
                            moves.Add(move);
                        }
                    }
                }
            }

            return moves;
        }
        
        public King(int x, int y, bool white):
        base(white, x, y)
        {
            piece = Program.Epiece.king;
        }
    }
}
