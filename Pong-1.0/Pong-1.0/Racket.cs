using System;

namespace Pong
{
    public class Racket
    {
        private readonly int xPosition;
        private readonly int length;
        private readonly char tile = '|';

        private int yPosition;

        public Racket(int xPosition, int length)
        {
            this.xPosition = xPosition;
            this.length = length;
            this.yPosition = 0;
        }

        public void Draw()
        {
            for (int i = 0; i < length; i++)
            {
                Console.SetCursorPosition(xPosition, i + 1 + yPosition);
                Console.WriteLine(tile);
            }
        }

        public void Clear()
        {
            for (int i = 0; i < length; i++)
            {
                Console.SetCursorPosition(xPosition, i + 1 + yPosition);
                Console.WriteLine(" ");
            }
        }

        public void MoveUp()
        {
            if (yPosition > 0)
            {
                Clear();
                yPosition--;
                Draw();
            }
        }

        public void MoveDown(int fieldWidth)
        {
            if (yPosition < fieldWidth - length - 1)
            {
                Clear();
                yPosition++;
                Draw();
            }
        }

        public bool IsBallHitting(int ballY)
        {
            Clear();
            Draw();
            return ballY >= yPosition + 1 && ballY <= yPosition + length;
        }
    }
}
