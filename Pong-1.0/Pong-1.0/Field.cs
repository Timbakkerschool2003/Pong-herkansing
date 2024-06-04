using System;

namespace Pong
{
    public class Field
    {
        // Readonly fields for field dimensions and tile character
        private readonly int length;
        private readonly int width;
        private readonly char horizontalTile = '-';
        private readonly char verticalTile = '|';
        private readonly string horizontalLine;

        // Singleton pattern start
        // Static field to keep track of the single instance of the class
        private static Field instance;

        // Private constructor to prevent external instantiation
        private Field(int length, int width)
        {
            this.length = length;
            this.width = width;
            this.horizontalLine = new string(horizontalTile, length);
        }

        // Public method to get the single instance of the class
        public static Field GetInstance(int length, int width)
        {
            if (instance == null)
            {
                instance = new Field(length, width);
            }
            return instance;
        }
        // Singleton pattern end

        // Draw the field boundaries
        public void Draw()
        {
            // Draw the top boundary
            Console.SetCursorPosition(0, 0);
            Console.WriteLine(horizontalLine);

            // Draw the left and right boundaries
            for (int i = 1; i < width; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write(verticalTile);
                Console.SetCursorPosition(length - 1, i);
                Console.Write(verticalTile);
            }

            // Draw the bottom boundary
            Console.SetCursorPosition(0, width);
            Console.WriteLine(horizontalLine);
        }
    }
}