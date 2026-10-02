using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace chess
{
    public class Board
    {
        public List<Piece> pieces = new List<Piece>();
        bool white = true;

        public Board(Piece[] pieces)
        {
            this.pieces = pieces.ToList();
        }

        public string Display()
        {
            string[,] board = new string[8, 8];
            
            foreach(Piece p in pieces)
            {
                string piece = Program.dict[p.piece];

                if (p.white)
                {
                    piece = piece.ToUpper();
                }

                board[p.coord.x, p.coord.y] = piece;
            }

            string result = "";
            bool white = false;
            int label1 = 65;
            string header = "";

            for (int i = 1; i < 9; i ++)
            {
                header += $"  {i} ";
            }

            result += header + '\n';
            
            for (int i = 0; i < 8; i ++)
            {
                char cLabel = (char) (label1 + i);
                
                result += cLabel;
                
                for (int j = 0; j < 8; j ++)
                {
                    char bracket1 = '(';
                    char bracket2 = ')';

                    if (white)
                    {
                        bracket1 = '{';
                        bracket2 = '}';
                    }
                    
                    result += $"{bracket1}{board[i, j], 2}{bracket2}";

                    white = !white;
                }

                result += cLabel;

                white = !white;

                result += '\n';
            }

            result += header;

            return result;
        }

        Piece[,] GenerateBoard()
        {
            Piece[,] boardPieces = new Piece[8, 8];

            foreach (Piece p in this.pieces)
            {
                boardPieces[p.coord.x, p.coord.y] = p;
            }

            return boardPieces;
        }

        public List<Coord> Move()
        {
            List<BoardMove> moves = new List<BoardMove>();

            Piece[,] boardPieces = GenerateBoard();
            List<Coord> attacks = new List<Coord>();

            foreach (Piece p in pieces)
            {
                if (p.white == white)
                {
                    foreach (Move m in p.GetMoves(boardPieces, ref attacks))
                    {
                        BoardMove move = new BoardMove(p, m);

                        if (moves.Contains(move))
                        {
                            Console.WriteLine($"{p}: {move}");
                        }
                        else
                        {
                            Console.WriteLine(move);
                        }

                        moves.Add(move);
                    }
                }
            }
            
            white = !white;

            List<Coord> uniqueAttacks = new List<Coord>();

            foreach (Coord attack in attacks)
            {
                if (!uniqueAttacks.Contains(attack))
                {
                    uniqueAttacks.Add(attack);
                }
            }
            
            return uniqueAttacks;
        }
    }
}
