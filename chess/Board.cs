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

        King whiteKing;
        King blackKing;

        public Board(Piece[] pieces, King whiteKing, King blackKing)
        {
            if (whiteKing.white != true)
            {
                throw new ArgumentException(whiteKing.white.ToString());
            }

            if (blackKing.white == true)
            {
                throw new ArgumentException(blackKing.white.ToString());
            }

            this.pieces = pieces.ToList();

            foreach (Piece piece in pieces)
            {
                if (piece is King)
                {
                    throw new ArgumentException(piece.ToString());
                }
            }

            this.whiteKing = whiteKing;
            this.blackKing = blackKing;
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

            board[whiteKing.coord.x, whiteKing.coord.y] = "K";
            board[blackKing.coord.x, blackKing.coord.y] = "k";

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

        static Piece[,] GenerateBoard(Board board)
        {
            Piece[,] boardPieces = new Piece[8, 8];

            foreach (Piece p in board.pieces)
            {
                boardPieces[p.coord.x, p.coord.y] = p;
            }

            King whiteKing = board.whiteKing;
            King blackKing = board.blackKing;

            boardPieces[whiteKing.coord.x, whiteKing.coord.y] = board.whiteKing;
            boardPieces[blackKing.coord.x, blackKing.coord.y] = board.blackKing;

            return boardPieces;
        }

        List<BoardMove> GenerateCastleMoves(bool white, List<Coord> attacks)
        {
            int y = 7;
            King king = blackKing;

            if (white)
            {
                y = 0;
                king = whiteKing;
            }

            if (king.moved)
            {
                return new List<BoardMove>();
            }

            // must be at starting position
            if (!king.coord.Equals(new Coord(3, y)))
            {
                return new List<BoardMove>();
            }

            List<BoardMove> moves = new List<BoardMove>();

            List<Rook> rooks = new List<Rook>();

            Coord rightRookCoord = new Coord(7, y);
            Coord leftRookCoord = new Coord(0, y);

            // Find rooks
            foreach (Piece piece in pieces)
            {
                if (piece is Rook)
                {
                    Rook rook = (Rook)piece;

                    if (!rook.moved)
                    {
                        if (new Coord[] { rightRookCoord, leftRookCoord }.Contains(rook.coord)) // One of the starting rooks
                        {
                            rooks.Add(rook);
                        }
                    }
                }
            }

            foreach (Rook rook in rooks)
            {
                List<Coord> cancles = new List<Coord>();

                if (rook.coord.Equals(rightRookCoord)) // rightmost rook
                {
                    cancles = new List<Coord>()
                            {
                                new Coord(6, y),
                                new Coord(5, y),
                                new Coord(4, y),
                                new Coord(3, y)
                            };
                }
                else
                {
                    cancles = new List<Coord>()
                            {
                                new Coord(1, y),
                                new Coord(2, y),
                                new Coord(3, y)
                            };
                }

                bool ableToCastle = true;
                Piece[,] boardPieces = GenerateBoard(this);

                // Checks if pieces are empty
                foreach(Coord coord in cancles)
                { 
                    if (!coord.Equals(new Coord(3, y)))
                    {
                        if (boardPieces[coord.x, coord.y] != null)
                        {
                            ableToCastle = false;

                            break;
                        }
                    }
                }

                if (ableToCastle)
                {
                    foreach (Coord attack in attacks)
                    {
                        if (cancles.Contains(attack))
                        {
                            ableToCastle = false;

                            break;
                        }
                    }
                }

                if (ableToCastle)
                {
                    moves.Add(new BoardMove(new Castle(rook, king)));
                }
            }

            return moves;
        }

        public bool Move()
        {
            List<BoardMove> moves = new List<BoardMove>();

            Piece[,] boardPieces = GenerateBoard(this);
            List<Coord> attackPlaceholder = new List<Coord>();
            List<Coord> oldAttacks = new List<Coord>();

            King king = blackKing;

            if (white)
            {
                king = whiteKing;
            }

            foreach (Piece p in pieces)
            {
                if (p.white == white)
                {
                    foreach (Move m in p.GetMoves(boardPieces, ref attackPlaceholder))
                    {
                        moves.Add(new BoardMove(p, m));
                    }
                }
                else
                {
                    p.GetMoves(boardPieces, ref oldAttacks);

                    if (white)
                    {
                        blackKing.GetMoves(boardPieces, ref oldAttacks);
                    }
                    else
                    {
                        whiteKing.GetMoves(boardPieces, ref oldAttacks);
                    }
                }
            }

            Move[] kingMoves = king.GetMoves(boardPieces, ref attackPlaceholder).ToArray();

            foreach (Move move in kingMoves)
            {
                moves.Add(new BoardMove(king, move));
            }

            if (white)
            {
                king = whiteKing; 
            }
            else
            {
                king = blackKing;   
            }

            Board nextBoard;

            List<int> indicesToRemove = new List<int>();

            // Checking if moves are valid
            for (int i = 0; i < moves.Count; i ++)
            {
                BoardMove move = moves[i];
                
                // Copying pieces (so old objects are not altered)
                List<Piece> pieces = new List<Piece>();

                foreach (Piece piece in this.pieces)
                {
                    Piece newPiece;
                    
                    switch (piece)
                    {
                        case Rook:
                            newPiece = new Rook(piece.coord.x, piece.coord.y, piece.white);

                            break;

                        case Queen:
                            newPiece = new Queen(piece.coord.x, piece.coord.y, piece.white);

                            break;

                        case Pawn:
                            Pawn pawn = (Pawn) piece;
                            
                            newPiece = new Pawn(piece.coord.x, piece.coord.y, piece.white, pawn.moved);

                            break;

                        case Knight:
                            newPiece = new Knight(piece.coord.x, piece.coord.y, piece.white);

                            break;

                        default: // Bishop
                            newPiece = new Bishop(piece.coord.x, piece.coord.y, piece.white);

                            break;
                    }

                    pieces.Add(newPiece);
                }

                // Generating board
                nextBoard = 
                new Board
                (
                    pieces.ToArray(),
                    new King(whiteKing.coord.x, whiteKing.coord.y, true),
                    new King(blackKing.coord.x, blackKing.coord.y, false)
                );
                
                // finding piece to move
                Piece? pieceToMove = null;
                
                foreach(Piece piece in pieces)
                {
                    if (piece.Equals(move.p))
                    {
                        pieceToMove = piece;

                        break;
                    }
                }

                // finding king
                King nextKing = nextBoard.blackKing;

                if (white)
                {
                    nextKing = nextBoard.whiteKing;
                }

                // default case: piece to move is the king
                if (pieceToMove == null)
                {
                    pieceToMove = nextKing;
                }

                pieceToMove.SetCoord(move.coord); // Moving piece

                // Generate Attacks

                List<Coord> attacks = new List<Coord>();
                Piece[,] newBoardPieces = GenerateBoard(nextBoard);

                // Generating oponent's king's attacks
                if (white)
                {
                    nextBoard.blackKing.GetMoves(newBoardPieces, ref attacks);
                }
                else
                {
                    nextBoard.whiteKing.GetMoves(newBoardPieces, ref attacks);
                }

                // Generating everyother attack
                foreach (Piece piece in pieces)
                {
                    if (piece.white != white)
                    {
                        piece.GetMoves(newBoardPieces, ref attacks);
                    }
                }

                // Check if move is valid

                foreach (Coord attack in attacks)
                {
                    if (nextKing.coord.Equals(attack)) // move puts king under check
                    {
                        indicesToRemove.Add(i);
                    }
                }
            }

            List<BoardMove> newMoves = new List<BoardMove>();

            // Filtiring out invalid moves
            for (int i = 0; i < moves.Count; i ++)
            {
                if (!indicesToRemove.Contains(i))
                {
                    newMoves.Add(moves[i]);
                }
            }

            // Adding castle move
            if (white)
            {
                newMoves.AddRange(GenerateCastleMoves(true, oldAttacks));
            }
            else
            {
                newMoves.AddRange(GenerateCastleMoves(false, oldAttacks));
            }

            moves = newMoves;

            if (moves.Count == 0)
            {
                return false;
            }

            Dictionary<string, BoardMove> moveDict = new Dictionary<string, BoardMove>();
            
            foreach (BoardMove move in moves)
            {
                if (moves.FindAll(n => n.Equals(move)).Count > 1)
                {
                    Piece piece = move.p;

                    string s = $"{Program.dict[piece.piece]}{piece.coord}:{move}";

                    moveDict.Add(s, move);

                    Console.WriteLine(s);
                }
                else
                { 
                    moveDict.Add(move.ToString(), move);

                    Console.WriteLine(move);
                }
            }

            BoardMove selectedMove;

            while (true)
            {
                Console.WriteLine("Select a move by typing it");

                string key = Console.ReadLine();

                try
                {
                    selectedMove = moveDict[key];

                    break;
                }
                catch
                { 
                    Console.WriteLine("ERROR: Invalid move entered, try again");
                }
            }
            
            white = !white;

            return true;
        }
    }
}
