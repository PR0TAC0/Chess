using System;
using System.Collections.Generic;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace chess
{
    public class Bishop: Piece
    {
        static List<Move> GetMovesForDirection(Piece[,] boardPieces, bool right, bool up, int x, int y, bool white)
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

            if (xFunction(x + xAdder) && yFunction(y + yAdder))
            {
                for (int i = 1; xFunction(x + (i * xAdder)) && yFunction(y + (i * yAdder)); i ++)
                {
                    int xMove = i * xAdder;
                    int yMove = i * yAdder;
                    
                    Move move = new Move(xMove, yMove);
                    Piece piece = boardPieces[x + xMove, y + yMove];

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

        public static List<Move> GetBishopMoves(Piece[,] boardPieces, int x, int y, bool white)
        {
            List<Move> moves = new List<Move>();
            
            moves.AddRange(GetMovesForDirection(boardPieces, true , true , x, y, white));
            moves.AddRange(GetMovesForDirection(boardPieces, true , false, x, y, white));
            moves.AddRange(GetMovesForDirection(boardPieces, false, true , x, y, white));
            moves.AddRange(GetMovesForDirection(boardPieces, false, false, x, y, white));

            return moves;
        }

        public override List<Move> GetMoves(Board board)
        {
            Piece[,] boardPieces = Program.GenerateBoard(board);

            return GetBishopMoves(boardPieces, coord.x, coord.y, white);   
        }

        public Bishop(int x, int y, bool white):
        base(white, x, y)
        {
            piece = Program.Epiece.bishop;
        }
    }
}
