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

        // Constructor to initialize the field dimensions
        public Field(int length, int width)
        {
            this.length = length;
            this.width = width;
            this.horizontalLine = new string(horizontalTile, length);
        }

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