using System;
using System.Collections.Generic;
using System.Text;

namespace chess
{
    public abstract class Piece
    {
        public Coord coord;
        public bool white;
        public Program.Epiece piece;

        public virtual void SetCoord(Coord coord)
        {
           this.coord.Move(coord);
        }

        public abstract List<Move> GetMoves(Piece[,] boardPieces, ref List<Coord> attacks);

        public override string ToString()
        {
            return Program.dict[piece] + coord;    
        }

        public Piece(bool white, int x, int y)
        {
            coord = new Coord(x, y);
            this.white = white;
        }

        public override bool Equals(object? obj)
        {
            if (obj is Piece)
            {
                switch (this)
                {
                    case Rook:
                        if (!(obj is Rook))
                        {
                            return false;
                        }

                        break;

                    case Queen:
                        if (!(obj is Queen))
                        {
                            return false;
                        }

                        break;

                    case Pawn:
                        if (!(obj is Pawn))
                        {
                            return false;
                        }

                        break;

                    case Knight:
                        if (!(obj is Knight))
                        {
                            return false;
                        }

                        break;

                    case King:
                        if (!(obj is King))
                        {
                            return false;
                        }

                        break;

                    case Bishop:
                        if (!(obj is Bishop))
                        {
                            return false;
                        }

                        break;
                }

                Piece piece = (Piece) obj;

                return this.white == piece.white && piece.coord.Equals(this.coord);
            }
            else
            {
                return false;
            }
        }
    }
}
