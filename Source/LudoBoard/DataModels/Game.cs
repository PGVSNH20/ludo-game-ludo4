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

        public static void CreateGame()
        {
            // Creating a new board
            Game board = new Game();

            // Retrieving the highest Id from db.
            int higestBoardId = new LudoDbAccess().GetHigestBoardId();
            board.Id = higestBoardId + 1;

            // Creating amount of players
            Console.WriteLine("How many players? (2-4)");
            
            List<Player> player = new List<Player>();

            // Checking the amount of players
            while (isRunning)
            {
                int.TryParse(Console.ReadLine(), out userInput);

                if (userInput > 1 && userInput < 5)
                {
                    for (int i = 0; i < userInput; i++)
                    {                      
                        player.Add(new Player());
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
            PlayGame();
        }

        private static void CreatePlayer(Game board, List<Player> player)
        {
            // Retrieving the highest Id from db.
            int higestPlayerId = new LudoDbAccess().GetHigestPlayerId();

            List<string> colors = new List<string>() { "Red", "Blue", "Green", "Yellow" };
            for (int i = 0; i < player.Count; i++)
            {
                // SET Player id, color
                player[i].Id = higestPlayerId + i + 1;
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
                    else
                    {
                        Console.WriteLine($"Added Player {i + 1} | Name: {player[i].Name} | Color: {player[i].PlayerColor} |");
                        isRunning = false;
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
                }
                counter += 4;

                Console.WriteLine($"Added x4 pieces with ID: {piece[i].Id} and their position are Board-Index: {nestPositions[i]}");
                Console.ReadLine();
            }

            LudoDbAccess ludoDbAccess = new LudoDbAccess();
            ludoDbAccess.SaveGame(board, player, piece);
        }

        public static void LoadGame()
        {
            //Laddar ett spel som man kan välja i en lista
        }

        public void CurrentBoard()
        {
            List<string> gb = new List<string>(60)         {"    ","    ","    ","    ","    ","    ","    ","    ","    ","    ",
                                                            "    ","    ","    ","    ","    ","    ","    ","    ","    ","    ",
                                                            "    ","    ","    ","    ","    ","    ","    ","    ","    ","    ",
                                                            "    ","    ","    ","    ","    ","    ","    ","    ","    ","    ",
                                                            "    ","    ","    ","    ","    ","    ","    ","    ","    ","    ",
                                                            "    ","    ","    ","    ","    ","    ","    ","    ","    ", "    ", "    "};


            Console.WriteLine($"[{gb[0]}]                  [{gb[1]}][{gb[2]}][{gb[3]}]                  [{gb[4]}]\n" +
                              $"                        [{gb[5]}][{gb[6]}][{gb[7]}]\n" +
                              $"                        [{gb[8]}][{gb[9]}][{gb[10]}]\n" +
                              $"                        [{gb[11]}][{gb[12]}][{gb[13]}]\n" +
                              $"[{gb[14]}][{gb[15]}][{gb[16]}][{gb[17]}][{gb[18]}][{gb[19]}][{gb[20]}][{gb[21]}][{gb[22]}][{gb[23]}][{gb[24]}]\n" +
                              $"[{gb[25]}][{gb[26]}][{gb[27]}][{gb[28]}][{gb[29]}][{gb[30]}][{gb[31]}][{gb[32]}][{gb[33]}][{gb[34]}][{gb[35]}]\n" +
                              $"[{gb[36]}][{gb[37]}][{gb[38]}][{gb[39]}][{gb[40]}][{gb[41]}][{gb[42]}][{gb[43]}][{gb[44]}][{gb[45]}][{gb[46]}]\n" +
                              $"                        [{gb[47]}][{gb[48]}][{gb[49]}]\n" +
                              $"                        [{gb[50]}][{gb[51]}][{gb[52]}]\n" +
                              $"                        [{gb[53]}][{gb[54]}][{gb[55]}]\n" +
                              $"[{gb[56]}]                  [{gb[57]}][{gb[58]}][{gb[59]}]                  [{gb[60]}]\n");
        }

        public void ContinueGame()
        {
            Game.LoadGame(); //Ladda senast sparade spelet
        }
        private static void PlayGame()
        {
            // TODO - WhoGoesFirst();
            Board board = new Board();
            board.WhoGoesFirst(); // Skapa denna metod i board
            // Slå tärning
            Dice rollDice = new Dice();
            rollDice.RollDice();
            // Gå med piece "MovePiece"
            board.
            // Start ordning
            // PlayerTurn Ska lägga in property i player (done) så att vi ser vems tur det är om spelet avbryts.
            // 
        }


    }

}
