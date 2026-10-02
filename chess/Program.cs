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

    public static Board standard =
    new Board
    (
        new Piece[]
        {
            new Pawn(0, 1, true),
            new Pawn(1, 1, true),
            new Pawn(2, 1, true),
            new Pawn(3, 1, true),
            new Pawn(4, 1, true),
            new Pawn(5, 1, true),
            new Pawn(6, 1, true),
            new Pawn(7, 1, true),
            new Pawn(0, 6, false),
            new Pawn(1, 6, false),
            new Pawn(2, 6, false),
            new Pawn(3, 6, false),
            new Pawn(4, 6, false),
            new Pawn(5, 6, false),
            new Pawn(6, 6, false),
            new Pawn(7, 6, false),
        }
    );

    static Board pawns = new Board(
    new Piece[]
        {
            new Pawn(1, 1, true),
            new Pawn(3, 1, true),
            new Pawn(0, 2, false),
            new Pawn(2, 2, false)
        }
    );

    static Board knights = new Board(
        new Piece[]
        {
            new Knight(3, 3, true),
            new Knight(0, 0, true),
            new Knight(0, 7, true),
            new Knight(7, 0, true),
            new Knight(7, 7, true)
        }
    );

    static Board rooks = new Board(
        new Piece[]
        {
            new Rook(3, 3, true),
            new Rook(0, 0, true),
            new Rook(0, 7, true),
            new Rook(7, 0, true),
            new Rook(7, 7, true),

            new Pawn(4, 3, false),
            new Pawn(3, 4, false),
            new Pawn(2, 3, false),
            new Pawn(3, 2, false)
        }
    );

    static Board bishops = new Board(
        new Piece[]
        {
            new Bishop(3, 3, true),
            new Bishop(0, 0, true),
            new Bishop(0, 7, true),
            new Bishop(7, 0, true),
            new Bishop(7, 7, true),

            new Pawn(4, 4, false),
            new Pawn(4, 2, false),
            new Pawn(2, 2, false),
            new Pawn(2, 4, false)
        }
    );

    static Board queen = new Board(
        new Piece[]
        {
            new Queen(3, 3, true)
        }
    );

    static Board kings = new Board
    (
        new Piece[]
        {
            new King(3, 3, true),
            new King(0, 0, true),
            new King(0, 7, true),
            new King(7, 0, true),
            new King(7, 7, true)
        }
    );

public static void Main()
    {
        Console.WriteLine(knights.Display());

        List<Coord> attacks = knights.Move();

        Console.WriteLine("ATTACKS:");

        foreach (Coord attack in attacks)
        {
            Console.WriteLine($"\t{attack}");
        }
    }
}