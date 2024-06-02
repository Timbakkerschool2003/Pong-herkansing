using System;

namespace Pong
{
    public class Field
    {
        // Readonly fields for field dimensions and tile character
        private readonly int length;
        private readonly int width;
        private readonly char tile = '#';
        private readonly string horizontalLine;

        // Constructor to initialize the field dimensions
        public Field(int length, int width)
        {
            this.length = length;
            this.width = width;
            this.horizontalLine = new string(tile, length);
        }

        // Draw the field borders
        public void Draw()
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine(horizontalLine);

            for (int i = 1; i < width; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write(tile);
                Console.SetCursorPosition(length - 1, i);
                Console.Write(tile);
            }

            Console.SetCursorPosition(0, width);
            Console.WriteLine(horizontalLine);
        }
    }
}