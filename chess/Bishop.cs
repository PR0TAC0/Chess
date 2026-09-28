using System;
using System.Collections.Generic;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace chess
{
    public class Bishop: Piece
    {
        public List<Move> GetMovesForDirection(Piece[,] boardPieces, bool right, bool up)
        {
            Func<int, bool> xFunction = Program.negativeFunction;
            Func<int, bool> yFunction = Program.negativeFunction;

            int xAdder = -1;
            int yAdder = -1;

            if (right)
            {
                xAdder = 1;
                xFunction = Program.positiveFunction;
            }

            if (up)
            {
                yAdder = 1;
                yFunction = Program.positiveFunction;
            }

            List<Move> moves = new List<Move>();

            if (xFunction(coord.x + xAdder) && yFunction(coord.y + yAdder))
            {
                for (int i = 1; xFunction(coord.x + (i * xAdder)) && yFunction(coord.y + (i * yAdder)); i ++)
                {
                    int xMove = i * xAdder;
                    int yMove = i * yAdder;
                    
                    Move move = new Move(xMove, yMove);
                    Piece piece = boardPieces[coord.x + xMove, coord.y + yMove];

                    if (piece == null)
                    {
                        moves.Add(move);
                    }
                    else
                    {
                        if (piece.white != white)
                        {
                            moves.Add(move);
                        }

                        break;
                    }
                }
            }

            return moves;
        }

        public override List<Move> GetMoves(Board board)
        {
            List<Move> moves = new List<Move>();

            Piece[,] boardPieces = Program.GenerateBoard(board);

            moves.AddRange(GetMovesForDirection(boardPieces, true , true));
            moves.AddRange(GetMovesForDirection(boardPieces, true , false));
            moves.AddRange(GetMovesForDirection(boardPieces, false, true));
            moves.AddRange(GetMovesForDirection(boardPieces, false, false));

            return moves;
        }

        public Bishop(int x, int y, bool white):
        base(white, x, y)
        {
            piece = Program.Epiece.bishop;
        }
    }
}
