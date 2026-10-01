using System;
using System.Collections.Generic;
using System.Text;

namespace chess
{
    public class Queen: Piece
    {
        public override List<Move> GetMoves(Piece[,] boardPieces)
        {
            List<Move> moves = new List<Move>();

            moves.AddRange(Rook.  GetRookMoves  (boardPieces, coord.x, coord.y, white));
            moves.AddRange(Bishop.GetBishopMoves(boardPieces, coord.x, coord.y, white));

            return moves;
        }
        
        public Queen(int x, int y, bool white):
        base(white, x, y)
        {
            piece = Program.Epiece.queen;
        }
    }
}
