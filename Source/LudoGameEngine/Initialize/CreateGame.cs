using Edokan.KaiZen.Colors;
using LudoBoard.DataAccess;
using LudoBoard.DataModels;
using LudoGameEngine.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudoGameEngine.Initialize
{
    public class CreateGame
    {
        private static int userInput = 0;
        private static bool isRunning = true;


        /// <summary>
        /// INITIALIZE GAME
        /// </summary>
        public void CreateNewGame()
        {
            // Creating a new board
            Game board = new Game();
            GameLoop gameLoop = new GameLoop();

            // Creating amount of players
            Console.WriteLine("How many players? (2-4)");

            List<Player> player = new List<Player>();
            LudoDbAccess ludoDbAccess = new LudoDbAccess();
            int highestId = ludoDbAccess.GetHighestPlayerId();
            GameBoard gameBoards = new GameBoard();

            // Checking the amount of players
            while (isRunning)
            {
                int.TryParse(Console.ReadLine(), out userInput);

                if (userInput > 1 && userInput < 5)
                {
                    for (int i = 0; i < userInput; i++)
                    {
                        player.Add(new Player() {
                            Id = highestId + i + 1,
                            PlayerBoard = gameBoards.PlayersGameBoards[i]
                        });                
                    }

                    isRunning = false;
                }
                else
                {
                    Console.WriteLine("Please enter a valid number.");
                }
            }

            CreatePlayer(board, player);
            gameLoop.RunGame(player);
        }

        private static void CreatePlayer(Game board, List<Player> player)
        {
            // Slumpar fram vem som startar.
            Random randomStartPlayer = new Random();
            int playerId = randomStartPlayer.Next(1, player.Count);

            List<string> colors = new List<string>() { "Red".Red(), "Blue".Blue(), "Green".Green(), "Yellow".Yellow() };
            using (var context = new LudoDbContext())
            {
                for (int i = 0; i < player.Count; i++)
                {
                    // SET Player Color
                    player[i].PlayerColor = colors[i];

                    // SET Player Name
                    bool isRunning = true;
                    while (isRunning)
                    {
                        Console.Write($"\nPlayer {i + 1} Name: ");
                        player[i].Name = Console.ReadLine().ToString();

                        bool containsInt = player[i].Name.Any(char.IsDigit);

                        if (containsInt == true)
                        {
                            Console.Write("No numbers as a name, try again.");
                        }
                        else if (player[i].Name == String.Empty)
                        {
                            Console.WriteLine("Please enter a name..");
                        }
                        else
                        {
                            Console.WriteLine($"Added Player {i + 1} | Name: {player[i].Name} | Color: {player[i].PlayerColor} |");
                            isRunning = false;
                        }
                    }

                    if (playerId == i + 1)
                    {
                        player[i].PlayerTurn = true;
                        Console.WriteLine($"{player[i].Name} starts.");
                    }
                }
            }

            CreatePieces(board, player);
        }

        private static void CreatePieces(Game board, List<Player> player)
        {
            if (player is null)
            {
                throw new ArgumentNullException(nameof(player));
            }

            // SET Piece Id, Position
            List<Piece> piece = new List<Piece>();
            var counter = 0;
            for (int i = 0; i < player.Count; i++)
            {
                piece.Add(new Piece());
                piece.Add(new Piece());
                piece.Add(new Piece());
                piece.Add(new Piece());

                for (int x = 0; x < 4; x++)
                {
                    piece[counter + x].Position = GameBoard.nestPositions[i];
                    piece[counter + x].IsActive = true;
                }
                counter += 4;
            }

            LudoDbAccess ludoDbAccess = new LudoDbAccess();
            ludoDbAccess.SaveGame(board, player, piece);

        }       
    }
}
