using System;

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
                // Het Monitor-patroon wordt hier gebruikt om ervoor te zorgen dat de bewerkingen 
                // voor het tekenen van het racket op de console thread-safe zijn. Dit is handig 
                // omdat het voorkomt dat meerdere threads tegelijkertijd toegang hebben tot de 
                // tekenbewerkingen, wat kan leiden tot inconsistente weergave of fouten.
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
                // Het Monitor-patroon zorgt hier ook voor thread-safe bewerkingen bij het wissen 
                // van het racket van de console. Dit voorkomt problemen wanneer meerdere threads 
                // tegelijkertijd proberen de console-output te wijzigen.
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
                // Het Monitor-patroon zorgt ervoor dat de bewerkingen voor het verplaatsen van het racket 
                // omhoog thread-safe zijn, waardoor wordt voorkomen dat meerdere threads tegelijkertijd 
                // toegang hebben tot en wijzigingen aanbrengen in de racketpositie.
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
                // Het Monitor-patroon zorgt ervoor dat de bewerkingen voor het verplaatsen van het racket 
                // omlaag thread-safe zijn, waardoor wordt voorkomen dat meerdere threads tegelijkertijd 
                // toegang hebben tot en wijzigingen aanbrengen in de racketpositie.
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
                // Het Monitor-patroon zorgt ervoor dat de bewerkingen voor het controleren of de bal 
                // het racket raakt thread-safe zijn, waardoor wordt voorkomen dat meerdere threads 
                // tegelijkertijd toegang hebben tot en wijzigingen aanbrengen in de racketpositie.
                return ballY >= yPosition + 1 && ballY <= yPosition + length;
            } // Monitor-patroon einde
        }
    }
}
