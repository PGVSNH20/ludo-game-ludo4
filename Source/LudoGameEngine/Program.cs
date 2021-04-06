using LudoBoard.DataAccess;
using LudoBoard.DataModels;

namespace LudoGameEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            var runMenu = new UserInterface();

            runMenu.MainMenu();
        }

        public void GameLoop()
		{
            //Pseudo kod
            //   initialize
            //   måste kolla hur många spelare spelar
            //   skapa sedan en lista på spelare
            //   initialisera spelet:
            //       skapa spelare
            //       skapa bräde
            //       skapa pjäser
            //
            //	while (isPlaying)
            //	{
            //       loopa genom alla spelare som ligger i en lista eller liknande
            //       loop 2 (listan av spelare loopas)
            //           {
            //           player turn
            //           diceroll (kolla om det finns pjäser på brädet, om det ej finns pjäser och värdet är för lågt skippa till nästa spelare)
            //           player move (hoppa ut ur nästet, flytta pjäs)
            //           --uppdatera brädets grafik--- (console clear)
            //           nästa spelare
            //           spara till databas
            //           }
            //       loopar tills spelet är över eller man stänger av spelet aka isPlaying = false
            // }
            }
        }
}
