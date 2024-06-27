using System;
using System.Collections.Generic;

namespace Pong
{
    public class Ball
    {
        // Velden voor de balpositie en bewegingsrichting
        private int x;
        private int y;
        private readonly int fieldLength;
        private readonly int fieldWidth;
        private readonly char tile = 'O';

        private bool isGoingDown = true;
        private bool isGoingRight = true;

        private List<IObserver> observers = new List<IObserver>();

        // Constructor om de balpositie en veldafmetingen te initialiseren
        public Ball(int startX, int startY, int fieldLength, int fieldWidth)
        {
            this.x = startX;
            this.y = startY;
            this.fieldLength = fieldLength;
            this.fieldWidth = fieldWidth;
        }

        // Observer-patroon start
        // Het Observer-patroon wordt hier gebruikt om meerdere objecten (de observers) op de hoogte te stellen
        // van veranderingen in de balpositie. Dit is handig omdat het de bal in staat stelt om automatisch 
        // te communiceren met andere onderdelen van het spel, zoals de rackets en de scoreborden, zonder 
        // dat deze onderdelen direct aan elkaar gekoppeld zijn.

        // Voeg een observer toe aan de lijst
        public void AddObserver(IObserver observer)
        {
            observers.Add(observer);
        }

        // Verwijder een observer uit de lijst
        public void RemoveObserver(IObserver observer)
        {
            observers.Remove(observer);
        }

        // Stel alle observers op de hoogte
        private void NotifyObservers()
        {
            foreach (var observer in observers)
            {
                observer.Update(this);
            }
        }

        // Observer-patroon einde

        // Teken de bal op de console
        public void Draw()
        {
            Console.SetCursorPosition(x, y);
            Console.Write(tile);
        }

        // Wis de bal van de console
        public void Clear()
        {
            Console.SetCursorPosition(x, y);
            Console.Write(' ');
        }

        // Beweeg de bal en handel botsingen af
        public void Move(Racket leftRacket, Racket rightRacket, ref int leftPlayerPoints, ref int rightPlayerPoints)
        {
            Clear();

            // Algorithm start: Ball movement and collision detection
            // Werk de balpositie bij
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

            // Handel botsingen met de boven- of onderkant van het veld af
            if (y == 1 || y == fieldWidth - 1)
            {
                isGoingDown = !isGoingDown;
            }

            // Handel botsingen met het linkerracket of scoren voor de rechterspeler af
            if (x == 2)  // Aangepaste positie voor linkerracket
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

            // Handel botsingen met het rechterracket of scoren voor de linker speler af
            if (x == fieldLength - 3)  // Aangepaste positie voor rechterracket
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
            // Algorithm end: Ball movement and collision detection

            Draw();
            NotifyObservers();
        }

        // Reset de bal naar het midden van het veld
        private void Reset()
        {
            x = fieldLength / 2;
            y = fieldWidth / 2;
        }

        // Getters voor de balpositie
        public int X { get { return x; } }
        public int Y { get { return y; } }
    }

    public interface IObserver
    {
        void Update(Ball ball);
    }
}
