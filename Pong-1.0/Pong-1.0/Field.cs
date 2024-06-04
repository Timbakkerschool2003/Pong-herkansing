using System;

namespace Pong
{
    public class Field
    {
        // Readonly velden voor veldafmetingen en tegelkarakter
        private readonly int length;
        private readonly int width;
        private readonly char horizontalTile = '-';
        private readonly char verticalTile = '|';
        private readonly string horizontalLine;

        // Singleton-patroon start
        // Statisch veld om de enige instantie van de klasse bij te houden
        // Het Singleton-patroon wordt hier gebruikt om ervoor te zorgen dat er slechts één instantie van het veld
        // bestaat in het spel. Dit is handig omdat het veld uniek moet zijn en overal in de applicatie toegankelijk 
        // moet zijn zonder meerdere keren te worden geïnitialiseerd.
        private static Field instance;

        // Privéconstructor om te voorkomen dat er van buitenaf een instantie wordt aangemaakt
        private Field(int length, int width)
        {
            this.length = length;
            this.width = width;
            this.horizontalLine = new string(horizontalTile, length);
        }

        // Openbare methode om de enige instantie van de klasse te krijgen
        public static Field GetInstance(int length, int width)
        {
            if (instance == null)
            {
                instance = new Field(length, width);
            }
            return instance;
        }
        // Singleton-patroon einde

        // Teken de veldgrenzen
        public void Draw()
        {
            // Teken de bovenste grens
            Console.SetCursorPosition(0, 0);
            Console.WriteLine(horizontalLine);

            // Teken de linker- en rechtergrens
            for (int i = 1; i < width; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write(verticalTile);
                Console.SetCursorPosition(length - 1, i);
                Console.Write(verticalTile);
            }

            // Teken de onderste grens
            Console.SetCursorPosition(0, width);
            Console.WriteLine(horizontalLine);
        }
    }
}
