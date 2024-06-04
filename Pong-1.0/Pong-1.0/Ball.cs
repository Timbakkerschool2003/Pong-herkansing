using System;

namespace Pong
{
    public class Ball
    {
        // Fields for ball position and movement direction
        private int x;
        private int y;
        private readonly int fieldLength;
        private readonly int fieldWidth;
        private readonly char tile = 'O';

        private bool isGoingDown = true;
        private bool isGoingRight = true;

        // Constructor to initialize the ball position and field dimensions
        public Ball(int startX, int startY, int fieldLength, int fieldWidth)
        {
            this.x = startX;
            this.y = startY;
            this.fieldLength = fieldLength;
            this.fieldWidth = fieldWidth;
        }

        // Draw the ball on the console
        public void Draw()
        {
            Console.SetCursorPosition(x, y);
            Console.WriteLine(tile);
        }

        // Clear the ball from the console
        public void Clear()
        {
            Console.SetCursorPosition(x, y);
            Console.WriteLine(" ");
        }

        // Move the ball and handle collisions
        public void Move(Racket leftRacket, Racket rightRacket, ref int leftPlayerPoints, ref int rightPlayerPoints)
        {
            Clear();

            // Update ball position
            if (isGoingDown)
            {
                y++;
            }
            else
            {
                y--;
            }

            if (isGoingRight)
            {
                x++;
            }
            else
            {
                x--;
            }

            // Handle collision with top or bottom of the field
            if (y == 1 || y == fieldWidth - 1)
            {
                isGoingDown = !isGoingDown;
            }

            // Handle collision with left racket or scoring for right player
            if (x == 1)
            {
                if (leftRacket.IsBallHitting(y))
                {
                    isGoingRight = !isGoingRight;
                    leftRacket.Clear();
                    leftRacket.Draw();
                }
                else
                {
                    rightPlayerPoints++;
                    Reset();
                }
            }

            // Handle collision with right racket or scoring for left player
            if (x == fieldLength - 2)
            {
                if (rightRacket.IsBallHitting(y))
                {
                    isGoingRight = !isGoingRight;
                    rightRacket.Clear();
                    rightRacket.Draw();
                }
                else
                {
                    leftPlayerPoints++;
                    Reset();
                }
            }

            Draw();
        }

        // Reset the ball to the center of the field
        private void Reset()
        {
            x = fieldLength / 2;
            y = fieldWidth / 2;
        }
    }
}
