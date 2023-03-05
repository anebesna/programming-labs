using System;
namespace flyweight
{
    public class Piece
    {
        protected char sprite;
        protected int x, y;
        protected Color color;

        public void Deploy(int x, int y, Color color)
        {
            this.x = x;
            this.y = y;
            this.color = color;
            Console.ForegroundColor = (ConsoleColor)color;
            Console.SetCursorPosition(x+2, y);
            Console.Write(sprite);
            Console.ResetColor();
        }

    }
    public enum Color
    {
        Yellow = ConsoleColor.Yellow,
        Blue = ConsoleColor.Blue
    }
}
