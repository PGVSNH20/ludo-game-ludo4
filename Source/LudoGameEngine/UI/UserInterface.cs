using LudoBoard.DataAccess;
using LudoBoard.DataModels;
using LudoGameEngine.Initialize;
using System;
using System.Collections.Generic;

namespace LudoGameEngine.UI
{
    public class UserInterface
	{
        private static int userInput = 0;
        private static bool isRunning = true;


        public void MainMenu()
        {
            GameLoop gameLoop = new GameLoop();
            while (isRunning)
            {
				Console.Clear();
                Console.WriteLine("Hello and welcome to Ludo game! What do you want to do?\n" +
                                    "[1] Continue\n" +
                                    "[2] Create new game\n" +
                                    "[3] Load game");

                int.TryParse(Console.ReadLine(), out userInput);

                switch (userInput)
                {

                    case 1:
                        gameLoop.InitializeGame(1);
                        break;
                    case 2:
                        gameLoop.InitializeGame(2);
                        break;
                    case 3:
                        MenuLoadGame();
                        break;

                    default:
                        Console.WriteLine("You need to press 1 or 2");
                        break;
                }
            }
        }


        public static void MenuLoadGame() //Visa information om sparade spel och ladda det. Skall kunna hoppa tillbaka till menyn. Om det inte finns någon data ge ett felmeddelande och skicka tillbaka till huvudmenyn.
        {
            // Laddar en lista med spel från LudoDbAccess som man sedan ska kunna välja från Load Game Menyn
            List<Game> games = new LudoDbAccess().GetAllUnfinishedGames();
            List<Player> players = new LudoDbAccess().GetAllPlayers();

            while (isRunning)
            {
                Console.WriteLine("What game do you want to load?\n");

                for (int i = 0; i < games.Count; i++)
                {
                    Console.WriteLine($"[{i+1}]\nGame Id: {games[i].Id} Last Played: {games[i].LastTimePlayedDate}");
                    Console.Write($"Players:");
                    foreach(Player p in players)
                    {
                        if (p.GameId == games[i].Id)
                            Console.Write($" {p.Name}");
                    }

                    Console.WriteLine($"\n---------------------------------------------------------------------------\n");
                }

                int.TryParse(Console.ReadLine(), out userInput);

                foreach (var g in games)
                {
                    if (userInput == g.Id)
                    {
                        LoadGame loadGame = new LoadGame();

                        Console.WriteLine($"Loading game id {g.Id}...");
                        loadGame.LoadAnyGame(g.Id);
                    }
                }

                //TODO: Ge ett felmeddelande om man inte har ett sparat spel och gå tillbaka till menyn
            }
        }
    }
}
