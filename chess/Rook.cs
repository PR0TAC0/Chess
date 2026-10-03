using System;
using System.Collections.Generic;
using System.Text;

namespace chess
{
    public class Rook: Piece
    {
        static List<Move> GetMovesForDirection(Piece[,] boardPieces, bool horizontal, bool forward, int x, int y, bool white, ref List<Coord> attacks)
        {   
            int position = y;

            if (horizontal)
            {
                position = x;
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
                    Coord attack = new Coord(0, position + i);

                    if (horizontal)
                    {
                        move = new Move(i, 0);
                        piece = boardPieces[position + i, 0];
                        attack = new Coord(position + i, 0);
                    }

                    if (piece == null)
                    {
                        moves.Add(move);
                        attacks.Add(attack);
                    }
                    else
                    {
                        if (piece.white != white)
                        {
                            moves.Add(move);

                            if (piece.piece == Program.Epiece.king)
                            {
                                attacks.Add(attack);
                            }
                        }

                        break;
                    }
                }
            }

            return moves;
        }

        public static List<Move> GetRookMoves(Piece[,] boardPieces, int x, int y, bool white, ref List<Coord> attacks)
        {
            List<Move> moves = new List<Move>();

            moves.AddRange(GetMovesForDirection(boardPieces, true , true , x, y, white, ref attacks));
            moves.AddRange(GetMovesForDirection(boardPieces, true , false, x, y, white, ref attacks));
            moves.AddRange(GetMovesForDirection(boardPieces, false, true , x, y, white, ref attacks));
            moves.AddRange(GetMovesForDirection(boardPieces, false, false, x, y, white, ref attacks));

            return moves;
        }
        
        public override List<Move> GetMoves(Piece[,] boardPieces, ref List<Coord> attacks)
        {   
            return GetRookMoves(boardPieces, coord.x, coord.y, white, ref attacks);
        }

        public Rook(int x, int y, bool white):
        base(white, x, y)
        {
            piece = Program.Epiece.rook;
        }
    }
}
