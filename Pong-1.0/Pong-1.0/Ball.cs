using System;

namespace Pong
{
    public class Ball
    {
        private int x;
        private int y;
        private readonly int fieldLength;
        private readonly int fieldWidth;
        private readonly char tile = 'O';

        private bool isGoingDown = true;
        private bool isGoingRight = true;

        public Ball(int startX, int startY, int fieldLength, int fieldWidth)
        {
            this.x = startX;
            this.y = startY;
            this.fieldLength = fieldLength;
            this.fieldWidth = fieldWidth;
        }

        public void Draw()
        {
            Console.SetCursorPosition(x, y);
            Console.WriteLine(tile);
        }

        public void Clear()
        {
            Console.SetCursorPosition(x, y);
            Console.WriteLine(" ");
        }

        public void Move(Racket leftRacket, Racket rightRacket, ref int leftPlayerPoints, ref int rightPlayerPoints)
        {
            Clear();

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

            // Collision with top or bottom
            if (y == 1 || y == fieldWidth - 1)
            {
                isGoingDown = !isGoingDown;
            }

            // Collision with left racket or scoring for right player
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

            // Collision with right racket or scoring for left player
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

        private void Reset()
        {
            x = fieldLength / 2;
            y = fieldWidth / 2;
        }
    }
}
