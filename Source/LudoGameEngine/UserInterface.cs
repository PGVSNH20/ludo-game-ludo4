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
                                    "[1] Create new game\n" +
                                    "[2] Load game");

                MenuChoice();              
            }
        }
        public static void LoadGame()
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

                MenuChoice();

                //TODO: Ge ett felmeddelande om man inte har ett sparat spel och gå tillbaka till menyn

            }
        }
        public static void MenuChoice()
        {
            int.TryParse(Console.ReadLine(), out userInput);

            switch (userInput)
            {

                case 1:
                    Game.CreateGame();
                    isRunning = false;
                    break;

                case 2:
                    LoadGame();
                    isRunning = false;
                    break;

                default:
                    Console.WriteLine("You need to press 1 or 2");
                    break;
            }
        }
    }
}
