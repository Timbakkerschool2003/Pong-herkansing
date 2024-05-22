using System;
using System.Threading;

namespace Pong
{
    public class Game
    {
        private const int FieldLength = 70;
        private const int FieldWidth = 19;
        private const int RacketLength = FieldWidth / 4;

        private Field field;
        private Racket leftRacket;
        private Racket rightRacket;
        private Ball ball;

        private int leftPlayerPoints;
        private int rightPlayerPoints;

        public Game()
        {
            field = new Field(FieldLength, FieldWidth);
            leftRacket = new Racket(1, RacketLength);
            rightRacket = new Racket(FieldLength - 2, RacketLength);
            ball = new Ball(FieldLength / 2, FieldWidth / 2, FieldLength, FieldWidth);
        }

        public void Start()
        {
            while (true)
            {
                field.Draw();
                leftRacket.Draw();
                rightRacket.Draw();
                ball.Draw();

                while (!Console.KeyAvailable)
                {
                    ball.Move(leftRacket, rightRacket, ref leftPlayerPoints, ref rightPlayerPoints);

                    Console.SetCursorPosition(FieldLength / 2 - 2, FieldWidth + 1);
                    Console.WriteLine($"{leftPlayerPoints} | {rightPlayerPoints}");

                    if (leftPlayerPoints == 10 || rightPlayerPoints == 10)
                    {
                        EndGame();
                        return;
                    }

                    Thread.Sleep(100);
                }

                HandleInput();
            }
        }

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

        private void EndGame()
        {
            Console.SetCursorPosition(0, FieldWidth + 2);

            if (rightPlayerPoints == 10)
            {
                Console.WriteLine("Right player wins!");
            }
            else
            {
                Console.WriteLine("Left player wins!");
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
