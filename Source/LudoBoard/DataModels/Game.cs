using LudoBoard.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LudoBoard.DataModels
{

    public class Game
	{
        private static int userInput = 0;
        private static bool isRunning = true;

        public int Id { get; set; }
        public DateTime? LastTimePlayedDate { get; set; } //Används vid laddning och sparning av spel
		public DateTime? CompletedDate { get; set; }
        public bool IsCompleted { get; set; } = false;
        public string WinnerPlayerName { get; set; } = "N/A";
        public ICollection<Player> Players { get; set; }

        public static List<int> player1GameBoard = new List<int>()
            {
                0,14,15,16,17,18,11,8,5,1,2,3,7,10,13,20,21,22,23,24,35,46,45,44,43,42,49,52,55,59,58,57,53,50,47,40,39,38,37,36,25,26,27,28,29,30
            };

        public static List<int> player2GameBoard = new List<int>()
            {
                4,3,7,10,13,20,21,22,23,24,35,46,45,44,43,42,49,52,55,59,58,57,53,50,47,40,39,38,37,36,25,14,15,16,17,18,11,8,5,1,2,6,9,12,19,30
            };

        public static List<int> player3GameBoard = new List<int>()
            {
                60,46,45,44,43,42,49,52,55,59,58,57,53,50,47,40,39,38,37,36,25,14,15,16,17,18,11,8,5,1,2,3,7,10,13,20,21,22,23,24,35,34,33,32,31,30
            };

        public static List<int> player4GameBoard = new List<int>()
            {
                56,57,53,50,47,40,39,38,37,36,25,14,15,16,17,18,11,8,5,1,2,3,7,10,13,20,21,22,23,24,35,46,45,44,43,42,49,52,55,59,58,54,51,48,41,30
            };
        public static void CreateGame()
        {
            // Creating a new board
            Game board = new Game();

            // Creating amount of players
            Console.WriteLine("How many players? (2-4)");
            
            List<Player> player = new List<Player>();
            LudoDbAccess ludoDbAccess = new LudoDbAccess();
            int highestId = ludoDbAccess.GetHighestPlayerId();


            // Checking the amount of players
            while (isRunning)
            {
                int.TryParse(Console.ReadLine(), out userInput);

                if (userInput > 1 && userInput < 5)
                {
                    for (int i = 0; i < userInput; i++)
                    {
                        player.Add(new Player() { Id = highestId + i + 1 });
                        /*PlayerBoard = PlayerBoard(player[ i + 1 ]GameBoard}*/

                    }
                    Console.WriteLine($"Added: \"{userInput}\" players to list");
                    Console.ReadLine();
                    
                    isRunning = false;
                }
                else
                {
                    Console.WriteLine("Please enter a valid number.");
                }
            }

            CreatePlayer(board, player);
            PlayGame(player);
        }

        private static void CreatePlayer(Game board, List<Player> player)
        {
            // Slumpar fram vem som startar.
            Random randomStartPlayer = new Random();            
            int playerId = randomStartPlayer.Next(1, player.Count);

            List<string> colors = new List<string>() { "Red", "Blue", "Green", "Yellow" };
            using (var context = new LudoDbContext())
            {
                for (int i = 0; i < player.Count; i++)
                {
                    if (i == 0)
                    {
                        foreach (var value in player1GameBoard)
                        {
                            player[i].PlayerBoard.Add(value);
                        }
                        context.SaveChanges();
                    }

                    else if (i == 1)
                    {
                        foreach (var value in player2GameBoard)
                        {
                            player[i].PlayerBoard.Add(value);
                        }
                        context.SaveChanges();
                    }
                    else if (i == 2)
                    {
                        foreach (var value in player3GameBoard)
                        {
                            player[i].PlayerBoard.Add(value);
                        }
                        context.SaveChanges();
                    }
                    else if (i == 3)
                    {
                        foreach (var value in player4GameBoard)
                        {
                            player[i].PlayerBoard.Add(value);
                        }
                        context.SaveChanges();
                    }
                    else
                    {
                        throw new Exception();
                    }
                    if (playerId == i + 1)
                    {
                        player[i].PlayerTurn = true;
                        Console.WriteLine($"{player[i].Name} starts.");
                    }
                    // SET Player id, color

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

            // Board Nest Positions @Current map.
            List<int> nestPositions = new List<int> { 0, 4, 56, 60 };

            // SET Piece Id, Position
            List<Piece> piece = new List<Piece>();
            var counter = 0;
            for (int i = 0; i < player.Count; i++)
            {
                piece.Add(new Piece());                              
                piece.Add(new Piece());
                piece.Add(new Piece());
                piece.Add(new Piece());

                for (int x = 0; x < 4;x++)
                {
                    piece[counter + x].Position = nestPositions[i];
                    piece[counter + x].IsActive = true;
                }
                counter += 4;

                Console.WriteLine($"Added x4 pieces with ID: {piece[i].Id} and their position are Board-Index: {nestPositions[i]}");
                Console.ReadLine();
            }

            LudoDbAccess ludoDbAccess = new LudoDbAccess();
            ludoDbAccess.SaveGame(board, player, piece);
        }

        public void ContinueGame()
        {
            // TODO - Här måste vi hämta GetHighestBoardId för att fortsätta spela klart senast sparade spelet
            //Ladda senast sparade spelet
        }
        private static void PlayGame(List<Player> players)
        {
            Board board = new Board();
            Dice rollDice = new Dice();
            List<Piece> onePlayersPieces = new List<Piece>();
            List<object> playerGameBoard = new List<object>();

            foreach (var player in players)
            {
                if (player.PlayerTurn == true)
                {
                    playerGameBoard.Add(player.PlayerBoard);
                    LudoDbAccess ludoDbAccess = new LudoDbAccess();
                    
                    Console.WriteLine($"It's player {player.Name} turn to roll the dice.");
                    onePlayersPieces = ludoDbAccess.GetCurrentPlayersPieces(player.Id);
                }
            }
            Console.WriteLine("1. rolldice");
            Console.ReadKey();
            // Slå tärning            
            int i = rollDice.RollDice();
            Console.WriteLine($"You rolled {i}");

            // TODO - Lägg till check om det finns pieces i nest.
            //if (i == 6)
            //{
            //    board.AskIfMoveFromNestOrMoveOnBoard();
            //}
            //else
            //{                
            //    board.MovePiece(onePlayersPieces, i);
            //}
            board.MovePiece(onePlayersPieces, i, playerGameBoard);
            // PlayerTurn Ska lägga in property i player (done) så att vi ser vems tur det är om spelet avbryts.
            // 
        }
    }
}
