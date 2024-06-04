using System;
using System.Threading;

namespace Pong
{
    public class Game
    {
        // Constanten voor veldafmetingen en racketlengte
        private const int FieldLength = 70;
        private const int FieldWidth = 19;
        private const int RacketLength = FieldWidth / 4;

        // Spelcomponenten
        private Field field;
        private Racket leftRacket;
        private Racket rightRacket;
        private Ball ball;

        // Spelerpunten
        private int leftPlayerPoints;
        private int rightPlayerPoints;

        // Facade-patroon start
        // De Game-klasse fungeert als een Facade om de interactie met de spelcomponenten te vereenvoudigen.
        // Het initialiseert en beheert het veld, de rackets en de bal, en biedt een enkele interface
        // voor het starten en beheren van het spel. Dit maakt de code gemakkelijker te gebruiken en te begrijpen
        // door de complexiteit van de onderliggende componenten te verbergen.

        // Constructor om de spelcomponenten te initialiseren
        public Game()
        {
            field = new Field(FieldLength, FieldWidth);
            leftRacket = new Racket(2, RacketLength); // Aangepaste positie voor linkerracket
            rightRacket = new Racket(FieldLength - 3, RacketLength); // Aangepaste positie voor rechterracket
            ball = new Ball(FieldLength / 2, FieldWidth / 2, FieldLength, FieldWidth);
        }

        // Start de spel lus
        public void Start()
        {
            // Verberg de cursor
            Console.CursorVisible = false;

            while (true)
            {
                // Teken de spelcomponenten
                field.Draw();
                leftRacket.Draw();
                rightRacket.Draw();
                ball.Draw();

                // Beweeg de bal terwijl er geen toets is ingedrukt
                while (!Console.KeyAvailable)
                {
                    ball.Move(leftRacket, rightRacket, ref leftPlayerPoints, ref rightPlayerPoints);

                    // Teken de rackets opnieuw om te voorkomen dat ze worden overschreven
                    leftRacket.Draw();
                    rightRacket.Draw();

                    // Toon de score
                    Console.SetCursorPosition(FieldLength / 2 - 2, FieldWidth + 1);
                    Console.WriteLine($"{leftPlayerPoints} | {rightPlayerPoints}");

                    // Controleer of het spel is afgelopen
                    if (leftPlayerPoints == 10 || rightPlayerPoints == 10)
                    {
                        EndGame();
                        return;
                    }

                    // Vertraging voor balbeweging
                    Thread.Sleep(100);
                }

                // Verwerk spelersinvoer
                HandleInput();
            }
        }

        // Verwerk spelersinvoer voor racketbeweging
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

        // Beëindig het spel en toon de winnaar
        private void EndGame()
        {
            // Toon de cursor
            Console.CursorVisible = true;

            Console.SetCursorPosition(0, FieldWidth + 2);

            if (rightPlayerPoints == 10)
            {
                Console.WriteLine("Rechts wint!");
            }
            else
            {
                Console.WriteLine("Links wint!");
            }

            Console.WriteLine("Klik op een toets om af te sluiten...");
            Console.ReadKey();
        }
        // Facade-patroon einde
    }
}
