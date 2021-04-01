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
                        Console.WriteLine("Added a player to List");
                        player.Add(new Player());
                    }
                    isRunning = false;
                }
                else
                {
                    Console.WriteLine("Please enter a valid number.");
                }
            }

            List<string> colors = new List<string>() { "Red", "Blue", "Green", "Yellow" };
            for (int i = 0; i < player.Count; i++)
            {
                // Försök att fixa ENUM
                foreach (var color in Enum.GetValues(typeof(Color)))
                {

                }

                //Sätter spelarens färg
                player[i].PlayerColor = colors[i];
                Console.WriteLine($"Player {i + 1} Name: ");

                //Sätter spelarens namn
                player[i].Name = Console.ReadLine().ToString();

                Console.WriteLine($"Player {i + 1} Name: {player[i].Name} Color: {player[i].PlayerColor}");

                CreatePieces(player);

            }






        }

        public static void CreatePieces(List<Player> Player)
        {

            List<Piece> piece = new List<Piece>();

            //for (int i = 0; i < Player.Count; i++)
            //{
            //    piece.Add(new Piece());
            //    piece[i].PlayerId = Player[i].Id; 

            //    piece.Add(new Piece());
            //    piece[i].PlayerId = Player[i].Id;


            //    piece.Add(new Piece());
            //    piece[i].PlayerId = Player[i].Id;

            //    piece.Add(new Piece());
            //    piece[i].PlayerId = Player[i].Id;

            //    Console.WriteLine(piece[i].PlayerId);
            //}
        }


        public static void LoadGame()
        {
            //Laddar ett spel som man kan välja i en lista
        }

        public void CurrentBoard()
        {

        }

        public void ContinueGame()
        {
            Game.LoadGame(); //Ladda senast sparade spelet
        }

    }

}
