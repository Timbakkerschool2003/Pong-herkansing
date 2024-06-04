using System;
using System.Threading;

namespace Pong
{
    public class Game
    {
        // Constants for field dimensions and racket length
        private const int FieldLength = 70;
        private const int FieldWidth = 19;
        private const int RacketLength = FieldWidth / 4;

        // Game components
        private Field field;
        private Racket leftRacket;
        private Racket rightRacket;
        private Ball ball;

        // Player points
        private int leftPlayerPoints;
        private int rightPlayerPoints;

        // Constructor to initialize game components
        public Game()
        {
            field = Field.GetInstance(FieldLength, FieldWidth); // Use GetInstance method for Singleton
            leftRacket = new Racket(2, RacketLength); // Custom position for left racket
            rightRacket = new Racket(FieldLength - 3, RacketLength); // Custom position for right racket
            ball = new Ball(FieldLength / 2, FieldWidth / 2, FieldLength, FieldWidth);
        }

        // Start the game loop
        public void Start()
        {
            // Hide the cursor
            Console.CursorVisible = false;

            while (true)
            {
                // Draw game components
                field.Draw();
                leftRacket.Draw();
                rightRacket.Draw();
                ball.Draw();

                // Move the ball while no key is pressed
                while (!Console.KeyAvailable)
                {
                    ball.Move(leftRacket, rightRacket, ref leftPlayerPoints, ref rightPlayerPoints);

                    // Redraw the rackets to prevent them from being overwritten
                    leftRacket.Draw();
                    rightRacket.Draw();

                    // Display the score
                    Console.SetCursorPosition(FieldLength / 2 - 2, FieldWidth + 1);
                    Console.WriteLine($"{leftPlayerPoints} | {rightPlayerPoints}");

                    // Check if the game is over
                    if (leftPlayerPoints == 10 || rightPlayerPoints == 10)
                    {
                        EndGame();
                        return;
                    }

                    // Delay for ball movement
                    Thread.Sleep(100);
                }

                // Process player input
                HandleInput();
            }
        }

        // Process player input for racket movement
        private void HandleInput()
        {
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.UpArrow:
                    rightRacket.MoveUp();
                    break;

                case ConsoleKey.DownArrow:
                    rightRacket.MoveDown(FieldWidth);
                    break;

                case ConsoleKey.W:
                    leftRacket.MoveUp();
                    break;

                case ConsoleKey.S:
                    leftRacket.MoveDown(FieldWidth);
                    break;
            }
        }

        // End the game and show the winner
        private void EndGame()
        {
            // Show the cursor
            Console.CursorVisible = true;

            Console.SetCursorPosition(0, FieldWidth + 2);

            if (rightPlayerPoints == 10)
            {
                Console.WriteLine("Rechts wint!");
            }
            else
            {
                Console.WriteLine("Links wint!!");
            }

            Console.WriteLine("Klik op een toets...");
            Console.ReadKey();
        }
    }
}