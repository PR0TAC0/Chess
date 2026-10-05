using System;
using System.Collections.Generic;
using System.Text;

namespace chess
{
    public class EnPassant: Coord
    {
        public Pawn attacker;
        public Pawn victim;
        public List<Piece> pieces;

        public EnPassant(Pawn attacker, Pawn victim, List<Piece> pieces): base(0, 0)
        {
            this.attacker = attacker;
            this.victim = victim;
            this.pieces = pieces;
        }

        public override bool Equals(object? obj)
        {
            return false;
        }
    }
}
