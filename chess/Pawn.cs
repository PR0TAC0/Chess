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

        Move GetMoveForAttack(Piece[,] boardPieces, bool right, ref List<Coord> attacks)
        {
            int yAdder = -1;
            Func<int, bool> yFunction = Program.negativeFunction;

            if (white)
            {
                yAdder = 1;
                yFunction = Program.positiveFunction;
            }

            int xAdder = -1;
            Func<int, bool> xFunction = Program.negativeFunction;

            if (right)
            {
                xAdder = 1;
                xFunction = Program.positiveFunction;
            }

            int newYCoord = coord.y + yAdder;
            int newXCoord = coord.x + xAdder;

            if (yFunction(newYCoord) && xFunction(newXCoord))
            {
                Piece piece = boardPieces[newXCoord, newYCoord];

                Move move = new Move(xAdder, yAdder);
                Coord attack = new Coord(newXCoord, newYCoord);

                if (piece != null)
                {
                    if (piece.white != white)
                    {
                        if (piece.piece == Program.Epiece.king)
                        {
                            attacks.Add(attack);
                        }
                        
                        return move;
                    }
                }
                else
                {
                    attacks.Add(attack);
                }
            }

            return null;
        }

        public override List<Move> GetMoves(Piece[,] boardPieces, ref List<Coord> attacks)
        {
            List<Move> result = new List<Move>();

            if (white)
            {
                if (coord.y < 7 && boardPieces[coord.x, coord.y + 1] == null)
                {
                    result.Add(new Move(0, 1));
                }

                if (coord.y < 6 && !moved && boardPieces[coord.x, coord.y + 2] == null)
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

                if (coord.y > 1 && !moved && boardPieces[coord.x, coord.y - 2] == null)
                {
                    result.Add(new Move(0, -2));
                }
            }

            Move[] attackMoves = new Move[2];

            attackMoves[0] = GetMoveForAttack(boardPieces, true , ref attacks);
            attackMoves[1] = GetMoveForAttack(boardPieces, false, ref attacks);

            foreach (Move attack in attackMoves)
            {
                if (attack != null)
                {
                    result.Add(attack);
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
