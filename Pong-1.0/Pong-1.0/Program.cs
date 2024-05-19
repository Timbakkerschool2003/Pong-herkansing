using System;
using System.Linq;
using System.Threading;

namespace Pong
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Veldaanstellingen
            const int fieldLength = 50, fieldWidth = 15; // Lengte en breedte van het veld
            const char fieldTile = '#'; // Veldtegel symbool
            string line = string.Concat(Enumerable.Repeat(fieldTile, fieldLength)); // Creëer de boven- en onderkant van het veld

            // Racketinstellingen
            const int racketLength = fieldWidth / 4; // Lengte van het racket
            const char racketTile = '|'; // Racketsymbool

            int leftRacketHeight = 0; // Startpositie van het linkerracket
            int rightRacketHeight = 0; // Startpositie van het rechterracket

            // Balinstellingen
            int ballX = fieldLength / 2; // Startpositie van de bal (X)
            int ballY = fieldWidth / 2; // Startpositie van de bal (Y)
            const char ballTile = 'O'; // Balsymbool

            bool isBallGoingDown = true; // Richting van de bal (omlaag)
            bool isBallGoingRight = true; // Richting van de bal (rechts)

            // Spelerscores
            int leftPlayerPoints = 0; // Punten van de linker speler
            int rightPlayerPoints = 0; // Punten van de rechter speler

            // Scorebordinstellingen
            int scoreboardX = fieldLength / 2 - 2; // X-positie van het scorebord
            int scoreboardY = fieldWidth + 1; // Y-positie van het scorebord

            while (true) // Hoofdgame loop
            {
                // Teken de boven- en onderkant van het veld
                Console.SetCursorPosition(0, 0);
                Console.WriteLine(line);

                Console.SetCursorPosition(0, fieldWidth);
                Console.WriteLine(line);

                // Teken de rackets
                for (int i = 0; i < racketLength; i++)
                {
                    Console.SetCursorPosition(0, i + 1 + leftRacketHeight); // Linkerracket
                    Console.WriteLine(racketTile);
                    Console.SetCursorPosition(fieldLength - 1, i + 1 + rightRacketHeight); // Rechterracket
                    Console.WriteLine(racketTile);
                }

                // Bewegende bal
                while (!Console.KeyAvailable)
                {
                    Console.SetCursorPosition(ballX, ballY);
                    Console.WriteLine(ballTile); // Teken de bal
                    Thread.Sleep(100); // Pauze

                    Console.SetCursorPosition(ballX, ballY);
                    Console.WriteLine(" "); // Verwijder de bal van de vorige positie

                    // Beweeg de bal
                    if (isBallGoingDown)
                    {
                        ballY++;
                    }
                    else
                    {
                        ballY--;
                    }

                    if (isBallGoingRight)
                    {
                        ballX++;
                    }
                    else
                    {
                        ballX--;
                    }

                    // Botsing met boven- of onderkant
                    if (ballY == 1 || ballY == fieldWidth - 1)
                    {
                        isBallGoingDown = !isBallGoingDown;
                    }

                    // Botsing met linkerracket of score voor rechterspeler
                    if (ballX == 1)
                    {
                        if (ballY >= leftRacketHeight + 1 && ballY <= leftRacketHeight + racketLength)
                        {
                            isBallGoingRight = !isBallGoingRight; // Bal stuitert terug
                        }
                        else
                        {
                            rightPlayerPoints++;
                            ballY = fieldWidth / 2;
                            ballX = fieldLength / 2;
                            Console.SetCursorPosition(scoreboardX, scoreboardY);
                            Console.WriteLine($"{leftPlayerPoints} | {rightPlayerPoints}");
                            if (rightPlayerPoints == 10)
                            {
                                break;
                            }
                        }
                    }

                    // Botsing met rechterracket of score voor linkerspeler
                    if (ballX == fieldLength - 2)
                    {
                        if (ballY >= rightRacketHeight + 1 && ballY <= rightRacketHeight + racketLength)
                        {
                            isBallGoingRight = !isBallGoingRight; // Bal stuitert terug
                        }
                        else
                        {
                            leftPlayerPoints++;
                            ballY = fieldWidth / 2;
                            ballX = fieldLength / 2;
                            Console.SetCursorPosition(scoreboardX, scoreboardY);
                            Console.WriteLine($"{leftPlayerPoints} | {rightPlayerPoints}");
                            if (leftPlayerPoints == 10)
                            {
                                break;
                            }
                        }
                    }
                }

                // Controleer of iemand gewonnen heeft
                if (rightPlayerPoints == 10 || leftPlayerPoints == 10)
                {
                    break;
                }

                // Verwerk invoer van de gebruiker om de rackets te bewegen
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.UpArrow:
                        if (rightRacketHeight > 0)
                        {
                            rightRacketHeight--;
                        }
                        break;

                    case ConsoleKey.DownArrow:
                        if (rightRacketHeight < fieldWidth - racketLength - 1)
                        {
                            rightRacketHeight++;
                        }
                        break;

                    case ConsoleKey.W:
                        if (leftRacketHeight > 0)
                        {
                            leftRacketHeight--;
                        }
                        break;

                    case ConsoleKey.S:
                        if (leftRacketHeight < fieldWidth - racketLength - 1)
                        {
                            leftRacketHeight++;
                        }
                        break;
                }

                // Wis de oude racketposities
                for (int i = 1; i < fieldWidth; i++)
                {
                    Console.SetCursorPosition(0, i);
                    Console.WriteLine(" ");
                    Console.SetCursorPosition(fieldLength - 1, i);
                    Console.WriteLine(" ");
                }
            }

            // Eindspelbericht
            Console.SetCursorPosition(0, fieldWidth + 2);

            if (rightPlayerPoints == 10)
            {
                Console.WriteLine("Rechter speler heeft gewonnen!");
            }
            else
            {
                Console.WriteLine("Linker speler heeft gewonnen!");
            }

            Console.WriteLine("Druk op een toets om af te sluiten...");
            Console.ReadKey();
        }
    }
}
