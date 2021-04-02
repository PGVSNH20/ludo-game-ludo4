using LudoBoard.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudoBoard.DataModels
{

	public class Game
	{
        private static int userInput = 0;
        private static bool isRunning = true;


        public int Id { get; set; }
		public Piece Piece { get; set; }
		public DateTime LastTimePlayedDate { get; set; } //Används vid laddning och sparning av spel
		public DateTime CompletedDate { get; set; }
		public bool IsCompleted { get; set; }


        public static void CreateGame()
        {
            // Generate new game
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

                    isRunning = false;
                }
                else
                {
                    Console.WriteLine("Please enter a valid number.");
                }
            }

            int higestPlayerId = new LudoDbAccess().GetHigestPlayerId();
            List<string> colors = new List<string>() { "Red", "Blue", "Green", "Yellow" };
            for (int i = 0; i < player.Count; i++)
            {
                player[i].Id = higestPlayerId + i + 1;

                // Försök att fixa ENUM
                foreach (var color in Enum.GetValues(typeof(Color)))
                {

                }

                //Sätter spelarens färg
                player[i].PlayerColor = colors[i];

                //Sätter spelarens namn

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

                        Console.WriteLine($"Player {i + 1} | Name: {player[i].Name} | Color: {player[i].PlayerColor} |");

                        CreatePieces(player);
                        isRunning = false;
                    }

                }


            }






        }

        public static void CreatePieces(List<Player> Player)
        {
            //TODO- Fixa denna
            List<Piece> piece = new List<Piece>();
            
            for (int i = 0; i < Player.Count; i++)
            {
                piece.Add(new Piece());
                piece[i].Id = Player[i].Id;

                piece.Add(new Piece());
                piece[i].Id = Player[i].Id;


                piece.Add(new Piece());
                piece[i].Id = Player[i].Id;

                piece.Add(new Piece());
                piece[i].Id = Player[i].Id;

                Console.WriteLine(piece[i].Id);
            }
          
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

    }

}
