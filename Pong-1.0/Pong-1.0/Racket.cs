using System;

namespace Pong
{
    public class Racket
    {
        // Readonly fields for racket position and length
        private readonly int xPosition;
        private readonly int length;
        private readonly char tile = '|';

        // Current y position of the racket
        private int yPosition;

        // Constructor to initialize the racket position and length
        public Racket(int xPosition, int length)
        {
            this.xPosition = xPosition;
            this.length = length;
            this.yPosition = 0;
        }

        // Draw the racket on the console
        public void Draw()
        {
            for (int i = 0; i < length; i++)
            {
                Console.SetCursorPosition(xPosition, i + 1 + yPosition);
                Console.WriteLine(tile);
            }
        }

        // Clear the racket from the console
        public void Clear()
        {
            for (int i = 0; i < length; i++)
            {
                Console.SetCursorPosition(xPosition, i + 1 + yPosition);
                Console.WriteLine(" ");
            }
        }

        // Move the racket up
        public void MoveUp()
        {
            if (yPosition > 0)
            {
                Clear();
                yPosition--;
                Draw();
            }
        }

        // Move the racket down
        public void MoveDown(int fieldWidth)
        {
            if (yPosition < fieldWidth - length - 1)
            {
                Clear();
                yPosition++;
                Draw();
            }
        }

        // Check if the ball is hitting the racket
        public bool IsBallHitting(int ballY)
        {
            Clear();
            Draw();
            return ballY >= yPosition + 1 && ballY <= yPosition + length;
        }
    }
}
