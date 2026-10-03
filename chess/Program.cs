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

    public static void Main()
    {
        Console.WriteLine(test.Display());

        test.Move();
    }
}