using System;
using System.Collections.Generic;
using System.Text;

namespace chess
{
    public class Rook: Piece
    {
        public List<Move> GetMovesForDirection(Piece[,] boardPieces, bool horizontal, bool forward)
        {
            int position = coord.y;

            if (horizontal)
            {
                position = coord.x;
            }

            int adder = -1;
            Func<int, bool> function = Program.negativeFunction;

            if (forward)
            {
                adder = 1;
                function = Program.positiveFunction;
            }

            List<Move> moves = new List<Move>();

            if (function(position + adder))
            {
                for (int i = adder; function(position + i); i += adder)
                {
                    Move move = new Move(0, i);
                    Piece piece = boardPieces[0, position + i];
                    
                    if (horizontal)
                    {
                        move = new Move(i, 0);
                        piece = boardPieces[position + i, 0];
                    }

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

        public Rook(int x, int y, bool white):
        base(white, x, y)
        {
            piece = Program.Epiece.rook;
        }
    }
}
