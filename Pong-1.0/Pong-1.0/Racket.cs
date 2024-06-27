using System;
using System.Threading;

namespace Pong
{
    public class Racket
    {
        // Readonly velden voor racketpositie en lengte
        private readonly int xPosition;
        private readonly int length;
        private readonly char tile = '|';

        // Huidige y-positie van het racket
        private int yPosition;

        // Object voor vergrendeling
        private readonly object lockObject = new object();

        // Constructor om de racketpositie en lengte te initialiseren
        public Racket(int xPosition, int length)
        {
            this.xPosition = xPosition;
            this.length = length;
            this.yPosition = 0;
        }

        // Teken het racket op de console
        public void Draw()
        {
            lock (lockObject) // Monitor-patroon start
            {
                for (int i = 0; i < length; i++)
                {
                    Console.SetCursorPosition(xPosition, i + 1 + yPosition);
                    Console.WriteLine(tile);
                }
            } // Monitor-patroon einde
        }

        // Wis het racket van de console
        public void Clear()
        {
            lock (lockObject) // Monitor-patroon start
            {
                for (int i = 0; i < length; i++)
                {
                    Console.SetCursorPosition(xPosition, i + 1 + yPosition);
                    Console.WriteLine(" ");
                }
            } // Monitor-patroon einde
        }

        // Beweeg het racket omhoog
        public void MoveUp()
        {
            lock (lockObject) // Monitor-patroon start
            {
                if (yPosition > 0)
                {
                    Clear();
                    yPosition--;
                    Draw();
                }
            } // Monitor-patroon einde
        }

        // Beweeg het racket omlaag
        public void MoveDown(int fieldWidth)
        {
            lock (lockObject) // Monitor-patroon start
            {
                if (yPosition < fieldWidth - length - 1)
                {
                    Clear();
                    yPosition++;
                    Draw();
                }
            } // Monitor-patroon einde
        }

        // Controleer of de bal het racket raakt
        public bool IsBallHitting(int ballY)
        {
            lock (lockObject) // Monitor-patroon start
            {
                return ballY >= yPosition + 1 && ballY <= yPosition + length;
            } // Monitor-patroon einde
        }

        // Multithreading start
        public void StartMoving(int fieldWidth)
        {
            Thread moveUpThread = new Thread(() =>
            {
                while (true)
                {
                    MoveUp();
                    Thread.Sleep(100); // Pauzeer tussen de bewegingen
                }
            });

            Thread moveDownThread = new Thread(() =>
            {
                while (true)
                {
                    MoveDown(fieldWidth);
                    Thread.Sleep(100); // Pauzeer tussen de bewegingen
                }
            });

            moveUpThread.Start();
            moveDownThread.Start();
        }
        // Multithreading einde
    }
}
