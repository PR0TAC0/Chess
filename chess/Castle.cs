using System;
using System.Collections.Generic;
using System.Text;

namespace chess
{
    public class Castle: Coord
    {
        public Rook rook;
        public King king;

        public Castle(Rook rook, King king): base(0, 0)
        {
            this.rook = rook;
            this.king = king;
        }

        public override bool Equals(object? obj)
        {
            return false;
        }
    }
}
