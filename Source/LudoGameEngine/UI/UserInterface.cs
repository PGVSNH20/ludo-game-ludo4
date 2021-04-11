using Edokan.KaiZen.Colors;
using LudoBoard.DataAccess;
using LudoBoard.DataModels;
using LudoGameEngine.Initialize;
using System;
using System.Collections.Generic;
using System.Threading;

namespace LudoGameEngine.UI
{
    public class UserInterface
	{
        public static readonly List<string> playerColors = new List<string> { "Red".Red(), "Blue".Blue(), "Yellow".Yellow(), "Green".Green() };
        static int playerCounter = 0;

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

        public static void MenuLoadGame()
        {
            GameLoop gameLoop = new GameLoop();
            while (isRunning)
            {
                Console.Clear();
                Console.WriteLine("[1] Load Game\n" +
                                    "[2] Completed Games\n");

                int.TryParse(Console.ReadLine(), out userInput);

                switch (userInput)
                {

                    case 1:
                        LoadGamesUI();
                        break;
                    case 2:
                        LoadOldGamesUI();
                        break;

                    default:
                        Console.WriteLine("You need to press 1 or 2");
                        break;
                }
            }


        }

        // Loading Active Games To List, Player Can Choose What Game To Continue Playing
        public static void LoadGamesUI()
        {
            Console.Clear();

            List<Game> games = new LudoDbAccess().GetAllUnfinishedGames();
            List<Player> players = new LudoDbAccess().GetAllPlayers();

            UserInterface userInterface = new UserInterface();
            if (games.Count == 0)
            {
                Console.WriteLine("Sorry mate, no games to be found " +
                                  "\nPlease create a new game.");
                Thread.Sleep(2000);
                userInterface.MainMenu();
            }

            while (isRunning)
            {
                Console.WriteLine("What game do you want to load?\n");

                for (int i = 0; i < games.Count; i++)
                {
                    playerCounter = 0;
                    Console.WriteLine($"[{games[i].Id}]\nGame Id: {games[i].Id}\nLast Played: {games[i].LastTimePlayedDate}");
                    Console.Write($"Players:");

                    
                    foreach (Player p in players)
                    {
                        
                        if (p.GameId == games[i].Id)
                        {
                            Console.Write($"\nPlayer: {p.Name} | Color: {playerColors[playerCounter]}");
                            playerCounter++;
                        }
                       
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
            }
        }

        // Loading Old Games to view in a List
        public static void LoadOldGamesUI()
        {
            Console.Clear();
            List<Game> games = new LudoDbAccess().GetAllFinishedGames();
            List<Player> players = new LudoDbAccess().GetAllPlayers();
            UserInterface userInterface = new UserInterface();
            if (games.Count == 0)
            {
                Console.WriteLine("Sorry mate, no games to be found " +
                                  "\nPlease create a new game.");

                Thread.Sleep(2000);
                userInterface.MainMenu();
            }

            while (isRunning)
            {
                Console.WriteLine("Here are all your completed games! \n");

                for (int i = 0; i < games.Count; i++)
                {
                    playerCounter = 0;
                    Console.WriteLine($"Game Id: {games[i].Id}\nGame Completed: {games[i].CompletedDate}\nWinner: {games[i].WinnerPlayerName}");
                    Console.Write($"Players:");
                    foreach (Player p in players)
                    {
                        if (p.GameId == games[i].Id)
                        {
                            Console.Write($"\nPlayer: {p.Name} | Color: {playerColors[playerCounter]}");
                            playerCounter++;
                        }
                    }


                    Console.WriteLine($"\n---------------------------------------------------------------------------\n");
                }

                Console.WriteLine("[1] Go To Main Menu");
                do
                {
                    int.TryParse(Console.ReadLine(), out userInput);
                } while (userInput != 1);

                userInterface.MainMenu();
            }
        }
    }
}
