using System;
using System.Linq;

namespace Pong
{
    public class Field
    {
        // Readonly fields for field dimensions and tile character
        private readonly int length;
        private readonly int width;
        private readonly char tile = '#';
        private readonly string line;

        // Constructor to initialize the field dimensions
        public Field(int length, int width)
        {
            this.length = length;
            this.width = width;
            this.line = new string(tile, length);
        }

        // Draw the field borders
        public void Draw()
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine(line);

            Console.SetCursorPosition(0, width);
            Console.WriteLine(line);
        }
    }
}
