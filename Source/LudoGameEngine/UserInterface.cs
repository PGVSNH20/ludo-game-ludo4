using LudoBoard.DataAccess;
using LudoBoard.DataModels;
using System;
using System.Collections.Generic;

namespace LudoGameEngine
{
    public class UserInterface
	{
        private static int userInput = 0;
        private static bool isRunning = true;


        public void MainMenu()
        {

            while (isRunning)
            {
				
                    Console.WriteLine("Hello and welcome to Ludo game! What do you want to do?\n" +
                                    "[1] Continue\n" +
                                    "[2] Create new game" +
                                    "[3] Load game");
            }
        }
        public static void MenuLoadGame() //Visa information om sparade spel och ladda det. Skall kunna hoppa tillbaka till menyn. Om det inte finns någon data ge ett felmeddelande och skicka tillbaka till huvudmenyn.
        {
            // Laddar en lista med spel från LudoDbAccess som man sedan ska kunna välja från Load Game Menyn
            List<Game> games = new LudoDbAccess().GetAllUnfinishedGames();
            List<Player> players = new LudoDbAccess().GetAllPlayers();
            
            while (isRunning)
            {
                Console.Clear();
                Console.WriteLine("What game do you want to load?\n");

                for (int i = 0; i < games.Count; i++)
                {
                    Console.WriteLine($"[{i}]\nGame Id: {games[i].Id} Last Played: {games[i].LastTimePlayedDate}");
                    Console.Write($"Players:");
                    foreach(Player p in players)
                    {
                        if (p.GameId == games[i].Id)
                            Console.Write($" {p.Name}");
                    }

                    Console.WriteLine($"\n---------------------------------------------------------------------------\n");
                }

                //TODO: Ge ett felmeddelande om man inte har ett sparat spel och gå tillbaka till menyn
            }
        }
    }
}
