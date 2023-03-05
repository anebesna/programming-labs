using System;
using System.Linq;
using static System.Console;

namespace flyweight
{
    class Program
    {
        static string[] correctPositions = {"R", "KN", "B", "Q", "K", "B", "KN", "R"};
        static PieceFactory pieceFactory = new PieceFactory();

        static void Main(string[] args)
        {
            Write("1|");
            PlaceBlackPieces();
            WriteLine();
            for (int i = 3; i < 7; i++)
            {
                WriteLine($"{i}|{string.Concat(Enumerable.Repeat("-", 8))}");
            }
            PlaceWhitePieces();
            WriteLine("\n  ────────");
            WriteLine("  ABCDEFGH");
            WriteLine($"\nAmount of created objects: {pieceFactory.pieceCount}");
        }
        static public void PlaceWhitePieces()
        {
            for (int i = 0; i < correctPositions.Length; i++)
            {
                Piece piece = pieceFactory.GetPiece(correctPositions[i]);
                Piece pawn = pieceFactory.GetPiece("P");
                SetCursorPosition(0, 5);
                Write("\n7|");
                pawn.Deploy(i, 6, Color.Yellow);
                Write("\n8|");
                piece.Deploy(i, 7, Color.Yellow);
            }
        }
        static public void PlaceBlackPieces()
        {
            for (int i = 0; i < correctPositions.Length; i++)
            {
                Piece piece = pieceFactory.GetPiece(correctPositions[i]);
                Piece pawn = pieceFactory.GetPiece("P");
                Write("\n2|");
                piece.Deploy(i, 0, Color.Blue);
                pawn.Deploy(i, 1, Color.Blue);
            }
        }
    }
}
