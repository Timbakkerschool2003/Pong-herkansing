using System;
using System.Linq;

namespace Pong
{
    public class Field
    {
        private readonly int length;
        private readonly int width;
        private readonly char tile = '#';
        private readonly string line;

        public Field(int length, int width)
        {
            this.length = length;
            this.width = width;
            this.line = new string(tile, length);
        }

        public void Draw()
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine(line);

            Console.SetCursorPosition(0, width);
            Console.WriteLine(line);
        }
    }
}
