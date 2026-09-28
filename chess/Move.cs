using System;
using System.Collections.Generic;
using System.Text;

namespace chess
{
    public class Move
    {
        public int x { get; } 
        public int y { get; }

        public Move(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
