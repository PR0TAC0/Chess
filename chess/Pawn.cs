using System;
using System.Collections.Generic;
using System.Text;

namespace chess
{
    public class Pawn: Piece
    {
        public bool moved = false;
        
        public override void SetCoord(Coord coord)
        {
            moved = true;
        }

        public override List<Move> GetMoves(Board board)
        {
            List<Move> result = new List<Move>();

            Piece[,] boardPieces = Program.GenerateBoard(board);

            if (white)
            {
                if (coord.y < 7 && boardPieces[coord.x, coord.y + 1] == null)
                {
                    result.Add(new Move(0, 1));
                }

                if (coord.y + 1 < 8)
                {
                    if (coord.x + 1 < 8)
                    {
                        Piece piece = boardPieces[coord.x + 1, coord.y + 1];

                        if (piece != null && !piece.white)
                        {
                            result.Add(new Move(1, 1));
                        }
                    }

                    if (coord.x - 1 > -1)
                    {
                        Piece piece = boardPieces[coord.x - 1, coord.y + 1];

                        if (piece != null && !piece.white)
                        {
                            result.Add(new Move(-1, 1));
                        }
                    }
                }

                if (!moved && boardPieces[coord.x, coord.y + 2] == null)
                {
                    result.Add(new Move(0, 2));
                }
            }
            else
            {
                if (coord.y > 0 && boardPieces[coord.x, coord.y - 1] == null)
                {
                    result.Add(new Move(0, -1));
                }

                if (coord.y - 1 > -1)
                {
                    if (coord.x + 1 < 8)
                    {
                        Piece piece = boardPieces[coord.x + 1, coord.y - 1];

                        if (piece != null && piece.white)
                        {
                            result.Add(new Move(1, -1));
                        }
                    }

                    if (coord.x - 1 > -1)
                    {
                        Piece piece = boardPieces[coord.x - 1, coord.y - 1];

                        if (piece != null && piece.white)
                        {
                            result.Add(new Move(-1, -1));
                        }
                    }
                }

                if (!moved && boardPieces[coord.x, coord.y - 2] == null)
                {
                    result.Add(new Move(0, -2));
                }
            }

            return result;
        }

        public Pawn(int x, int y, bool white) :
        base
        (
            white,
            x, y
        )
        {
            piece = Program.Epiece.pawn;
        }
    }
}
