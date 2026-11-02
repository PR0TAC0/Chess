using chess;
using System.Reflection.Metadata.Ecma335;

public class Program
{
    public enum Epiece
    {
        pawn,
        knight,
        bishop,
        rook,
        queen,
        king
    }

    public static Dictionary<Program.Epiece, string> dict = new Dictionary<Program.Epiece, string>
    {
        { Program.Epiece.king  , "k" },
        { Program.Epiece.queen , "q" },
        { Program.Epiece.pawn  , "p" },
        { Program.Epiece.bishop, "b" },
        { Program.Epiece.knight, "kn" },
        { Program.Epiece.rook  , "r" }
    };

    public static Func<int, bool> negativeFunction = n => n > -1;
    public static Func<int, bool> positiveFunction = n => n < 8;

    static Board test = new Board
    (
        new Piece[]
        {
            new Pawn(0, 1, false),
        },
        new King(0, 0, true),
        new King(0, 7, false)
    );

    static Board castleTest = new Board
    (
        new Piece[]
        {
            new Rook(0, 0, true),
            new Rook(7, 0, true),
            new Pawn(6, 0, true)
        },
        new King(3, 0, true),
        new King(3, 7, false)
    );

    static Board enPassantTest = new Board
    (
        new Piece[]
        {
            new Pawn(4, 4, false, true),
            new Pawn(5, 4, true)
        },
        new King(3, 3, true),
        new King(3, 7, false)
    );

    static Board takingTest = new Board
    (
        new Piece[]
        {
            new Rook(3, 6, true),
            new Rook(0, 6, false)
        },
        new King(0, 0, true),
        new King(7, 7, false)
    );

    public static void Main()
    {
        Board board = new Board
        (
            new Piece[]
            {
                new Rook  (0, 0, true),
                new Knight(1, 0, true),
                new Bishop(2, 0, true),
                new Queen (3, 0, true),
                new Bishop(5, 0, true),
                new Knight(6, 0, true),
                new Rook  (7, 0, true),
                new Rook  (0, 1, true),
                new Pawn  (1, 1, true),
                new Pawn  (2, 1, true),
                new Pawn  (3, 1, true),
                new Pawn  (4, 1, true),
                new Pawn  (5, 1, true),
                new Pawn  (6, 1, true),
                new Pawn  (7, 1, true),

                new Rook  (0, 7, false),
                new Knight(1, 7, false),
                new Bishop(2, 7, false),
                new Queen (3, 7, false),
                new Bishop(5, 7, false),
                new Knight(6, 7, false),
                new Rook  (7, 7, false),
                new Rook  (0, 6, false),
                new Pawn  (1, 6, false),
                new Pawn  (2, 6, false),
                new Pawn  (3, 6, false),
                new Pawn  (4, 6, false),
                new Pawn  (5, 6, false),
                new Pawn  (6, 6, false),
                new Pawn  (7, 6, false),
            },
            new King(4, 0, true),
            new King(4, 7, false)
        );
        
        while (true)
        {
            Console.WriteLine(board.Display());
            board.Move();
        }
    }
}