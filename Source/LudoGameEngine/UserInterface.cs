using LudoBoard.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

                int.TryParse(Console.ReadLine(), out userInput);

                switch (userInput)
                {
                    case 1:
                        CreateGame();
                        isRunning = false;
                        break;

                    case 2:
                        isRunning = false;
                        break;

                    default:
                        Console.WriteLine("You need to press 1 or 2");
                        break;

                }
            }
        }

		public void CreateGame()
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

            List<string> colors = new List<string>() { "Red", "Blue", "Green", "Yellow"};
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
            }
        }

		public void CurrentBoard()
		{

		}

		public void ContinueGame()
		{
			LoadGame(); //Ladda senast sparade spelet
		}

		public void LoadGame()
		{
			//Laddar ett spel som man kan välja i en lista
		}

	}
}
