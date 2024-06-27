using System;

namespace Pong
{
    public class Program
    {
        // Hoofdinvoerpunt van het programma
        public static void Main(string[] args)
        {
            try // Error handling start
            {
                // Maak een nieuwe game-instantie aan
                Game game = new Game();
                // Start het spel
                game.Start();
            }
            catch (Exception ex)
            {
                // Log de fout naar de console
                Console.WriteLine($"Er is een fout opgetreden: {ex.Message}");
                // Optioneel: log het volledige stacktrace voor debugging
                Console.WriteLine(ex.StackTrace);
            } // Error handling einde
        }
    }
}
