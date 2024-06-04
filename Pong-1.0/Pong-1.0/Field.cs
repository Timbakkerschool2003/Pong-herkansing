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

        // Singleton Pattern start
        // Static variable to hold the single instance of the class
        private static Field instance;

        // Private constructor to prevent instantiation from outside
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
        // Singleton Pattern end

        // Draw the field borders
        public void Draw()
        {
            // Draw the top border
            Console.SetCursorPosition(0, 0);
            Console.WriteLine(horizontalLine);

            // Draw the left and right borders
            for (int i = 1; i < width; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write(verticalTile);
                Console.SetCursorPosition(length - 1, i);
                Console.Write(verticalTile);
            }

            // Draw the bottom border
            Console.SetCursorPosition(0, width);
            Console.WriteLine(horizontalLine);
        }
    }
}
