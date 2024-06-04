using System;
using System.Collections.Generic;

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

        private List<IObserver> observers = new List<IObserver>();

        // Constructor to initialize the ball position and field dimensions
        public Ball(int startX, int startY, int fieldLength, int fieldWidth)
        {
            this.x = startX;
            this.y = startY;
            this.fieldLength = fieldLength;
            this.fieldWidth = fieldWidth;
        }

        //ObserverPattern start
        // Add an observer to the list
        public void AddObserver(IObserver observer)
        {
            observers.Add(observer);
        }

        // Remove an observer from the list
        public void RemoveObserver(IObserver observer)
        {
            observers.Remove(observer);
        }

        // Notify all observers
        private void NotifyObservers()
        {
            foreach (var observer in observers)
            {
                observer.Update(this);
            }
        }

        //ObserverPattern End

        // Draw the ball on the console
        public void Draw()
        {
            Console.SetCursorPosition(x, y);
            Console.Write(tile);
        }

        // Clear the ball from the console
        public void Clear()
        {
            Console.SetCursorPosition(x, y);
            Console.Write(' ');
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
            if (x == 2)  // Adjusted position for left racket
            {
                if (leftRacket.IsBallHitting(y))
                {
                    isGoingRight = !isGoingRight;
                }
                else
                {
                    rightPlayerPoints++;
                    Reset();
                }
            }

            // Handle collision with right racket or scoring for left player
            if (x == fieldLength - 3)  // Adjusted position for right racket
            {
                if (rightRacket.IsBallHitting(y))
                {
                    isGoingRight = !isGoingRight;
                }
                else
                {
                    leftPlayerPoints++;
                    Reset();
                }
            }

            Draw();
            NotifyObservers();
        }

        // Reset the ball to the center of the field
        private void Reset()
        {
            x = fieldLength / 2;
            y = fieldWidth / 2;
        }

        // Getters for ball position
        public int X { get { return x; } }
        public int Y { get { return y; } }
    }

    public interface IObserver
    {
        void Update(Ball ball);
    }
}
