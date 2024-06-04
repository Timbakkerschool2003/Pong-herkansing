using System;

namespace Pong
{
    public class Program
    {
        // Hoofdinvoerpunt van het programma
        public static void Main(string[] args)
        {
            // Maak een nieuwe game-instantie aan
            Game game = new Game();
            // Start het spel
            game.Start();
        }
    }
}
