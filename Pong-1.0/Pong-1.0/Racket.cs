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

        // Object for locking
        private readonly object lockObject = new object();

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
            lock (lockObject) // Monitor Pattern start
            {
                for (int i = 0; i < length; i++)
                {
                    Console.SetCursorPosition(xPosition, i + 1 + yPosition);
                    Console.WriteLine(tile);
                }
            } // Monitor Pattern end
        }

        // Clear the racket from the console
        public void Clear()
        {
            lock (lockObject) // Monitor Pattern start
            {
                for (int i = 0; i < length; i++)
                {
                    Console.SetCursorPosition(xPosition, i + 1 + yPosition);
                    Console.WriteLine(" ");
                }
            } // Monitor Pattern end
        }

        // Move the racket up
        public void MoveUp()
        {
            lock (lockObject) // Monitor Pattern start
            {
                if (yPosition > 0)
                {
                    Clear();
                    yPosition--;
                    Draw();
                }
            } // Monitor Pattern end
        }

        // Move the racket down
        public void MoveDown(int fieldWidth)
        {
            lock (lockObject) // Monitor Pattern start
            {
                if (yPosition < fieldWidth - length - 1)
                {
                    Clear();
                    yPosition++;
                    Draw();
                }
            } // Monitor Pattern end
        }

        // Check if the ball is hitting the racket
        public bool IsBallHitting(int ballY)
        {
            lock (lockObject) // Monitor Pattern start
            {
                return ballY >= yPosition + 1 && ballY <= yPosition + length;
            } // Monitor Pattern end
        }
    }
}
